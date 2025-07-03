using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text?.Trim();
            string password = PasswordEntry.Text;

            if (UserStore.ValidateUser(username, password))
            {
                // Navigate to MainPage after successful login
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                LoginMessage.Text = "Invalid username or password.";
                LoginMessage.IsVisible = true;
            }
        }

        private async void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Forgot Password", "Feature coming soon!", "OK");
        }

        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CreateAccountPage());
        }
    }
}
