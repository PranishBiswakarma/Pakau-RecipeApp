using System;
using System.Linq;
using Xamarin.Forms;
using RecipeMobileApp.Models;
using RecipeMobileApp.ViewModels;

namespace RecipeMobileApp
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            viewModel = new MainPageViewModel();
            BindingContext = viewModel;
            CuisinePicker.SelectedIndex = -1;
        }

        private async void OnCuisineChanged(object sender, EventArgs e)
        {
            var selectedCuisine = CuisinePicker.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedCuisine))
            {
                var cuisine = viewModel.CuisineCategories.FirstOrDefault(c => c.Name == selectedCuisine);
                if (cuisine != null)
                {
                    await Navigation.PushAsync(new RecipeListPage(cuisine.Name, cuisine.ImageUrl));
                }
            }
        }

        private async void OnCuisineImageSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedCuisine = e.CurrentSelection.FirstOrDefault() as CuisineCategory;
            if (selectedCuisine != null)
            {
                await Navigation.PushAsync(new RecipeListPage(selectedCuisine.Name, selectedCuisine.ImageUrl));
                CuisineCollection.SelectedItem = null;
            }
        }
    }
}

