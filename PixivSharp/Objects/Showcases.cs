using System;
using System.Collections.Generic;
using System.Text;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using R = Newtonsoft.Json.Required;
using N = Newtonsoft.Json.NullValueHandling;

namespace PixivSharp.Objects
{
    internal class 你不需要管这是啥玩意啊你只需要调用下面的Body
    {
        [J("error")] public bool Error { get; set; }
        [J("message")] public string Message { get; set; }
        [J("body")] public ShowcaseData[] Body { get; set; }
    }

    public class ShowcaseData
    {
        [J("id")] public string Id { get; set; }
        [J("lang")] public string Lang { get; set; }
        [J("entry")] public Entry Entry { get; set; }
        [J("tags")] public TagShowcase[] Tags { get; set; }
        [J("thumbnailUrl")] public Uri ThumbnailUrl { get; set; }
        [J("title")] public string Title { get; set; }
        [J("publishDate")] public long PublishDate { get; set; }
        [J("category")] public string Category { get; set; }
        [J("subCategory")] public string SubCategory { get; set; }
        [J("subCategoryLabel")] public string SubCategoryLabel { get; set; }
        [J("subCategoryIntroduction")] public string SubCategoryIntroduction { get; set; }
        [J("introduction")] public string Introduction { get; set; }
        [J("footer")] public string Footer { get; set; }
        [J("illusts")] public IllustShowcase[] Illusts { get; set; }
        [J("relatedArticles")] public RelatedArticle[] RelatedArticles { get; set; }
        [J("followingUserIds")] public object[] FollowingUserIds { get; set; }
        [J("isOnlyOneUser")] public bool IsOnlyOneUser { get; set; }
    }

    public partial class Entry
    {
        [J("id")] public string Id { get; set; }
        [J("title")] public string Title { get; set; }
        [J("pure_title")] public string PureTitle { get; set; }
        [J("catchphrase")] public string Catchphrase { get; set; }
        [J("header")] public string Header { get; set; }
        [J("body")] public string Body { get; set; }
        [J("footer")] public string Footer { get; set; }
        [J("sidebar")] public string Sidebar { get; set; }
        [J("publish_date")] public long PublishDate { get; set; }
        [J("language")] public string Language { get; set; }
        [J("big_category")] public BigCategory BigCategory { get; set; }
        [J("pixivision_category_slug")] public string PixivisionCategorySlug { get; set; }
        [J("pixivision_category")] public PixivisionCategory PixivisionCategory { get; set; }
        [J("pixivision_subcategory_slug")] public string PixivisionSubcategorySlug { get; set; }
        [J("pixivision_subcategory")] public PixivisionSubcategory PixivisionSubcategory { get; set; }
        [J("tags")] public TagShowcase[] Tags { get; set; }
        [J("article_url")] public Uri ArticleUrl { get; set; }
        [J("intro")] public string Intro { get; set; }
        [J("facebook_count")] public string FacebookCount { get; set; }
        [J("twitter_count")] public string TwitterCount { get; set; }
    }

    public class BigCategory
    {
        [J("slug")] public string Slug { get; set; }
        [J("name")] public string Name { get; set; }
        [J("exclude")] public string[] Exclude { get; set; }
    }

    public class PixivisionCategory
    {
        [J("label")] public string Label { get; set; }
        [J("introduction")] public string Introduction { get; set; }
    }

    public partial class PixivisionSubcategory
    {
        [J("label")] public string Label { get; set; }
        [J("label_en")] public string LabelEn { get; set; }
        [J("title")] public string Title { get; set; }
        [J("introduction")] public string Introduction { get; set; }
        [J("image_url")] public string ImageUrl { get; set; }
        [J("big_image_url")] public string BigImageUrl { get; set; }
    }

    public partial class TagShowcase
    {
        [J("id")] public string Id { get; set; }
        [J("name")] public string Name { get; set; }
    }

    public partial class IllustShowcase
    {
        [J("spotlight_article_id")] public long SpotlightArticleId { get; set; }
        [J("illust_id")] public long IllustId { get; set; }
        [J("description")] public string Description { get; set; }
        [J("language")] public string Language { get; set; }
        [J("illust_user_id")] public string IllustUserId { get; set; }
        [J("illust_title")] public string IllustTitle { get; set; }
        [J("illust_ext")] public string IllustExt { get; set; }
        [J("illust_width")] public string IllustWidth { get; set; }
        [J("illust_height")] public string IllustHeight { get; set; }
        [J("illust_restrict")] public string IllustRestrict { get; set; }
        [J("illust_x_restrict")] public string IllustXRestrict { get; set; }
        [J("illust_create_date")] public string IllustCreateDate { get; set; }
        [J("illust_upload_date")] public string IllustUploadDate { get; set; }
        [J("illust_server_id")] public string IllustServerId { get; set; }
        [J("illust_hash")] public object IllustHash { get; set; }
        [J("illust_type")] public string IllustType { get; set; }
        [J("illust_sanity_level")] public long IllustSanityLevel { get; set; }
        [J("illust_book_style")] public string IllustBookStyle { get; set; }
        [J("illust_page_count")] public string IllustPageCount { get; set; }
        [J("illust_comment")] public string IllustComment { get; set; }
        [J("user_account")] public string UserAccount { get; set; }
        [J("user_name")] public string UserName { get; set; }
        [J("user_comment")] public string UserComment { get; set; }
        [J("url")] public Url Url { get; set; }
        [J("ugoira_meta")] public object UgoiraMeta { get; set; }
        [J("user_icon")] public Uri UserIcon { get; set; }
    }

    public class Url
    {
        [J("1200x1200")] public Uri The1200X1200 { get; set; }
        [J("768x1200")] public Uri The768X1200 { get; set; }
        [J("ugoira600x600")] public string Ugoira600X600 { get; set; }
        [J("ugoira1920x1080")] public string Ugoira1920X1080 { get; set; }
    }

    public class RelatedArticle
    {
        [J("id")] public string Id { get; set; }
        [J("ja")] public En Ja { get; set; }
        [J("en")] public En En { get; set; }
        [J("zh")] public En Zh { get; set; }
        [J("zh_tw")] public En ZhTw { get; set; }
        [J("facebook_count")] public string FacebookCount { get; set; }
        [J("tweet_count")] public string TweetCount { get; set; }
        [J("tweet_max_count")] public string TweetMaxCount { get; set; }
        [J("publish_date")] public long PublishDate { get; set; }
        [J("category")] public object Category { get; set; }
        [J("pixivision_category_slug")] public string PixivisionCategorySlug { get; set; }
        [J("pixivision_subcategory_slug")] public string PixivisionSubcategorySlug { get; set; }
        [J("thumbnail")] public object Thumbnail { get; set; }
        [J("thumbnail_illust_id")] public string ThumbnailIllustId { get; set; }
        [J("has_body")] public string HasBody { get; set; }
        [J("is_pr")] public bool IsPr { get; set; }
        [J("edit_status")] public string EditStatus { get; set; }
        [J("translation_status")] public string TranslationStatus { get; set; }
        [J("is_sample")] public bool IsSample { get; set; }
        [J("pr_client_name")] public object PrClientName { get; set; }
        [J("illusts")] public object[] Illusts { get; set; }
        [J("big_category_slug")] public string BigCategorySlug { get; set; }
        [J("memo")] public string Memo { get; set; }
        [J("tags")] public object[] Tags { get; set; }
        [J("tag_ids")] public object TagIds { get; set; }
        [J("numbered_tags")] public object[] NumberedTags { get; set; }
        [J("main_abtest_pattern_id")] public string MainAbtestPatternId { get; set; }
        [J("advertisement_id")] public string AdvertisementId { get; set; }
    }

    public class En
    {
        [J("lang")] public string Lang { get; set; }
        [J("title")] public string Title { get; set; }
        [J("catchphrase")] public string Catchphrase { get; set; }
        [J("pure_title")] public string PureTitle { get; set; }
        [J("header")] public string Header { get; set; }
        [J("footer")] public string Footer { get; set; }
        [J("body")] public string Body { get; set; }
        [J("sidebar")] public string Sidebar { get; set; }
        [J("pixivision_subcategory")] public PixivisionSubcategory PixivisionSubcategory { get; set; }
        [J("writing_status")] public string WritingStatus { get; set; }
    }
}
