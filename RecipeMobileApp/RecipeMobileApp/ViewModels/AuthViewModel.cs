using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using RecipeMobileApp.Models;
using RecipeMobileApp.Services;
using System.Threading.Tasks;

namespace RecipeMobileApp.ViewModels
{
    public class AuthViewModel : INotifyPropertyChanged
    {
        private readonly DataService _dataService = new DataService();

        private string username;
        public string Username
        {
            get => username;
            set
            {
                if (username == value) return;
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                if (password == value) return;
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public AuthViewModel()
        {
            LoginCommand = new Command(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Validation Error", "Please enter username and password.", "OK");
                return;
            }

            var user = await _dataService.ValidateUserAsync(Username, Password);

            if (user != null)
            {
                // Navigate to MainPage and set user context
                App.SetLogin(user);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login Failed", "Incorrect username or password.", "OK");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
