using Microsoft.Maui.Controls;
using scannermaui.ViewModels;
using Microsoft.Maui.Storage;

namespace scannermaui.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}