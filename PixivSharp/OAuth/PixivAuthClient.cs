using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GammaLibrary.Enhancements;
using GammaLibrary.Extensions;
using PixivSharp.Exceptions;
using PixivSharp.Objects;

namespace PixivSharp.OAuth
{
    public static class PixivAuthClient
    {
        private static readonly HttpClientEx client;
        public static IWebProxy Proxy
        {
            get => client.Proxy;
            set => client.Proxy = value;
        }

        static PixivAuthClient()
        {
            client = new HttpClientEx();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("PixivAndroidApp/5.0.64 (Android 6.0)");
            client.Timeout = TimeSpan.FromSeconds(4);
        }

        public static async Task<Session> AuthAsync(string userName, string password)
        {
            try
            {
                var body = new HttpForm
                {
                    { "username", userName },
                    { "password", password },
                    { "grant_type", "password" },
                    { "client_id", PixivConstants.ClientID },
                    { "client_secret", PixivConstants.ClientSecret }
                };
                var result = (await client.PostJsonAsync<OAuthResult>(PixivURLs.OAuth, body)).Result;
                return new Session(result.AccessToken, result.RefreshToken, DateTime.Now, TimeSpan.FromSeconds(result.ExpiresIn), long.Parse(result.User.ID));
            }
            catch (HttpRequestException e)
            {
                throw new AuthFailedException(e.Message);
            }
        }

        public static async Task RefreshAsync(this Session session)
        {
            try
            {
                var body = new HttpForm
                {
                    { "refresh_token", session.RefreshToken },
                    { "grant_type", "refresh_token" },
                    { "client_id", PixivConstants.ClientID },
                    { "client_secret", PixivConstants.ClientSecret }
                };
                var result = (await client.PostJsonAsync<OAuthResult>(PixivURLs.OAuth, body)).Result;
                
                session.LastRefreshTime = DateTime.Now;
                session.AccessToken = result.AccessToken;
                session.RefreshToken = result.RefreshToken;
            }
            catch (HttpRequestException)
            {
                throw new RefreshFailedException();
            }
        }
    }
}
