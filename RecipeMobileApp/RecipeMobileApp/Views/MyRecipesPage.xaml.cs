using System;
using Xamarin.Forms;
using RecipeMobileApp.Models;
using RecipeMobileApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeMobileApp.Views
{
    public partial class MyRecipesPage : ContentPage
    {
        private readonly DataService _db = new DataService();
        private User _user;

        public MyRecipesPage(User user)
        {
            InitializeComponent();
            _user = user;
            LoadMyRecipes();
        }

        async void LoadMyRecipes()
        {
            var my = await _db.GetUserRecipes(_user.Id);
            RecipeList.ItemsSource = my;
        }

        async void OnView(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var recipe = (Recipe)btn.CommandParameter;
            await Navigation.PushAsync(new UserRecipeDetailPage(recipe, _user));
        }
    }
}
