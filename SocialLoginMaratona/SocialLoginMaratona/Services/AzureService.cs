using Microsoft.WindowsAzure.MobileServices;
using SocialLoginMaratona.Helpers;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialLoginMaratona.Autentication;

[assembly:Dependency(typeof(SocialLoginMaratona.Services.AzureService))]
namespace SocialLoginMaratona.Services
{
    public class AzureService
    {
        static readonly string AppUrl = "url";

        public MobileServiceClient Client { get; set; } = null;

        public static bool UseAuth { get; set; } = false;

        public void Initialize()
        {
            Client = new MobileServiceClient(AppUrl);

            if (!string.IsNullOrWhiteSpace(Settings.AuthToken) && !string.IsNullOrWhiteSpace(Settings.UserId))
            {
                Client.CurrentUser = new MobileServiceUser(Settings.UserId)
                {
                    MobileServiceAuthenticationToken = Settings.AuthToken
                };
            }  
        }

        public async Task<bool> LogInAsync()
        {
            Initialize();

            var auth = DependencyService.Get<IAuthentication>();
            var user = await auth.LoginAsync(Client, MobileServiceAuthenticationProvider.Facebook);

            if(user == null)
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Ops!", "Não conseguimos efetuar o seu login, tente novamente.", "Ok");
                });

                return false;
            }
            else
            {
                Settings.AuthToken = user.MobileServiceAuthenticationToken;
                Settings.UserId = user.UserId;
            }

            return true;
        }

    }
}
