using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;

namespace TwitterAPIWinforms.Interfaces
{
    public interface ITwitterAuthenticationService
    {
        TwitterClient GetTwitterClient();
        Task<IAuthenticatedUser> GetAuthenticatedUserAsync();
    }
}
