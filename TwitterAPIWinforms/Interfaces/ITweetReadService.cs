using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Models;
using TwitterAPIWinforms.Models;

namespace TwitterAPIWinforms.Interfaces
{
    public interface ITweetReadService
    {
        Task<List<ITweet>> GetTimeLineAsync();
        int GetTotalTweets(List<ITweet> timeline);
        HashTags GetTopUsedHashTag(List<ITweet> timeline);
        decimal GetPercentageOfTweetContainsURL(List<ITweet> timeline);
        decimal GetPercentageOfTweetContainsPicURL(List<ITweet> timeline);
        Domains GetTopDomainsInUrl(List<ITweet> timeline);
        int GetAverageTweetsPerHour(List<ITweet> timeline);
        int GetAverageTweetsPerMinute(List<ITweet> timeline);
        int GetAverageTweetsPerSeconds(List<ITweet> timeline);
        decimal GetPercentageOfTweetsWithEmojis(List<ITweet> timeline);
        Emojis GetTopUsedEmojis(List<ITweet> timeline);
    }
}
