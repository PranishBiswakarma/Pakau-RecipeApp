using System;
using Xamarin.Forms;
using RecipeMobileApp.Models;
using RecipeMobileApp.ViewModels;

namespace RecipeMobileApp.Views
{
    public partial class RecipeListPage : ContentPage
    {
        private RecipeViewModel viewModel;
        private string cuisineFilter;
        private string category;

        public RecipeListPage(string cuisine, string category)
        {
            InitializeComponent();
            cuisineFilter = cuisine;
            this.category = category;
            viewModel = new RecipeViewModel(); // FIXED: No argument
            BindingContext = viewModel;
        }

        private async void OnAddRecipeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEditRecipePage(viewModel, cuisineFilter, category));
        }

        private async void OnEditRecipeClicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var recipe = btn?.CommandParameter as Recipe;
            if (recipe != null)
                await Navigation.PushAsync(new AddEditRecipePage(viewModel, cuisineFilter, category, recipe));
        }

        private void OnDeleteRecipeClicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var recipe = btn?.CommandParameter as Recipe;
            if (recipe != null)
            {
                viewModel.DeleteRecipe(recipe);
            }
        }

        private async void OnRecipeSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedRecipe = e.CurrentSelection[0] as Recipe;
            if (selectedRecipe != null)
            {
                await Navigation.PushAsync(new RecipeDetailPage(selectedRecipe));
                RecipeCollection.SelectedItem = null;
            }
        }
    }
}
