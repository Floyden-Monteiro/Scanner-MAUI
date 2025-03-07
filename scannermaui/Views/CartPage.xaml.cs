using Microsoft.Maui.Controls;
using scannermaui.ViewModels;
using Microsoft.Maui.Storage;

namespace scannermaui.Views
{
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
            BindingContext = App.CartViewModel;
        }

        private async void OnLogoutClicked(object sender, System.EventArgs e)
        {
            bool confirm = await DisplayAlert("Logout", "Are you sure you want to log out?", "Yes", "No");
            if (confirm)
            {
                Preferences.Set("IsLoggedIn", false);
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}