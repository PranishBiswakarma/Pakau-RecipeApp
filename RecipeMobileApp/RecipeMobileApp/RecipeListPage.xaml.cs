using RecipeMobileApp.Models;
using RecipeMobileApp.ViewModels;
using System.Linq;
using Xamarin.Forms;

namespace RecipeMobileApp
{
    public partial class RecipeListPage : ContentPage
    {
        private RecipeViewModel viewModel;
        private string currentCuisine;

        public RecipeListPage(string cuisine, string cuisineImage)
        {
            InitializeComponent();
            viewModel = new RecipeViewModel();
            BindingContext = viewModel;
            currentCuisine = cuisine;
            CuisineLabel.Text = cuisine;
            CuisineImage.Source = cuisineImage;
            VegSwitch.IsToggled = false; // Default to Veg
            viewModel.FilterByCuisineAndType(currentCuisine, "Veg");
        }

        private void OnVegSwitchToggled(object sender, ToggledEventArgs e)
        {
            viewModel.FilterByCuisineAndType(currentCuisine, e.Value ? "NonVeg" : "Veg");
        }

        private async void OnRecipeSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedRecipe = e.CurrentSelection.FirstOrDefault() as Recipe;
            if (selectedRecipe != null)
            {
                await Navigation.PushAsync(new RecipeDetailPage(selectedRecipe));
                RecipeCollection.SelectedItem = null;
            }
        }
    }
}
