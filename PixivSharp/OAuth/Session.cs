using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PixivSharp.Objects;

namespace PixivSharp.OAuth
{
    public class Session
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime LastRefreshTime { get; set; }
        public TimeSpan ExpiresIn { get; set; }
        public UserID ID { get; set; }

        public Session(string accessToken, string refreshToken, DateTime lastRefreshTime, TimeSpan expiresIn, UserID id)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            LastRefreshTime = lastRefreshTime;
            ExpiresIn = expiresIn;
            ID = id;
        }

        /// <summary>
        /// WARNING: This constructor is only for testing.
        /// </summary>
        /// <param name="accessToken"></param>
        public Session(string accessToken)
        {
            AccessToken = accessToken;
            RefreshToken = "F5";
            LastRefreshTime = DateTime.MinValue;
            ExpiresIn = TimeSpan.FromDays(10000 * 365);
            /*
             * 曾经, 有一份真挚的爱情摆在我面前. 我没有珍惜,
             * 等到我失去的时候才后悔莫及,
             * 人世间最痛苦的事莫过于此......
             * 如果上天能够给我一个再来一次的机会,
             * 我会对那个女孩子说三个字:
             * "我爱你。"
             * 如果非要在这份爱上加一个期限,
             * 我希望是.....一万年！
             */
        }
    }

    public static class SessionExtensions
    {
        public static bool Expired(this Session session)
        {
            var expireTime = session.LastRefreshTime + session.ExpiresIn;
            return expireTime < DateTime.Now;
        }

        public static async Task RefreshIfRequired(this Session session)
        {
            if (session.Expired()) await session.RefreshAsync();
        }
    }
}
