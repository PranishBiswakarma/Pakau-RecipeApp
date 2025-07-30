using System;
using Xamarin.Forms;
using RecipeMobileApp.Models;
using RecipeMobileApp.Services;
using System.Collections.Generic;

namespace RecipeMobileApp.Views
{
    public partial class FavoritesPage : ContentPage
    {
        private readonly DataService _data = new DataService();
        private readonly User _user;

        public FavoritesPage(User user)
        {
            InitializeComponent();
            _user = user;
            Load();
        }

        async void Load()
        {
            var favs = await _data.GetFavoriteRecipes(_user.Id);
            FavList.ItemsSource = favs;
        }

        async void OnView(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var recipe = (Recipe)btn.CommandParameter;
            await Navigation.PushAsync(new RecipeDetailPage(recipe, _user));
        }
    }
}
