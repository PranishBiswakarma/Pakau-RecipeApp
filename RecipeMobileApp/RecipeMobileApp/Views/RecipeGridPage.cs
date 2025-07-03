using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using RecipeMobileApp.Models;
using RecipeMobileApp.ViewModels;

namespace RecipeMobileApp.Views
{
    public class RecipeGridPage : ContentPage
    {
        private RecipeViewModel viewModel;
        private string cuisine;
        private string category;
        private string currentType = "Veg"; // Default tab

        private CollectionView recipeCollection;
        private Button vegButton;
        private Button nonVegButton;

        public RecipeGridPage(RecipeViewModel vm, string cuisine, string category)
        {
            this.viewModel = vm;
            this.cuisine = cuisine;
            this.category = category;

            Title = $"{cuisine} - {category}";

            // Veg/Non-Veg toggle
            vegButton = new Button { Text = "Veg", BackgroundColor = Color.LightGreen, TextColor = Color.Black };
            nonVegButton = new Button { Text = "Non-Veg", BackgroundColor = Color.LightGray, TextColor = Color.Black };

            vegButton.Clicked += (s, e) => { currentType = "Veg"; UpdateGrid(); };
            nonVegButton.Clicked += (s, e) => { currentType = "Non-Veg"; UpdateGrid(); };

            var toggleLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Children = { vegButton, nonVegButton }
            };

            // Add button
            var addButton = new ToolbarItem { Text = "Add" };
            addButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new AddEditRecipePage(viewModel, cuisine, category, type: currentType));
            };
            ToolbarItems.Add(addButton);

            // Recipe grid
            recipeCollection = new CollectionView
            {
                ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Vertical),
                ItemTemplate = new DataTemplate(() =>
                {
                    var image = new Image { HeightRequest = 120, Aspect = Aspect.AspectFill };
                    image.SetBinding(Image.SourceProperty, "ImageUrl");

                    var name = new Label { FontSize = 16, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center };
                    name.SetBinding(Label.TextProperty, "Name");

                    var editButton = new Button { Text = "Edit", WidthRequest = 60 };
                    editButton.SetBinding(Button.CommandParameterProperty, ".");

                    var deleteButton = new Button { Text = "Delete", WidthRequest = 60 };
                    deleteButton.SetBinding(Button.CommandParameterProperty, ".");

                    editButton.Clicked += async (s, e) =>
                    {
                        var btn = s as Button;
                        var recipe = btn?.CommandParameter as Recipe;
                        if (recipe != null)
                            await Application.Current.MainPage.Navigation.PushAsync(
                                new AddEditRecipePage(viewModel, cuisine, category, recipe));
                    };

                    deleteButton.Clicked += (s, e) =>
                    {
                        var btn = s as Button;
                        var recipe = btn?.CommandParameter as Recipe;
                        if (recipe != null)
                        {
                            viewModel.DeleteRecipe(recipe);
                            UpdateGrid();
                        }
                    };

                    var stack = new StackLayout
                    {
                        Children = { image, name, new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                HorizontalOptions = LayoutOptions.Center,
                                Spacing = 10,
                                Children = { editButton, deleteButton }
                            }
                        }
                    };
                    return new Frame { Content = stack, Margin = 10, HasShadow = true };
                })
            };

            Content = new StackLayout
            {
                Children = { toggleLayout, recipeCollection }
            };

            UpdateGrid();
        }

        private void UpdateGrid()
        {
            vegButton.BackgroundColor = currentType == "Veg" ? Color.LightGreen : Color.LightGray;
            nonVegButton.BackgroundColor = currentType == "Non-Veg" ? Color.LightGreen : Color.LightGray;

            var filtered = viewModel.AllRecipes
                .Where(r => r.Cuisine == cuisine && r.Category == category && r.Type == currentType)
                .Take(4) // Show up to 4 dishes
                .ToList();

            while (filtered.Count < 4)
                filtered.Add(new Recipe { Name = "", ImageUrl = "https://via.placeholder.com/120", Type = currentType, Category = category, Cuisine = cuisine });

            recipeCollection.ItemsSource = new ObservableCollection<Recipe>(filtered);
        }
    }
}
