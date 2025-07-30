using System;
using RecipeMobileApp.Models;
using RecipeMobileApp.ViewModels;
using Xamarin.Forms;

namespace RecipeMobileApp.Views
{
    public partial class ProfilePage : ContentPage
    {
        private readonly ProfileViewModel viewModel;

        public ProfilePage(User user)
        {
            InitializeComponent();
            viewModel = new ProfileViewModel(user);
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = viewModel.InitAsync();
        }

        // ❤️ Favorite clicked → Open RecipeDetail
        private async void OnFavoriteTapped(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0) return;

            var recipe = e.CurrentSelection[0] as Recipe;
            if (recipe != null)
                await Navigation.PushAsync(new RecipeDetailPage(recipe, viewModel.User));

            ((CollectionView)sender).SelectedItem = null;
        }

        // ✅ New: My Recipes clicked → Open RecipeDetail
        private async void OnUserRecipeTapped(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0) return;

            var recipe = e.CurrentSelection[0] as Recipe;
            if (recipe != null)
                await Navigation.PushAsync(new RecipeDetailPage(recipe, viewModel.User));

            ((CollectionView)sender).SelectedItem = null;
        }

        // 💾 Save Profile button
        private async void OnSaveProfileClicked(object sender, EventArgs e)
        {
            bool saved = await viewModel.SaveProfileAsync();
            if (saved)
                await DisplayAlert("Success", "Profile saved successfully.", "OK");
            else
                await DisplayAlert("Error", "Username is required.", "OK");
        }

        private async void OnEditRecipe(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var recipe = (Recipe)btn.CommandParameter;
            await Navigation.PushAsync(new AddEditUserRecipePage(recipe, viewModel.User));
        }

        private async void OnDeleteRecipe(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var recipe = (Recipe)btn.CommandParameter;

            bool confirm = await DisplayAlert("Confirm Delete", $"Delete recipe '{recipe.Name}'?", "Yes", "No");
            if (confirm)
            {
                await viewModel.DeleteRecipeAsync(recipe);
            }
        }
    }
}
