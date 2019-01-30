using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GammaLibrary.Enhancements;
using GammaLibrary.Extensions;

namespace PixivSharp
{
    public class PixivImageClient
    {
        public static PixivImageClient GlobalClient { get; } = new PixivImageClient();

        private static readonly ThreadLocal<HttpClientEx> Client;
        public static IWebProxy Proxy { get; set; }

        static PixivImageClient()
        {
            Client = new ThreadLocal<HttpClientEx>(() => new HttpClientEx(new HttpClientHandler()));
            Proxy = Client.Value.Proxy;
        }

        public Task<FileInfo> DownloadToDirectoryAsync(Uri url, string path, IProgress<double> progress = null, CancellationToken cancellationToken = default)
        {
            return DownloadToDirectoryAsync(url.ToString(), path, progress, cancellationToken);
        }

        public async Task<FileInfo> DownloadToDirectoryAsync(string url, string path, IProgress<double> progress = null, CancellationToken cancellationToken = default)
        {
            var realPath = Path.Combine(path, url.Split('/').Last());
            await DownloadAsync(url, realPath, progress, cancellationToken);
            return new FileInfo(realPath);
        }

        public Task DownloadAsync(Uri url, string path, IProgress<double> progress = null, CancellationToken cancellationToken = default)
        {
            return DownloadAsync(url.ToString(), path, progress, cancellationToken);
        }

        public async Task DownloadAsync(string url, string path, IProgress<double> progress = null, CancellationToken cancellationToken = default)
        {
            var client = Client.Value;
            client.Proxy = Proxy;
            client.DefaultRequestHeaders.Referrer = new Uri(url);

            await client.DownloadAsync(url, path, progress, cancellationToken);
        }
    }
}
