using System.Linq;
using ZXing.Net.Maui;
using System.Diagnostics;

namespace scannermaui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        private void barcodeReader_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
        {
            Debug.WriteLine("Barcode event fired!"); 

            if (e.Results?.Any() == true)
            {
                string barcodeValue = e.Results.First().Value;
                string barcodeFormat = e.Results.First().Format.ToString();

                Dispatcher.Dispatch(async () =>
                {
                    await DisplayAlert("Barcode Scanned", $"Value: {barcodeValue}\nFormat: {barcodeFormat}", "OK");
                    barcodeResult.Text = $"{barcodeValue} ({barcodeFormat})";
                });
            }
        }
    }
}