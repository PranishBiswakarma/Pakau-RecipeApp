using System;
using Xamarin.Forms;
using RecipeMobileApp.Models;

namespace RecipeMobileApp.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly User currentUser;

        public MainPage(User user)
        {
            InitializeComponent();
            currentUser = user;
        }

        private async void OnCategoryClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is string category)
            {
                await Navigation.PushAsync(new RecipeListPage(currentUser.Id, category, currentUser));
            }
        }

        private async void OnProfileClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage(currentUser));
        }

        // ✅ New: Handle Add Recipe Button Click
        private async void OnAddRecipeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEditUserRecipePage(null, currentUser));
        }
    }
}
