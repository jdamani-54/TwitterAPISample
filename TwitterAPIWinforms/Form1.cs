using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tweetinvi;
using Tweetinvi.Models;
using System.Configuration;
using TwitterAPIWinforms.Models;
using TwitterAPIWinforms.Interfaces;

namespace TwitterAPIWinforms
{
    public partial class Form1 : Form
    {
        private string APIKey = ConfigurationManager.AppSettings["APIKey"];
        private string APISecretKey = ConfigurationManager.AppSettings["APISecretKey"];
        private string AccessTocken = ConfigurationManager.AppSettings["AccessTocken"];
        private string AccessTockenSecret = ConfigurationManager.AppSettings["AccessTockenSecret"];
        private TwitterClient client;
        private IAuthenticatedUser user;
        private ITwitterAuthenticationService twitterAuthenticationService;
        private ITweetReadService tweetReadService;
        public Form1()
        {
            InitializeComponent();
            this.twitterAuthenticationService = (ITwitterAuthenticationService)Program.ServiceProvider.GetService(typeof(ITwitterAuthenticationService));
            tweetReadService = (ITweetReadService)Program.ServiceProvider.GetService(typeof(ITweetReadService));
            client = twitterAuthenticationService.GetTwitterClient();            
            ClearMessages();
        }

        private async void Form1_LoadAsync(object sender, EventArgs e)
        {
            await ShowUserProfile();
            await ShowTweetCounts();
        }
        private async Task ShowTweetCounts()
        {
            try
            {
                var timeline = await tweetReadService.GetTimeLineAsync();

                AppendMessage(String.Format("1. Total tweets received {0}.", (tweetReadService.GetTotalTweets(timeline))));

                AppendMessage(String.Format("2. Average tweets per hour is {0}.", (tweetReadService.GetAverageTweetsPerHour(timeline))));

                AppendMessage(String.Format("3. Average tweets per minute is {0}.", (tweetReadService.GetAverageTweetsPerMinute(timeline))));

                AppendMessage(String.Format("4. Average tweets per second is {0}.", (tweetReadService.GetAverageTweetsPerSeconds(timeline))));

                AppendMessage(String.Format("5. Percent of tweets that contains emojis {0}%.", (tweetReadService.GetPercentageOfTweetsWithEmojis(timeline))));

                var topUsedEmojis = tweetReadService.GetTopUsedEmojis(timeline);
                AppendMessage(String.Format("6. Top used emoji is {0}. Count is {1}.", topUsedEmojis.Emoji, topUsedEmojis.Count));

                var topUsedHashTag = tweetReadService.GetTopUsedHashTag(timeline);
                AppendMessage(String.Format("7. Top hashtag is #{0}. Count is {1}.", topUsedHashTag.HashTag, topUsedHashTag.Count));

                decimal UrlPerc = tweetReadService.GetPercentageOfTweetContainsURL(timeline);
                AppendMessage(String.Format("8. Percentage of tweets contains a URL is {0}%", UrlPerc));

                decimal ImageUrlPerc = tweetReadService.GetPercentageOfTweetContainsPicURL(timeline);
                AppendMessage(String.Format("9. Percentage of tweets contains image is {0}%", ImageUrlPerc));

                var topUsedDomain = tweetReadService.GetTopDomainsInUrl(timeline);
                AppendMessage(String.Format("10. Top used domain is '{0}'. Number of times used is {1}.", topUsedDomain.Domain, topUsedDomain.Count));
            }
            catch (WebException ex)
            {
                if (ex.Response != null && ((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
                {
                    AppendMessage(((HttpWebResponse)ex.Response).StatusDescription);
                }
                else
                {
                    AppendMessage(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }
            catch (Exception ex)
            {
                AppendMessage(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }

        }
        private async Task ShowUserProfile()
        {
            try
            {
                var user = await twitterAuthenticationService.GetAuthenticatedUserAsync();
                if (user != null)
                {
                    pictureBox1.ImageLocation = user.ProfileImageUrlFullSize;
                    this.user = user;
                    lblName.Text = user.Name;
                    lblScreenName.Text = user.ScreenName;
                }
            }
            catch(WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
                {
                    AppendMessage(((HttpWebResponse)ex.Response).StatusDescription);
                }
            }
            catch (Exception ex)
            {
                AppendMessage(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        private void ClearMessages()
        {
            textBox1.Text = string.Empty;
        }
        private void AppendMessage(string message)
        {
            textBox1.Text = textBox1.Text + message + "\r\n";
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearMessages();
            await ShowTweetCounts();
        }
    }
}
