using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tweetinvi.Models;
using TwitterAPIWinforms.Interfaces;
using TwitterAPIWinforms.Models;

namespace TwitterAPIWinforms.Services
{
    public class TweetReadService: ITweetReadService
    {        
        ITwitterAuthenticationService twitterAuthenticationService;
        const string regex = "(?:0\x20E3|1\x20E3|2\x20E3|3\x20E3|4\x20E3|5\x20E3|6\x20E3|7\x20E3|8\x20E3|9\x20E3|#\x20E3|\\*\x20E3|\xD83C(?:\xDDE6\xD83C(?:\xDDE8|\xDDE9|\xDDEA|\xDDEB|\xDDEC|\xDDEE|\xDDF1|\xDDF2|\xDDF4|\xDDF6|\xDDF7|\xDDF8|\xDDF9|\xDDFA|\xDDFC|\xDDFD|\xDDFF)|\xDDE7\xD83C(?:\xDDE6|\xDDE7|\xDDE9|\xDDEA|\xDDEB|\xDDEC|\xDDED|\xDDEE|\xDDEF|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF6|\xDDF7|\xDDF8|\xDDF9|\xDDFB|\xDDFC|\xDDFE|\xDDFF)|\xDDE8\xD83C(?:\xDDE6|\xDDE8|\xDDE9|\xDDEB|\xDDEC|\xDDED|\xDDEE|\xDDF0|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF5|\xDDF7|\xDDFA|\xDDFB|\xDDFC|\xDDFD|\xDDFE|\xDDFF)|\xDDE9\xD83C(?:\xDDEA|\xDDEC|\xDDEF|\xDDF0|\xDDF2|\xDDF4|\xDDFF)|\xDDEA\xD83C(?:\xDDE6|\xDDE8|\xDDEA|\xDDEC|\xDDED|\xDDF7|\xDDF8|\xDDF9|\xDDFA)|\xDDEB\xD83C(?:\xDDEE|\xDDEF|\xDDF0|\xDDF2|\xDDF4|\xDDF7)|\xDDEC\xD83C(?:\xDDE6|\xDDE7|\xDDE9|\xDDEA|\xDDEB|\xDDEC|\xDDED|\xDDEE|\xDDF1|\xDDF2|\xDDF3|\xDDF5|\xDDF6|\xDDF7|\xDDF8|\xDDF9|\xDDFA|\xDDFC|\xDDFE)|\xDDED\xD83C(?:\xDDF0|\xDDF2|\xDDF3|\xDDF7|\xDDF9|\xDDFA)|\xDDEE\xD83C(?:\xDDE8|\xDDE9|\xDDEA|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF6|\xDDF7|\xDDF8|\xDDF9)|\xDDEF\xD83C(?:\xDDEA|\xDDF2|\xDDF4|\xDDF5)|\xDDF0\xD83C(?:\xDDEA|\xDDEC|\xDDED|\xDDEE|\xDDF2|\xDDF3|\xDDF5|\xDDF7|\xDDFC|\xDDFE|\xDDFF)|\xDDF1\xD83C(?:\xDDE6|\xDDE7|\xDDE8|\xDDEE|\xDDF0|\xDDF7|\xDDF8|\xDDF9|\xDDFA|\xDDFB|\xDDFE)|\xDDF2\xD83C(?:\xDDE6|\xDDE8|\xDDE9|\xDDEA|\xDDEB|\xDDEC|\xDDED|\xDDF0|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF5|\xDDF6|\xDDF7|\xDDF8|\xDDF9|\xDDFA|\xDDFB|\xDDFC|\xDDFD|\xDDFE|\xDDFF)|\xDDF3\xD83C(?:\xDDE6|\xDDE8|\xDDEA|\xDDEB|\xDDEC|\xDDEE|\xDDF1|\xDDF4|\xDDF5|\xDDF7|\xDDFA|\xDDFF)|\xDDF4\xD83C\xDDF2|\xDDF5\xD83C(?:\xDDE6|\xDDEA|\xDDEB|\xDDEC|\xDDED|\xDDF0|\xDDF1|\xDDF2|\xDDF3|\xDDF7|\xDDF8|\xDDF9|\xDDFC|\xDDFE)|\xDDF6\xD83C\xDDE6|\xDDF7\xD83C(?:\xDDEA|\xDDF4|\xDDF8|\xDDFA|\xDDFC)|\xDDF8\xD83C(?:\xDDE6|\xDDE7|\xDDE8|\xDDE9|\xDDEA|\xDDEC|\xDDED|\xDDEE|\xDDEF|\xDDF0|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF7|\xDDF8|\xDDF9|\xDDFB|\xDDFD|\xDDFE|\xDDFF)|\xDDF9\xD83C(?:\xDDE6|\xDDE8|\xDDE9|\xDDEB|\xDDEC|\xDDED|\xDDEF|\xDDF0|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF7|\xDDF9|\xDDFB|\xDDFC|\xDDFF)|\xDDFA\xD83C(?:\xDDE6|\xDDEC|\xDDF2|\xDDF8|\xDDFE|\xDDFF)|\xDDFB\xD83C(?:\xDDE6|\xDDE8|\xDDEA|\xDDEC|\xDDEE|\xDDF3|\xDDFA)|\xDDFC\xD83C(?:\xDDEB|\xDDF8)|\xDDFD\xD83C\xDDF0|\xDDFE\xD83C(?:\xDDEA|\xDDF9)|\xDDFF\xD83C(?:\xDDE6|\xDDF2|\xDDFC)))|[\xA9\xAE\x203C\x2049\x2122\x2139\x2194-\x2199\x21A9\x21AA\x231A\x231B\x2328\x23CF\x23E9-\x23F3\x23F8-\x23FA\x24C2\x25AA\x25AB\x25B6\x25C0\x25FB-\x25FE\x2600-\x2604\x260E\x2611\x2614\x2615\x2618\x261D\x2620\x2622\x2623\x2626\x262A\x262E\x262F\x2638-\x263A\x2648-\x2653\x2660\x2663\x2665\x2666\x2668\x267B\x267F\x2692-\x2694\x2696\x2697\x2699\x269B\x269C\x26A0\x26A1\x26AA\x26AB\x26B0\x26B1\x26BD\x26BE\x26C4\x26C5\x26C8\x26CE\x26CF\x26D1\x26D3\x26D4\x26E9\x26EA\x26F0-\x26F5\x26F7-\x26FA\x26FD\x2702\x2705\x2708-\x270D\x270F\x2712\x2714\x2716\x271D\x2721\x2728\x2733\x2734\x2744\x2747\x274C\x274E\x2753-\x2755\x2757\x2763\x2764\x2795-\x2797\x27A1\x27B0\x27BF\x2934\x2935\x2B05-\x2B07\x2B1B\x2B1C\x2B50\x2B55\x3030\x303D\x3297\x3299]|\xD83C[\xDC04\xDCCF\xDD70\xDD71\xDD7E\xDD7F\xDD8E\xDD91-\xDD9A\xDE01\xDE02\xDE1A\xDE2F\xDE32-\xDE3A\xDE50\xDE51\xDF00-\xDF21\xDF24-\xDF93\xDF96\xDF97\xDF99-\xDF9B\xDF9E-\xDFF0\xDFF3-\xDFF5\xDFF7-\xDFFF]|\xD83D[\xDC00-\xDCFD\xDCFF-\xDD3D\xDD49-\xDD4E\xDD50-\xDD67\xDD6F\xDD70\xDD73-\xDD79\xDD87\xDD8A-\xDD8D\xDD90\xDD95\xDD96\xDDA5\xDDA8\xDDB1\xDDB2\xDDBC\xDDC2-\xDDC4\xDDD1-\xDDD3\xDDDC-\xDDDE\xDDE1\xDDE3\xDDEF\xDDF3\xDDFA-\xDE4F\xDE80-\xDEC5\xDECB-\xDED0\xDEE0-\xDEE5\xDEE9\xDEEB\xDEEC\xDEF0\xDEF3]|\xD83E[\xDD10-\xDD18\xDD80-\xDD84\xDDC0]";
        public TweetReadService(ITwitterAuthenticationService twitterAuthenticationService)
        {
            this.twitterAuthenticationService = twitterAuthenticationService;
        }
        public async Task<List<ITweet>> GetTimeLineAsync()
        {
            var client = twitterAuthenticationService.GetTwitterClient();
            return (await client.Timelines.GetHomeTimelineAsync()).ToList<ITweet>();
        }
        public async Task<List<TweetResults>> GetTweetResultsAsync()
        {
            var timeline = await GetTimeLineAsync();

            List<TweetResults> result = new List<TweetResults>();
            result.Add(new TweetResults()
            {
                SlNo = 1,
                ResultDescription = "Total tweets received",
                Result = GetTotalTweets(timeline).ToString()
            });

            result.Add(new TweetResults()
            {
                SlNo = 2,
                ResultDescription = "Average tweets per hour",
                Result = GetAverageTweetsPerHour(timeline).ToString()
            });

            result.Add(new TweetResults()
            {
                SlNo = 3,
                ResultDescription = "Average tweets per minute",
                Result = GetAverageTweetsPerMinute(timeline).ToString()
            });

            result.Add(new TweetResults()
            {
                SlNo = 4,
                ResultDescription = "Average tweets per second",
                Result = GetAverageTweetsPerSeconds(timeline).ToString()
            });

            result.Add(new TweetResults()
            {
                SlNo = 5,
                ResultDescription = "Percent of tweets that contains emojis",
                Result = GetPercentageOfTweetsWithEmojis(timeline).ToString()
            });

            result.Add(new TweetResults()
            {
                SlNo = 6,
                ResultDescription = "Top used emoji",
                Result = GetTopUsedEmojis(timeline).Emoji
            });

            result.Add(new TweetResults()
            {
                SlNo = 7,
                ResultDescription = "Top used hashtag",
                Result = "#" + GetTopUsedHashTag(timeline).HashTag
            });

            result.Add(new TweetResults()
            {
                SlNo = 8,
                ResultDescription = "Percentage of tweets contains a URL",
                Result = GetPercentageOfTweetContainsURL(timeline).ToString() + "%"
            });

            result.Add(new TweetResults()
            {
                SlNo = 9,
                ResultDescription = "Percentage of tweets contains image",
                Result = GetPercentageOfTweetContainsPicURL(timeline).ToString() + "%"
            });

            result.Add(new TweetResults()
            {
                SlNo = 10,
                ResultDescription = "Top used domain",
                Result = GetTopDomainsInUrl(timeline).Domain
            });

            return result;
        }
        private int GetTotalTweets(List<ITweet> timeline)
        {
            return timeline.Count;
        }
        private Emojis GetTopUsedEmojis(List<ITweet> timeline)
        {
            var tweetsWithImojis = timeline.Where(x => Regex.Match(x.FullText, regex).Length > 0);
            List<string> emojis = new List<string>();
            foreach (var tweets in tweetsWithImojis)
            {
                emojis.AddRange(Regex.Matches(tweets.FullText, regex).Cast<Match>().Select(m => m.Value).ToList());
            }
            var emojisGrouped = emojis.GroupBy(x => x).Select(s => new Emojis() { Emoji = s.Key, Count = s.Count() });
            return emojisGrouped.OrderByDescending(x => x.Count).FirstOrDefault();
        }
        private decimal GetPercentageOfTweetsWithEmojis(List<ITweet> timeline)
        {
            var tweetsWithImojis = timeline.Where(x => Regex.Match(x.FullText, regex).Length > 0);
            return System.Math.Round(100 * (Convert.ToDecimal(tweetsWithImojis.Count()) / Convert.ToDecimal(timeline.Count)), 2);
        }
        private HashTags GetTopUsedHashTag(List<ITweet> timeline)
        {
            var hashtags = timeline.SelectMany(x => x.Hashtags).GroupBy(g => g.Text).Select(c => new HashTags() { HashTag = c.Key, Count = c.Count() });
            return hashtags.OrderByDescending(x => x.Count).FirstOrDefault();
        }
        private decimal GetPercentageOfTweetContainsURL(List<ITweet> timeline)
        {
            var urlCounts = timeline.Where(x => x.Urls.Count > 0).ToList();
            return System.Math.Round(100 * (Convert.ToDecimal(urlCounts.Count) / Convert.ToDecimal(timeline.Count)), 2);
        }
        private decimal GetPercentageOfTweetContainsPicURL(List<ITweet> timeline)
        {
            var tweetsWithImages = timeline.SelectMany(x => x.Media).Where(x => x.MediaType == "photo").GroupBy(x => x.Id).ToList();
            return System.Math.Round(100 * (Convert.ToDecimal(tweetsWithImages.Count) / Convert.ToDecimal(timeline.Count)), 2);
        }
        private Domains GetTopDomainsInUrl(List<ITweet> timeline)
        {
            var domainsInTweets = timeline.SelectMany(x => x.Urls)
                .GroupBy(s => new Uri(s.ExpandedURL).Host)
                .Select(p => new Domains() { Domain = p.Key, Count = p.Count() });

            return domainsInTweets.OrderByDescending(x => x.Count).FirstOrDefault();
        }
        private int GetAverageTweetsPerHour(List<ITweet> timeline)
        {
            return timeline.Where(x => x.CreatedAt.DateTime >= DateTime.Now.AddHours(-1)).Count();
        }
        private int GetAverageTweetsPerMinute(List<ITweet> timeline)
        {
            return timeline.Where(x => x.CreatedAt.DateTime >= DateTime.Now.AddMinutes(-1)).Count();
        }
        private int GetAverageTweetsPerSeconds(List<ITweet> timeline)
        {
            return timeline.Where(x => x.CreatedAt.DateTime >= DateTime.Now.AddSeconds(-1)).Count();
        }
    }
}
