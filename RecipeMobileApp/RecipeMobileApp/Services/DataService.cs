using RecipeMobileApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeMobileApp.Services
{
    public class DataService
    {
        private readonly SQLiteAsyncConnection _db;

        public DataService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "recipes.db3");
            _db = new SQLiteAsyncConnection(dbPath);

            _db.CreateTableAsync<User>().Wait();
            _db.CreateTableAsync<Recipe>().Wait();
            _db.CreateTableAsync<Favorite>().Wait();
        }

        // ===== USERS =====

        public Task<List<User>> GetAllUsers() => _db.Table<User>().ToListAsync();

        public Task<User> ValidateUserAsync(string username, string password) =>
            _db.Table<User>().FirstOrDefaultAsync(x => x.Username == username && x.Password == password);

        public Task AddUserAsync(User user) => _db.InsertAsync(user);

        public Task UpdateUserAsync(User user) => _db.UpdateAsync(user);

        // ===== RECIPES =====

        public Task<List<Recipe>> GetRecipesAsync() => _db.Table<Recipe>().ToListAsync();

        public Task AddUserRecipeAsync(Recipe recipe) => _db.InsertAsync(recipe);

        public Task UpdateUserRecipeAsync(Recipe recipe) => _db.UpdateAsync(recipe);

        public Task<List<Recipe>> GetUserRecipesAsync(int userId) =>
            _db.Table<Recipe>().Where(r => r.CreatedByUserId == userId).ToListAsync();

        public Task<List<Recipe>> GetUserRecipes(int userId) => GetUserRecipesAsync(userId);

        public Task<int> DeleteRecipeAsync(Recipe recipe) => _db.DeleteAsync(recipe); // ✅ This is correct

        // ===== FAVORITES =====

        public Task<List<Favorite>> GetFavoritesAsync(int userId) =>
            _db.Table<Favorite>().Where(f => f.UserId == userId).ToListAsync();

        public async Task ToggleFavoriteAsync(int userId, int recipeId)
        {
            var existing = await _db.Table<Favorite>()
                .FirstOrDefaultAsync(f => f.UserId == userId && f.RecipeId == recipeId);

            if (existing != null)
                await _db.DeleteAsync(existing);
            else
                await _db.InsertAsync(new Favorite { UserId = userId, RecipeId = recipeId });
        }

        public async Task<List<Recipe>> GetFavoriteRecipesAsync(int userId)
        {
            var favorites = await _db.Table<Favorite>()
                .Where(f => f.UserId == userId)
                .ToListAsync();

            var recipeIds = favorites.Select(f => f.RecipeId).ToList();
            var allRecipes = await _db.Table<Recipe>().ToListAsync();

            return allRecipes.Where(r => recipeIds.Contains(r.Id)).ToList();
        }

        public Task<List<Recipe>> GetFavoriteRecipes(int userId) => GetFavoriteRecipesAsync(userId);
    }
}
