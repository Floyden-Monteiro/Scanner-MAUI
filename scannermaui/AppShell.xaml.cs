using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using scannermaui.Views;
using System;

namespace scannermaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");

            if (confirm)
            {
                Preferences.Set("IsLoggedIn", false);
                Preferences.Remove("UserToken");
                Preferences.Remove("DisplayName");

                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}
