using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace PixivSharp.Objects
{
    public class OAuthResult
    {
        [J("response")] public Response Result { get; set; }
    }

    public class Response
    {
        [J("access_token")] public string AccessToken { get; set; }
        [J("expires_in")] public long ExpiresIn { get; set; }
        [J("token_type")] public string TokenType { get; set; }
        [J("scope")] public string Scope { get; set; }
        [J("refresh_token")] public string RefreshToken { get; set; }
        [J("device_token")] public string DeviceToken { get; set; }
        [J("user")] public OAuthUser User { get; set; }
    }


    public class OAuthUser
    {
        [J("profile_image_urls")] public OAuthProfileImageUrls ProfileImageUrls { get; set; }
        [J("id")] public string ID { get; set; }
        [J("name")] public string Name { get; set; }
        [J("account")] public string Account { get; set; }
        [J("mail_address")] public string MailAddress { get; set; }
        [J("is_premium")] public bool IsPremium { get; set; }
        [J("x_restrict")] public long XRestrict { get; set; }
        [J("is_mail_authorized")] public bool IsMailAuthorized { get; set; }
    }

    public class OAuthProfileImageUrls
    {
        [J("px_16x16")] public Uri Px16X16 { get; set; }
        [J("px_50x50")] public Uri Px50X50 { get; set; }
        [J("px_170x170")] public Uri Px170X170 { get; set; }
    }
}
