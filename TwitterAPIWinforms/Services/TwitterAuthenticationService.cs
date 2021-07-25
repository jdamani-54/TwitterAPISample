using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using TwitterAPIWinforms.Interfaces;

namespace TwitterAPIWinforms.Services
{    
    public class TwitterAuthenticationService : ITwitterAuthenticationService
    {
        private TwitterClient client;
        public TwitterAuthenticationService()
        {
            string APIKey = ConfigurationManager.AppSettings["APIKey"];
            string APISecretKey = ConfigurationManager.AppSettings["APISecretKey"];
            string AccessTocken = ConfigurationManager.AppSettings["AccessTocken"];
            string AccessTockenSecret = ConfigurationManager.AppSettings["AccessTockenSecret"];
            client = new TwitterClient(APIKey, APISecretKey, AccessTocken, AccessTockenSecret);
        }
        public TwitterClient GetTwitterClient()
        {
            return client;
        }
        public async Task<IAuthenticatedUser> GetAuthenticatedUserAsync()
        {
            return (await client.Users.GetAuthenticatedUserAsync());
        }
    }
}
