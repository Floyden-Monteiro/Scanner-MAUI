using System.Collections.ObjectModel;
using System.Windows.Input;
using scannermaui.Models;
using Microsoft.Maui.Storage;
using System.ComponentModel;

namespace scannermaui.ViewModels
{
    public class CartViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CartItem> CartItems { get; private set; }
        public ICommand RemoveFromCartCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CartViewModel()
        {
            CartItems = LoadCartItems();
            RemoveFromCartCommand = new Command<CartItem>(RemoveFromCart);
        }

        public void AddToCart(string productName)
        {
            var item = CartItems.FirstOrDefault(i => i.ProductName == productName);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                CartItems.Add(new CartItem { ProductName = productName, Quantity = 1 });
            }

            SaveCartItems();
        }

        private async void RemoveFromCart(CartItem item)
        {
            if (item == null) return;

            bool isConfirmed = await Shell.Current.DisplayAlert(
                "Confirm Delete",
                $"Are you sure you want to remove {item.ProductName}?",
                "Yes", "No");

            if (isConfirmed)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    CartItems.Remove(item);
                    OnPropertyChanged(nameof(CartItems));
                });

                SaveCartItems();
            }
        }

        private async void SaveCartItems()
        {
            try
            {
                await SecureStorage.SetAsync("CartItems",
                    System.Text.Json.JsonSerializer.Serialize(CartItems));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error",
                    "Failed to save cart items", "OK");
            }
        }

        private ObservableCollection<CartItem> LoadCartItems()
        {
            try
            {
                var cartItemsJson = SecureStorage.GetAsync("CartItems").Result;
                return string.IsNullOrEmpty(cartItemsJson)
                    ? new ObservableCollection<CartItem>()
                    : System.Text.Json.JsonSerializer.Deserialize<ObservableCollection<CartItem>>(cartItemsJson);
            }
            catch
            {
                return new ObservableCollection<CartItem>();
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}