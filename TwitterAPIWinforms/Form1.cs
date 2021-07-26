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
        private ITwitterAuthenticationService twitterAuthenticationService;
        private ITweetReadService tweetReadService;
        public Form1()
        {
            InitializeComponent();
            this.twitterAuthenticationService = (ITwitterAuthenticationService)Program.ServiceProvider.GetService(typeof(ITwitterAuthenticationService));
            tweetReadService = (ITweetReadService)Program.ServiceProvider.GetService(typeof(ITweetReadService));       
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
                var timeLineResult = await tweetReadService.GetTweetResultsAsync();
                dataGridView1.DataSource = timeLineResult;
            }
            catch (WebException ex)
            {
                if (ex.Response != null && ((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
                {
                    ShowMessage(((HttpWebResponse)ex.Response).StatusDescription);
                }
                else
                {
                    ShowMessage(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
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
                    lblName.Text = user.Name;
                    lblScreenName.Text = user.ScreenName;
                }
            }
            catch(WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
                {
                    ShowMessage(((HttpWebResponse)ex.Response).StatusDescription);
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        private void ClearMessages()
        {
            dataGridView1.DataSource = null;
        }
        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearMessages();
            await ShowTweetCounts();
        }
    }
}
