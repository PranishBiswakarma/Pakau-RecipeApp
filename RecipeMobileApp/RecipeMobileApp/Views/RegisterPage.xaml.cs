using RecipeMobileApp.Models;
using RecipeMobileApp.Services;
using Xamarin.Forms;

namespace RecipeMobileApp.Views
{
    public partial class RegisterPage : ContentPage
    {
        private readonly DataService _dataService = new DataService();

        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterClicked(object sender, System.EventArgs e)
        {
            var username = UsernameEntry.Text?.Trim();
            var password = PasswordEntry.Text;
            var confirm = ConfirmPasswordEntry.Text;

            var name = NameEntry?.Text?.Trim();
            var bio = BioEditor?.Text?.Trim();
            var hobbies = HobbiesEntry?.Text?.Trim();
            string profileImage = "defaultprofile.png"; // Or get this from an Entry or file picker

            ErrorLabel.IsVisible = false;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirm))
            {
                ErrorLabel.Text = "Username & Password required.";
                ErrorLabel.IsVisible = true;
                return;
            }

            if (password != confirm)
            {
                ErrorLabel.Text = "Passwords do not match.";
                ErrorLabel.IsVisible = true;
                return;
            }

            var existing = await _dataService.ValidateUserAsync(username, password);
            if (existing != null)
            {
                ErrorLabel.Text = "User already exists.";
                ErrorLabel.IsVisible = true;
                return;
            }

            var newUser = new User
            {
                Username = username,
                Password = password,
                Name = name,
                Bio = bio,
                Hobbies = hobbies,
                ProfileImageUrl = profileImage
            };

            await _dataService.AddUserAsync(newUser);

            await DisplayAlert("Success", "Registration complete!", "OK");
            await Navigation.PopAsync(); // Go back to LoginPage
        }

        private async void OnCancelClicked(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
