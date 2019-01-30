using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using R = Newtonsoft.Json.Required;
using N = Newtonsoft.Json.NullValueHandling;

namespace PixivSharp.Objects
{
    public struct IllustID
    {
        public long ID { get; }

        public IllustID(long id)
        {
            ID = id;
        }

        public static implicit operator long(IllustID id)
        {
            return id.ID;
        }

        public static implicit operator IllustID(long id)
        {
            return new IllustID(id);
        }

        public static implicit operator string(IllustID id)
        {
            return id.ToString();
        }

        public override string ToString()
        {
            return ID.ToString();
        }

        public static IllustID Of(long id) => id;
    }

    public class IllustData
    {
        [J("illust")] public Illust Illust { get; set; }
    }

    public class Illust
    {
        [J("id")] public IllustID Id { get; set; }
        [J("title")] public string Title { get; set; }
        [J("type"), JsonConverter(typeof(StringEnumConverter))] public IllustType Type { get; set; }
        [J("image_urls")] public ImageUrls ImageUrls { get; set; }
        [J("caption")] public string Caption { get; set; }
        [J("restrict")] public long Restrict { get; set; }
        [J("user")] public User User { get; set; }
        [J("tags")] public Tag[] Tags { get; set; }
        [J("tools")] public string[] Tools { get; set; }
        [J("create_date")] public string CreateDate { get; set; }
        [J("page_count")] public long PageCount { get; set; }
        [J("width")] public long Width { get; set; }
        [J("height")] public long Height { get; set; }
        [J("sanity_level")] public long SanityLevel { get; set; }
        [J("x_restrict")] public long XRestrict { get; set; }
        [J("series")] public object Series { get; set; }
        [J("meta_single_page")] public MetaSinglePage MetaSinglePage { get; set; }
        [J("meta_pages")] public MetaPage[] MetaPages { get; set; }
        [J("total_view")] public long TotalView { get; set; }
        [J("total_bookmarks")] public long TotalBookmarks { get; set; }
        [J("is_bookmarked")] public bool IsBookmarked { get; set; }
        [J("visible")] public bool Visible { get; set; }
        [J("is_muted")] public bool IsMuted { get; set; }
        [J("total_comments")] public long TotalComments { get; set; }
    }

    public enum IllustType
    {
        Illust,
        Ugoira
    }

    public class UgoiraData
    {
        [J("ugoira_metadata")] public UgoiraMetadata Metadata { get; set; }
    }

    public class UgoiraMetadata
    {
        [J("zip_urls")] public ZipUrls ZipUrls { get; set; }
        [J("frames")] public Frame[] Frames { get; set; }
    }

    public class Frame
    {
        [J("file")] public string File { get; set; }
        [J("delay")] public int Delay { get; set; }
    }

    public class ZipUrls
    {
        [J("medium")] public Uri Medium { get; set; }
    }

    public class RecommendIllustsPage : IPage<RecommendIllustsPage>
    {
        [J("illusts")] public Illust[] Illusts { get; set; }
        [J("ranking_illusts", NullValueHandling = N.Ignore)] public Illust[] RankingIllusts { get; set; }
        [J("contest_exists")] public bool ContestExists { get; set; }
        [J("privacy_policy")] public PrivacyPolicy PrivacyPolicy { get; set; }
        [J("next_url")] public Uri NextUrl { get; set; }
    }

    public partial class BookmarkDetail
    {
        [J("is_bookmarked")] public bool IsBookmarked { get; set; }
        [J("tags")] public Tag[] Tags { get; set; }
        [J("restrict")] public string Restrict { get; set; }
    }

    public class BookmarkTagsPage : IPage<BookmarkTagsPage>
    {
        [J("bookmark_tags")] public BookmarkTag[] BookmarkTags { get; set; }
        [J("next_url")] public Uri NextUrl { get; set; }
    }

    public class BookmarkTag
    {
        [J("name")] public string Name { get; set; }
        [J("count")] public long Count { get; set; }
    }

    public class TrendTags
    {
        [J("trend_tags")] public TrendTag[] Tags { get; set; }
    }

    public class TrendTag
    {
        [J("tag")] public string Tag { get; set; }
        [J("illust")] public Illust Illust { get; set; }
    }

    public class CommentPage : IPage<CommentPage>
    {
        [J("total_comments")] public long TotalComments { get; set; }
        [J("comments")] public Comment[] Comments { get; set; }
        [J("next_url")] public Uri NextUrl { get; set; }
    }

    public class Comment
    {
        [J("id", NullValueHandling = N.Ignore)] public long? Id { get; set; }
        [J("comment", NullValueHandling = N.Ignore)] public string CommentComment { get; set; }
        [J("date", NullValueHandling = N.Ignore)] public string Date { get; set; }
        [J("user", NullValueHandling = N.Ignore)] public User User { get; set; }
        [J("parent_comment", NullValueHandling = N.Ignore)] public Comment ParentComment { get; set; }
    }

    public class ImageUrls
    {
        [J("square_medium")] public Uri SquareMedium { get; set; }
        [J("medium")] public Uri Medium { get; set; }
        [J("large")] public Uri Large { get; set; }
        [J("original", NullValueHandling = N.Ignore)] public Uri Original { get; set; }
    }

    public class MetaPage
    {
        [J("image_urls")] public ImageUrls ImageUrls { get; set; }
    }

    public class MetaSinglePage
    {
        [J("original_image_url", NullValueHandling = N.Ignore)] public Uri OriginalImageUrl { get; set; }
    }

    public class Tag
    {
        [J("name")] public string Name { get; set; }
    }
}
