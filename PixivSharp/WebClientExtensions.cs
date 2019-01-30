using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GammaLibrary.Extensions;

namespace PixivSharp
{
    public static class WebClientExtensions
    {
        public static async Task<T> GetJsonAsync<T>(this WebClient client, string address)
        {
            var str = await client.DownloadStringTaskAsync(address);
            return str.JsonDeserialize<T>();
        }

        public static async Task<T> GetHeavyJsonAsync<T>(this WebClient client, string address)
        {
            var str = await client.DownloadStringTaskAsync(address);
            return await Task.Run(() => str.JsonDeserialize<T>());
        }

        public static async Task<T> PostJsonAsync<T>(this WebClient client, string address, string data)
        {
            var str = await client.UploadStringTaskAsync(address, data);
            return str.JsonDeserialize<T>();
        }

        public static async Task<T> PostHeavyJsonAsync<T>(this WebClient client, string address, string data)
        {
            var str = await client.UploadStringTaskAsync(address, data);
            return await Task.Run(() => str.JsonDeserialize<T>());
        }
    }
}
