using SocialLoginMaratona.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialLoginMaratona.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public string UserId { get; private set; }
        public string Token { get; private set; }

        public MainViewModel()
        {
            UserId = Settings.UserId;
            Token = Settings.AuthToken;

            System.Diagnostics.Debug.WriteLine(UserId+" "+ Token);
        }
    }
}
