using Android.Content.Res;
using SocialLoginMaratona.Helpers;
using SocialLoginMaratona.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SocialLoginMaratona.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        //INavigation navigation;

        AzureService azureService;

        public Command LoginCommand { get; }

        private bool _isBusy;

        public string Title { get; private set; }


        public LoginViewModel()
        {
            Settings.AuthToken = string.Empty;
            Settings.UserId = string.Empty;

            azureService = DependencyService.Get<AzureService>();

            LoginCommand = new Command(async () => await ExecuteLoginCommandAsync());

            Title = "Social Login Demo";
        }

        private async Task ExecuteLoginCommandAsync()
        {
            if (_isBusy || !(await LogInAsync()))
                return;
            else
            {
                //await navigation.PushAsync(new LoginPage());

                await PushAsync<MainViewModel>();

                //RemovePageFromStack();
            }
            _isBusy = false;

        }

        public Task<bool> LogInAsync()
        {
            _isBusy = true;

            if (Settings.IsLoggedIn)
                return Task.FromResult(true);

            return azureService.LogInAsync();
        }

        //private void RemovePageFromStack()
        //{
        //    var existingPages = navigation.NavigationStack.ToList();

        //    foreach (var page in existingPages)
        //    {
        //        if (page.GetType() == typeof(LoginPage))
        //            navigation.RemovePage(page);
        //    }
        //}
    }
}
