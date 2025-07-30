using RecipeMobileApp.Models;
using RecipeMobileApp.ViewModels;
using System;
using Xamarin.Forms;

namespace RecipeMobileApp.Views
{
    public partial class RecipeListPage : ContentPage
    {
        private readonly RecipesViewModel viewModel;
        private readonly User currentUser;
        private readonly string currentCategory;

        public RecipeListPage(int userId, string category, User user, bool showVeg = true)
        {
            InitializeComponent();

            currentUser = user;
            currentCategory = category;

            viewModel = new RecipesViewModel(userId)
            {
                SelectedCategory = category,
                ShowVeg = showVeg
            };

            BindingContext = viewModel;

            _ = viewModel.LoadRecipesAsync();
            _ = viewModel.LoadFavoriteRecipesAsync();
            _ = viewModel.LoadUserRecipesAsync();
        }

        // ✅ Filtered page navigation
        private async void OnVegClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecipeListPage(currentUser.Id, currentCategory, currentUser, true));
        }

        private async void OnNonVegClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecipeListPage(currentUser.Id, currentCategory, currentUser, false));
        }

        // ✅ 🔧 FIXED: Selection handler for recipe tap
        private async void OnRecipeSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            var selectedRecipe = e.CurrentSelection[0] as Recipe;
            if (selectedRecipe == null)
                return;

            await Navigation.PushAsync(new RecipeDetailPage(selectedRecipe, currentUser));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
