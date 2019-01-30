using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using GammaLibrary.Enhancements;
using GammaLibrary.Extensions;

namespace PixivSharp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = PixivClient.GlobalClient;

            await client.LoginAsync("YOUR_ACCOUNT", "YOUR_PASSWORD");
            Console.WriteLine(client.PixivSession.AccessToken);

            var favorites = await (await PixivClient.GlobalClient.GetUserBookmarksAsync(PixivClient.GlobalClient.PixivSession.ID)).GetAllIllusts();
            var favoritesLength = favorites.Length;
            Console.WriteLine(favorites);
        }
    }
    
}
