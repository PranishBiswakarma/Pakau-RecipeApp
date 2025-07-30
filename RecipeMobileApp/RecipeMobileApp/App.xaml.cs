using RecipeMobileApp.Models;
using RecipeMobileApp.Services;
using RecipeMobileApp.Views;
using Xamarin.Forms;
using System.Linq;


namespace RecipeMobileApp
{
    public partial class App : Application
    {
        public static User CurrentUser { get; private set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
            SeedSampleRecipes();
        }

        public static void SetLogin(User user)
        {
            CurrentUser = user;
            Current.MainPage = new NavigationPage(new MainPage(user));
        }
        private async void SeedSampleRecipes()
        {
            var db = new DataService();
            var existing = await db.GetRecipesAsync();

            if (!existing.Any())
            {
                var sampleUser = App.CurrentUser ?? new User { Id = 1, Username = "admin" };

                // Nepalese
                await db.AddUserRecipeAsync(new Recipe
                {
                    Name = "Dal Bhat",
                    Category = "Nepalese",
                    Ingredients = "Rice\nLentil soup\nSpinach\nPotatoes\nCurry\nPickle\nPapad",
                    Steps = "Cook rice\nPrepare dal (lentil soup)\nCook vegetables\nServe rice with dal and sides",
                    ImageUrl = "dalbhat.jpg",
                    IsVeg = true,
                    IsDefault = true,
                    CreatedByUserId = sampleUser.Id
                });

                await db.AddUserRecipeAsync(new Recipe
                {
                    Name = "Chicken Momo",
                    Category = "Nepalese",
                    Ingredients = "Flour\nChicken mince\nOnion\nGarlic\nGinger\nSalt\nPepper",
                    Steps = "Prepare dough\nMake chicken filling\nWrap momos\nSteam momos\nServe with chutney",
                    ImageUrl = "momo.jpg",
                    IsVeg = false,
                    IsDefault = true,
                    CreatedByUserId = sampleUser.Id
                });

                // Japanese
                await db.AddUserRecipeAsync(new Recipe
                {
                    Name = "Vegetarian Sushi",
                    Category = "Japanese",
                    Ingredients = "Sushi rice\nNori sheets\nAvocado\nCucumber\nCarrot",
                    Steps = "Prepare sushi rice\nSlice vegetables\nAssemble sushi rolls\nCut and serve",
                    ImageUrl = "vegsushi.jpg",
                    IsVeg = true,
                    IsDefault = true,
                    CreatedByUserId = sampleUser.Id
                });

                await db.AddUserRecipeAsync(new Recipe
                {
                    Name = "Salmon Sushi",
                    Category = "Japanese",
                    Ingredients = "Sushi rice\nNori sheets\nFresh salmon\nSoy sauce\nWasabi",
                    Steps = "Prepare rice\nSlice salmon\nAssemble nigiri\nServe with soy sauce and wasabi",
                    ImageUrl = "salmon_sushi.jpg",
                    IsVeg = false,
                    IsDefault = true,
                    CreatedByUserId = sampleUser.Id
                });

                // Chinese
                await db.AddUserRecipeAsync(new Recipe
                {
                    Name = "Vegetable Fried Rice",
                    Category = "Chinese",
                    Ingredients = "Rice\nCarrots\nPeas\nCorn\nSoy sauce\nScallions\nSesame oil",
                    Steps = "Cook rice\nStir fry vegetables\nAdd rice and seasonings\nToss and serve",
                    ImageUrl = "rice.jpg",
                    IsVeg = true,
                    IsDefault = true,
                    CreatedByUserId = sampleUser.Id
                });

                await db.AddUserRecipeAsync(new Recipe
                {
                    Name = "Chicken Noodle Soup",
                    Category = "Chinese",
                    Ingredients = "Chicken\nNoodles\nScallions\nBok choy\nCarrots\nGinger\nBroth",
                    Steps = "Boil chicken\nPrepare broth\nCook noodles with veggies\nCombine and serve",
                    ImageUrl = "soup.jpg",
                    IsVeg = false,
                    IsDefault = true,
                    CreatedByUserId = sampleUser.Id
                });

                // Italian
                await db.AddUserRecipeAsync(new Recipe
                {
                    Name = "Vegetarian Pasta",
                    Category = "Italian",
                    Ingredients = "Pasta\nTomato sauce\nGarlic\nBasil\nParmesan cheese",
                    Steps = "Cook pasta\nPrepare tomato sauce\nCombine and toss\nServe with parmesan",
                    ImageUrl = "pasta.jpg",
                    IsVeg = true,
                    IsDefault = true,
                    CreatedByUserId = sampleUser.Id
                });

                await db.AddUserRecipeAsync(new Recipe
                {
                    Name = "Pepperoni Pizza",
                    Category = "Italian",
                    Ingredients = "Pizza dough\nTomato sauce\nMozzarella\nPepperoni\nOlive oil",
                    Steps = "Prepare dough\nAdd toppings\nBake in oven\nSlice and serve",
                    ImageUrl = "pizza.jpg",
                    IsVeg = false,
                    IsDefault = true,
                    CreatedByUserId = sampleUser.Id
                });

                System.Diagnostics.Debug.WriteLine("[Seed] Sample recipes added.");
            }
        }

    }
}
