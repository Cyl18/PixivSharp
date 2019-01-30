using System;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using R = Newtonsoft.Json.Required;
using N = Newtonsoft.Json.NullValueHandling;

namespace PixivSharp.Objects
{
    public struct UserID
    {
        public long ID { get; }

        public UserID(long id)
        {
            ID = id;
        }

        public static implicit operator UserID(long id)
        {
            return new UserID(id);
        }

        public static implicit operator long(UserID id)
        {
            return id.ID;
        }

        public static implicit operator string(UserID id)
        {
            return id.ToString();
        }

        public override string ToString()
        {
            return ID.ToString();
        }

        public static UserID Of(long id) => id;
    }

    public class UserDetail // TODO Open Browser
    {
        [J("user")] public User User { get; set; }
        [J("profile")] public Profile Profile { get; set; }
        [J("profile_publicity")] public ProfilePublicity ProfilePublicity { get; set; }
        [J("workspace")] public Workspace Workspace { get; set; }

        public static implicit operator UserID(UserDetail id)
        {
            return id.User.Id;
        }
    }

    public class IllustsPage : IPage<IllustsPage>
    {
        [J("illusts")] public Illust[] Illusts { get; set; }
        [J("next_url")] public Uri NextUrl { get; set; }
    }

    public struct Profile
    {
        [J("webpage")] public Uri Webpage { get; set; }
        [J("gender")] public string Gender { get; set; }
        [J("birth")] public string Birth { get; set; }
        [J("birth_day")] public string BirthDay { get; set; }
        [J("birth_year")] public long BirthYear { get; set; }
        [J("region")] public string Region { get; set; }
        [J("address_id")] public long AddressId { get; set; }
        [J("country_code")] public string CountryCode { get; set; }
        [J("job")] public string Job { get; set; }
        [J("job_id")] public long JobId { get; set; }
        [J("total_follow_users")] public long TotalFollowUsers { get; set; }
        [J("total_mypixiv_users")] public long TotalMyPixivUsers { get; set; }
        [J("total_illusts")] public long TotalIllusts { get; set; }
        [J("total_manga")] public long TotalManga { get; set; }
        [J("total_novels")] public long TotalNovels { get; set; }
        [J("total_illust_bookmarks_public")] public long TotalIllustBookmarksPublic { get; set; }
        [J("total_illust_series")] public long TotalIllustSeries { get; set; }
        [J("background_image_url")] public Uri BackgroundImageUrl { get; set; }
        [J("twitter_account")] public string TwitterAccount { get; set; }
        [J("twitter_url")] public Uri TwitterUrl { get; set; }
        [J("pawoo_url")] public Uri PawooUrl { get; set; }
        [J("is_premium")] public bool IsPremium { get; set; }
        [J("is_using_custom_profile_image")] public bool IsUsingCustomProfileImage { get; set; }
    }

    public class ProfilePublicity
    {
        [J("gender")] public string Gender { get; set; }
        [J("region")] public string Region { get; set; }
        [J("birth_day")] public string BirthDay { get; set; }
        [J("birth_year")] public string BirthYear { get; set; }
        [J("job")] public string Job { get; set; }
        [J("pawoo")] public bool Pawoo { get; set; }
    }

    public class User
    {
        [J("id")] public UserID Id { get; set; }
        [J("name")] public string Name { get; set; }
        [J("account")] public string Account { get; set; }
        [J("profile_image_urls")] public ProfileImageUrls ProfileImageUrls { get; set; }
        [J("comment", NullValueHandling = N.Ignore)] public string Comment { get; set; }
        [J("is_followed", NullValueHandling = N.Ignore)] public bool IsFollowed { get; set; }
    }

    public class UserFollowPage : IPage<UserFollowPage>
    {
        [J("user_previews")] public UserPreview[] UserPreviews { get; set; }
        [J("next_url")] public Uri NextUrl { get; set; }
    }

    public class UserPreview
    {
        [J("user")] public User User { get; set; }
        [J("illusts")] public Illust[] Illusts { get; set; }
        [J("novels")] public Novel[] Novels { get; set; }
        [J("is_muted")] public bool IsMuted { get; set; }
    }

    public class Novel
    {
        [J("id")] public long Id { get; set; }
        [J("title")] public string Title { get; set; }
        [J("caption")] public string Caption { get; set; }
        [J("restrict")] public long Restrict { get; set; }
        [J("x_restrict")] public long XRestrict { get; set; }
        [J("image_urls")] public ImageUrls ImageUrls { get; set; }
        [J("create_date")] public string CreateDate { get; set; }
        [J("tags")] public NovelTag[] Tags { get; set; }
        [J("page_count")] public long PageCount { get; set; }
        [J("text_length")] public long TextLength { get; set; }
        [J("user")] public User User { get; set; }
        [J("series")] public Series Series { get; set; }
        [J("is_bookmarked")] public bool IsBookmarked { get; set; }
        [J("total_bookmarks")] public long TotalBookmarks { get; set; }
        [J("total_view")] public long TotalView { get; set; }
        [J("visible")] public bool Visible { get; set; }
        [J("total_comments")] public long TotalComments { get; set; }
        [J("is_muted")] public bool IsMuted { get; set; }
    }
    public class NovelTag
    {
        [J("name")] public string Name { get; set; }
        [J("added_by_uploaded_user")] public bool AddedByUploadedUser { get; set; }
    }

    public class Series
    {
        //TODO WTF
    }

    public class ProfileImageUrls
    {
        [J("medium")] public Uri Medium { get; set; }
    }

    public class Workspace
    {
        [J("pc")] public string PC { get; set; }
        [J("monitor")] public string Monitor { get; set; }
        [J("tool")] public string Tool { get; set; }
        [J("scanner")] public string Scanner { get; set; }
        [J("tablet")] public string Tablet { get; set; }
        [J("mouse")] public string Mouse { get; set; }
        [J("printer")] public string Printer { get; set; }
        [J("desktop")] public string Desktop { get; set; }
        [J("music")] public string Music { get; set; }
        [J("desk")] public string Desk { get; set; }
        [J("chair")] public string Chair { get; set; }
        [J("comment")] public string Comment { get; set; }
        [J("workspace_image_url")] public string WorkspaceImageUrl { get; set; }
    }

    public class PrivacyPolicy
    {
        [J("version")] public string Version { get; set; }
        [J("message")] public string Message { get; set; }
        [J("url")] public Uri Url { get; set; }
    }
}
