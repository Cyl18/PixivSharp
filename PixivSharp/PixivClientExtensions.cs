using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GammaLibrary.Extensions;
using PixivSharp.Exceptions;
using PixivSharp.Objects;

namespace PixivSharp
{
    public static class PixivClientExtensions
    {
        public static Task<UserDetail> GetUserDetailAsync(this PixivClient client, UserID id)
        {
            return client.GetAsync<UserDetail>("/v1/user/detail", new HttpForm { { "user_id", id } });
        }

        public static Task<T> GetNextPageAsync<T>(this PixivClient client, IPage<T> page) where T : IPage<T>
        {
            if (page.NextUrl == null) throw new EndOfPageException();
            return client.GetNotStandardAPIAsync<T>(page.NextUrl);
        }

        public static Task<IllustsPage> GetUserIllustsAsync(this PixivClient client, UserID id, int offset = 0)
        {
            return client.GetAsync<IllustsPage>("/v1/user/illusts", new HttpForm { { "user_id", id }, { "offset", offset.ToString() } });
        }

        public static Task<IllustsPage> GetUserBookmarksAsync(this PixivClient client, UserID id, Restrict restrict = Restrict.Public)
        {
            return client.GetAsync<IllustsPage>("/v1/user/bookmarks/illust", new HttpForm { { "user_id", id }, { "restrict", restrict.GetSymbol() } });
        }

        public static Task<IllustsPage> GetFollowedUsersIllustAsync(this PixivClient client, Restrict restrict = Restrict.Public, int offset = 0)
        {
            client.RequireAuth();
            return client.GetAsync<IllustsPage>("/v2/illust/follow", new HttpForm { { "restrict", restrict.GetSymbol() }, { "offset", offset.ToString() } });
        }

        public static Task<IllustsPage> GetIllustRankingAsync(this PixivClient client, RankMode mode = RankMode.Day, DateTime? date = default, int offset = 0)
        {
            var data = new HttpForm { { "offset", offset.ToString() }, { "mode", mode.GetSymbol() } };
            if (date != null) data.Add("date", date.Value.ToString("yyyy-MM-dd"));

            return client.GetAsync<IllustsPage>("/v1/illust/ranking", data);
        }

        public static Task<IllustData> GetIllustDetailAsync(this PixivClient client, IllustID id)
        {
            return client.GetAsync<IllustData>("/v1/illust/detail", new HttpForm { { "illust_id", id } });
        }

        public static Task<BookmarkDetail> GetIllustBookmarkDetailAsync(this PixivClient client, IllustID id)
        {
            return client.GetAsync<BookmarkDetail>("/v2/illust/bookmark/detail", new HttpForm { { "illust_id", id } });
        }

        public static Task<CommentPage> GetIllustCommentsAsync(this PixivClient client, IllustID id, int offset = 0, bool includeTotalComments = true)
        {
            return client.GetAsync<CommentPage>("/v1/illust/comments", new HttpForm
            {
                { "illust_id", id },
                { "offset", offset.ToString() },
                { "include_total_comments", includeTotalComments.ToString().ToLower() }
            });
        }

        public static Task<IllustsPage> GetIllustRelatedAsync(this PixivClient client, IllustID id)
        {
            return client.GetAsync<IllustsPage>("/v2/illust/related", new HttpForm { { "user_id", id } });
        }

        public static Task<RecommendIllustsPage> GetIllustRecommendAsync(this PixivClient client, ContentType type = ContentType.Illust, bool includeRankingIllusts = true)
        {
            client.RequireAuth();
            var url = "/v1/illust/recommended";
            return client.GetAsync<RecommendIllustsPage>(url, new HttpForm
            {
                { "content_type", type.GetSymbol() },
                { "include_ranking_illusts", includeRankingIllusts.ToString().ToLower() },
                { "include_ranking_label", "true" } // 我就是不给参数 你能奈我何?
            });
        }

        public static Task<TrendTags> GetTrendingTagsAsync(this PixivClient client)
        {
            return client.GetAsync<TrendTags>("/v1/trending-tags/illust");
        }

        public static Task<IllustsPage> SearchIllustAsync(this PixivClient client, string word, SearchMode mode = SearchMode.PartialMatchTags, SortOptions sortOptions = SortOptions.DateDecreasing, SearchDuration? duration = default, int offset = 0)
        {
            var data = new HttpForm { { "word", word }, { "search_target", mode.GetSymbol() }, { "sort", sortOptions.GetSymbol() }, { "offset", offset.ToString() } };
            if (duration != null) data.Add("duration", duration.GetSymbol());

            return client.GetAsync<IllustsPage>("/v1/search/illust");
        }

        // TODO support tag
        public static Task AddIllustBookmarkAsync(this PixivClient client, IllustID id, Restrict restrict = Restrict.Public)
        {
            client.RequireAuth();
            return client.PostAsync("/v2/illust/bookmark/add", new HttpForm { { "illust_id", id }, { "restrict", restrict.GetSymbol() } });
        }

        public static Task DeleteIllustBookmarkAsync(this PixivClient client, IllustID id)
        {
            client.RequireAuth();
            return client.PostAsync("/v1/illust/bookmark/delete", new HttpForm { { "illust_id", id } });
        }

        public static Task<BookmarkTagsPage> GetUserBookmarkTagsAsync(this PixivClient client, Restrict restrict = Restrict.Public)
        {
            client.RequireAuth();
            return client.GetAsync<BookmarkTagsPage>("/v1/user/bookmark-tags/illust", new HttpForm { { "restrict", restrict.GetSymbol() } });
        }

        public static Task<UserFollowPage> GetUserFollowingsAsync(this PixivClient client, UserID id, Restrict restrict = Restrict.Public)
        {
            return client.GetAsync<UserFollowPage>("/v1/user/following", new HttpForm { { "user_id", id }, { "restrict", restrict.GetSymbol() } });
        }

        public static Task<UserFollowPage> GetUserFollowersAsync(this PixivClient client, UserID id, Restrict restrict = Restrict.Public)
        {
            return client.GetAsync<UserFollowPage>("/v1/user/follower", new HttpForm { { "user_id", id }, { "restrict", restrict.GetSymbol() } });
        }

        public static Task<UgoiraData> GetUgoiraMetadataAsync(this PixivClient client, IllustID id)
        {
            return client.GetAsync<UgoiraData>("/v1/ugoira/metadata", new HttpForm { { "illust_id", id } });
        }

        public async static Task<ShowcaseData[]> GetShowcaseDataAsync(this PixivClient client, long articleId)
        {
            return (await client.GetNotStandardAPIAsync<你不需要管这是啥玩意啊你只需要调用下面的Body>($"https://www.pixiv.net/ajax/showcase/article?article_id={articleId}")).Body;
        }
    }

    public enum RankMode
    {
        [Symbol("day")] Day,
        [Symbol("week")] Week,
        [Symbol("month")] Month,
        [Symbol("day_male")] DayMale,
        [Symbol("day_female")] DayFemale,
        [Symbol("week_original")] WeekOriginal,
        [Symbol("week_rookie")] WeekRookie,
        [Symbol("day_r18")] R18Day,
        [Symbol("day_male_r18")] R18DayMale,
        [Symbol("day_female_r18")] R18DayFemale,
        [Symbol("week_r18")] WeekR18,
        [Symbol("week_r18g")] WeekR18G
    }

    public enum Restrict
    {
        [Symbol("public")] Public,
        [Symbol("private")] Private
    }

    public enum ContentType
    {
        [Symbol("illust")] Illust,
        [Symbol("manga")] Manga
    }

    public enum SortOptions
    {
        [Symbol("date_desc")] DateDecreasing,
        [Symbol("date_asc")] DateIncreasing
    }

    public enum SearchMode
    {
        [Symbol("partial_match_for_tags")] PartialMatchTags,
        [Symbol("exact_match_for_tags")] ExactMatchTags,
        [Symbol("title_and_caption")] TitleAndCaption
    }

    public enum SearchDuration
    {
        [Symbol("within_last_day")] WithinLastDay,
        [Symbol("within_last_week")] WithinLastWeek,
        [Symbol("within_last_month")] WithinLastMonth,
    }
}
