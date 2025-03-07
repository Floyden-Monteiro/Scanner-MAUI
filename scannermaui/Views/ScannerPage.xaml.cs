using Microsoft.Maui.Controls;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;
using scannermaui.ViewModels;

namespace scannermaui.Views
{
    public partial class ScannerPage : ContentPage
    {
        private readonly CartViewModel cartViewModel;

        public ScannerPage()
        {
            InitializeComponent();
            cartViewModel = App.CartViewModel;
            BindingContext = cartViewModel;
            StartScannerAnimation();
           
        }

        private async void OnScanButtonClicked(object sender, EventArgs e)
        {
            var cameraBarcodeReaderView = new CameraBarcodeReaderView();

            cameraBarcodeReaderView.BarcodesDetected += OnBarcodesDetected;

            var scanPage = new ContentPage
            {
                Content = cameraBarcodeReaderView
            };

            await Navigation.PushAsync(scanPage);
            cameraBarcodeReaderView.IsDetecting = true;
        }

        private async void OnBarcodesDetected(object sender, BarcodeDetectionEventArgs e)
        {
            if (e.Results?.Any() == true)
            {
                var result = e.Results.First();

                if (sender is CameraBarcodeReaderView camera)
                {
                    camera.IsDetecting = false;
                }

                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await Navigation.PopAsync();
                    cartViewModel.AddToCart(result.Value);
                    await Shell.Current.GoToAsync("//CartPage");
                });
            }
        }

        private async void StartScannerAnimation()
        {
            while (true)
            {
                await ScannerLight.TranslateTo(0, 100, 1800, Easing.SinInOut);
                await ScannerLight.TranslateTo(0, 10, 1800, Easing.SinInOut);
            }
        }

    }
}