using Microsoft.Maui;
using Microsoft.Maui.Controls;
using scannermaui.ViewModels;
using scannermaui.Views;

namespace scannermaui
{
    public partial class App : Application
    {
        public static CartViewModel CartViewModel { get; private set; }

        public App()
        {
            InitializeComponent();

        

            if (Preferences.Get("IsLoggedIn", false))
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }



    }
}