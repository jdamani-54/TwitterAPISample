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
        Task<List<TweetResults>> GetTweetResultsAsync();
    }
}
