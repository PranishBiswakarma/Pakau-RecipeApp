using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccountPage : ContentPage
    {
        public CreateAccountPage()
        {
            InitializeComponent();
        }

        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            string username = NewUsernameEntry.Text?.Trim();
            string password = NewPasswordEntry.Text;
            string phone = PhoneEntry.Text?.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(phone))
            {
                AccountMessage.Text = "All fields are required.";
                AccountMessage.TextColor = Color.Red;
                AccountMessage.IsVisible = true;
                return;
            }

            if (UserStore.UserExists(username))
            {
                AccountMessage.Text = "Username already exists.";
                AccountMessage.TextColor = Color.Red;
                AccountMessage.IsVisible = true;
                return;
            }

            UserStore.AddUser(username, password, phone);
            AccountMessage.Text = "Account created! Please login.";
            AccountMessage.TextColor = Color.Green;
            AccountMessage.IsVisible = true;
        }

        private async void OnBackToLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
