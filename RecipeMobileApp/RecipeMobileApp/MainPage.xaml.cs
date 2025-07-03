using System.Linq;
using Xamarin.Forms;
using RecipeMobileApp.Models;
using RecipeMobileApp.ViewModels;
using RecipeMobileApp.Views;

namespace RecipeMobileApp
{
    public partial class MainPage : ContentPage
    {
        private RecipeViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            viewModel = new RecipeViewModel();
            BindingContext = viewModel;

            // Safe: Only set ItemsSource if not null
            if (viewModel.Cuisines != null)
                CuisinePicker.ItemsSource = viewModel.Cuisines.ToList();

            CuisinePicker.SelectedIndex = 0;
            UpdateCategoryGrid();
        }

        private void CuisinePicker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateCategoryGrid();
        }

        private void UpdateCategoryGrid()
        {
            string selectedCuisine = CuisinePicker.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedCuisine) && viewModel.CuisineCategories != null)
            {
                var filtered = viewModel.CuisineCategories
                    .Where(c => c.Cuisine == selectedCuisine)
                    .ToList();

                CategoryGrid.ItemsSource = filtered;
            }
            else
            {
                CategoryGrid.ItemsSource = null;
            }
        }

        private async void CategoryGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CuisinePicker.SelectedIndex == -1 || e.CurrentSelection.Count == 0)
                return;

            string selectedCuisine = CuisinePicker.SelectedItem as string;
            var selectedCategory = (e.CurrentSelection[0] as CuisineCategory)?.Category;

            await Navigation.PushAsync(new RecipeGridPage(viewModel, selectedCuisine, selectedCategory));
            CategoryGrid.SelectedItem = null;
        }
    }
}
