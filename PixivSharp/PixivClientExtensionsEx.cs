using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixivSharp.Objects;
using AnimatedGif;
using GammaLibrary.Extensions;

namespace PixivSharp
{
    public static class PixivClientExtensionsEx
    {
        public static Task<Illust[]> GetAllBookmarksAsync(this PixivClient client, UserID id)
        {

        }

        public static Task<UgoiraData> GetUgoiraData(this Illust illust)
        {
            if (illust.Type != IllustType.Ugoira) throw new ArgumentException("this illust isn't ugoira", nameof(illust));
            return PixivClient.GlobalClient.GetUgoiraMetadataAsync(illust.Id);
        }

        public static string GetIllustIDFromUrl(this string url)
        {
            return url.Split('/').Last().Split('_').First();
        }

        public static string GetIllustIDFromUrl(this Uri url)
        {
            return url.ToString().GetIllustIDFromUrl();
        }

        // 这里我不写泛型是因为有的东西(如推荐) 一直翻页会瞬间爆炸
        public static async Task<Illust[]> GetAllIllusts(this PixivClient client, IllustsPage page)
        {
            var result = new List<Illust>(page.Illusts);
            var currentPage = page;

            var count = 0;
            while (currentPage.NextUrl != null)
            {
                var fetchedPage = await client.GetNextPageAsync(currentPage);
                result.AddRange(fetchedPage.Illusts);
                currentPage = fetchedPage;
                Trace.WriteLine($"fetched page: {++count*30}");
            }

            return result.ToArray();
        }

        public static Task<Illust[]> GetAllIllusts(this IllustsPage page)
        {
            return PixivClient.GlobalClient.GetAllIllusts(page);
        }

        public static async Task<FileInfo> GetGif(this UgoiraMetadata ugoira)
        {
            var gifPath = Path.Combine(PixivConstants.CacheFolder, $"{ugoira.ZipUrls.Medium.GetIllustIDFromUrl()}.gif");
            if (PixivSharpConfigs.UseCache && File.Exists(gifPath)) return new FileInfo(gifPath);

            // download and extract
            var zip = await PixivImageClient.GlobalClient.DownloadToDirectoryAsync(ugoira.ZipUrls.Medium, PixivConstants.CacheFolder);
            var extractPath = Path.Combine(PixivConstants.CacheFolder, Path.GetFileNameWithoutExtension(zip.Name));
            if (Directory.Exists(extractPath)) Directory.Delete(extractPath, true);

            ZipFile.ExtractToDirectory(zip.FullName, extractPath);

            // create gif
            using (var gif = AnimatedGif.AnimatedGif.Create(gifPath, 0))
            {
                foreach (var frame in ugoira.Frames)
                {
                    gif.AddFrame(Path.Combine(extractPath, frame.File), frame.Delay, GifQuality.Bit8);
                }
            }

            zip.Delete();
            Directory.Delete(extractPath, true);

            return new FileInfo(gifPath);
        }


        public static Uri[] GetImageUrls(this Illust illust)
        {
            return illust.MetaPages.Length == 0 ?
                illust.MetaSinglePage.OriginalImageUrl.AsArray() : 
                illust.MetaPages.Select(page => page.ImageUrls.Original).ToArray();
        }

        public static void OpenBrowser(this Illust illust)
        {
            var url = $"https://www.pixiv.net/member_illust.php?mode=medium&illust_id={illust.Id}";
            url.OpenProcess();
        }

        public static void OpenBrowser(this User user)
        {
            var url = $"https://www.pixiv.net/member.php?id={user.Id}";
            url.OpenProcess();
        }
    }
}
