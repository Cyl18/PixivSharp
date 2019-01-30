using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GammaLibrary.Enhancements;
using GammaLibrary.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PixivSharp.Exceptions;
using PixivSharp.OAuth;

[assembly: Fody.ConfigureAwait(false)]
namespace PixivSharp
{
    public class PixivClient
    {
        public static PixivClient GlobalClient { get; } = new PixivClient();
        public Session PixivSession { get; set; }
        public bool Authed => PixivSession != null;
        public IWebProxy Proxy
        {
            get => client.Proxy;
            set => client.Proxy = value;
        }

        private readonly HttpClientEx client = new HttpClientEx(new HttpClientHandler());

        public PixivClient()
        {
            client.DefaultRequestHeaders.Add("App-OS", "ios");
            client.DefaultRequestHeaders.Add("App-OS-Version", "9.3.3");
            client.DefaultRequestHeaders.Add("App-Version", "6.0.8");
            client.DefaultRequestHeaders.Add("Accept-Language", "zh-cn");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("PixivIOSApp/6.0.8 (iOS 9.3.3; iPhone6,1)");
        }

        public PixivClient(Session session) : this()
        {
            PixivSession = session;
        }

        public void RequireAuth()
        {
            if (!Authed)
                throw new LoginRequiredException();
        }


        private async Task RefreshToken()
        {
            if (PixivSession != null) await PixivSession.RefreshIfRequired();
        }

        public async Task LoginAsync(string userName, string password)
        {
            PixivSession = await PixivAuthClient.AuthAsync(userName, password);
        }

        public Task<T> GetAsync<T>(string path, HttpForm form, string basePath = "https://app-api.pixiv.net")
        {
            var queryString = form.Select(pair => $"{pair.Key}={pair.Value}").Connect("&");
            return GetExAsync<T>(path, queryString, basePath);
        }

        public Task<T> GetExAsync<T>(string path, string queryString, string basePath = "https://app-api.pixiv.net")
        {
            return GetAsync<T>($"{path}?{queryString}", basePath);
        }

        public Task<T> GetAsync<T>(Uri path, string basePath = "https://app-api.pixiv.net")
        {
            return GetAsync<T>(path.ToString(), basePath);
        }

        public Task<T> GetAsync<T>(string path, string basePath = "https://app-api.pixiv.net")
        {
            return GetInternal<T>(Uri.EscapeUriString($"{basePath}{path}"));
        }

        public Task<T> GetNotStandardAPIAsync<T>(Uri path)
        {
            return GetNotStandardAPIAsync<T>(path.ToString());
        }

        public Task<T> GetNotStandardAPIAsync<T>(string path)
        {
            return GetInternal<T>(Uri.EscapeUriString(path));
        }


        private async Task<T> GetInternal<T>(string path, bool retry = false)
        {
            try
            {
                await RefreshToken();
                if (retry && PixivSession != null) await PixivSession.RefreshAsync();

                ResetAccessToken();
                return await client.GetJsonAsync<T>(path);
            }
            catch (RefreshFailedException)
            {
                Trace.TraceWarning($"Token refresh failed.");
                throw new ReLoginRequiredException();
            }
            catch (HttpRequestException e)
            {
                if (!retry) return await GetInternal<T>(path, true);

                Trace.TraceWarning($"Http request failed. Path=[{path}]");
                Trace.TraceWarning($"\tException details: {e}");
                throw;
            }
        }

        public Task<T> PostAsync<T>(Uri path, HttpForm data, string basePath = "https://app-api.pixiv.net/v1")
        {
            return PostAsync<T>(path.ToString(), data, basePath);
        }

        public Task<T> PostAsync<T>(string path, HttpForm data, string basePath = "https://app-api.pixiv.net/v1")
        {
            return PostInternal<T>(Uri.EscapeUriString($"{basePath}{path}"), data);
        }

        public Task PostAsync(string path, HttpForm data, string basePath = "https://app-api.pixiv.net/v1")
        {
            return PostInternal(Uri.EscapeUriString($"{basePath}{path}"), data);
        }

        private async Task PostInternal(string path, HttpForm data, bool retry = false)
        {
            try
            {
                await RefreshToken();
                if (retry && PixivSession != null) await PixivSession.RefreshAsync();

                ResetAccessToken();
                await client.PostAsync(path, new FormUrlEncodedContent(data));
            }
            catch (RefreshFailedException)
            {
                Trace.TraceWarning($"Token refresh failed.");
                throw new ReLoginRequiredException();
            }
            catch (HttpRequestException e)
            {
                if (!retry)
                {
                    await PostInternal(path, data, true);
                    return;
                }

                Trace.TraceWarning($"Http request failed. Path=[{path}]");
                Trace.TraceWarning($"\tException details: {e}");
                throw;
            }
        }

        private async Task<T> PostInternal<T>(string path, HttpForm data, bool retry = false)
        {
            try
            {
                await RefreshToken();
                if (retry && PixivSession != null) await PixivSession.RefreshAsync();

                ResetAccessToken();
                return await client.PostJsonAsync<T>(path, new FormUrlEncodedContent(data));
            }
            catch (RefreshFailedException)
            {
                Trace.TraceWarning($"Token refresh failed.");
                throw new ReLoginRequiredException();
            }
            catch (HttpRequestException e)
            {
                if (!retry) return await PostInternal<T>(path, data, true);

                Trace.TraceWarning($"Http request failed. Path=[{path}]");
                Trace.TraceWarning($"\tException details: {e}");
                throw;
            }
        }

        private void ResetAccessToken()
        {
            if (PixivSession != null) client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {PixivSession.AccessToken}");
        }
    }
}
