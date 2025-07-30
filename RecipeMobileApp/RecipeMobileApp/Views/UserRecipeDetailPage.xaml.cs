using RecipeMobileApp.Models;
using Xamarin.Forms;

namespace RecipeMobileApp.Views
{
    public partial class UserRecipeDetailPage : ContentPage
    {
        private readonly Recipe _recipe;
        private readonly User _user;

        public UserRecipeDetailPage(Recipe recipe, User user)
        {
            InitializeComponent();
            _recipe = recipe;
            _user = user;
            BindingContext = recipe;
        }

        private async void OnEditClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AddEditUserRecipePage(_recipe, _user));
        }

        private async void OnDeleteClicked(object sender, System.EventArgs e)
        {
            var confirm = await DisplayAlert("Confirm Delete", "Are you sure?", "Yes", "No");
            if (confirm)
            {
                var db = new Services.DataService();
                await db.DeleteRecipeAsync(_recipe);
                await DisplayAlert("Deleted", "Recipe removed.", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}
