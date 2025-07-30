using RecipeMobileApp.Models;
using RecipeMobileApp.ViewModels;
using Xamarin.Forms;

namespace RecipeMobileApp.Views
{
    public partial class AddEditUserRecipePage : ContentPage
    {
        private readonly AddEditUserRecipeViewModel viewModel;

        public AddEditUserRecipePage(Recipe recipe, User user)
        {
            InitializeComponent();
            viewModel = new AddEditUserRecipeViewModel(recipe, user);
            BindingContext = viewModel;
        }

        private async void OnSaveClicked(object sender, System.EventArgs e)
        {
            bool success = await viewModel.SaveRecipeAsync();
            if (success)
            {
                await DisplayAlert("Success", "Recipe saved.", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Failed to save recipe.", "OK");
            }
        }
    }
}
