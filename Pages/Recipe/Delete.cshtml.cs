using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;

namespace RecipeApp.Pages.Recipe
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly RecipeApp.Models.RecipeAppContext _context;

        public DeleteModel(RecipeApp.Models.RecipeAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Recipe Recipe { get; set; } = default!;
        public string userId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.RecipeId == id);

            if (recipe == null)
            {
                return NotFound();
            }
            else
            {
                Recipe = recipe;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.RecipeId == id);
            if (recipe == null)
            {
                return NotFound();
            }
            else
            {
                Recipe = recipe;
            }

            if (Recipe.UserId == userId)
            {
                var ratings = _context.Ratings.Where(r => r.RecipeId == id);
                var comments = _context.Comments.Where(r => r.RecipeId == id);
                var ingredients = _context.Ingredients.Where(r => r.RecipeId == id);
                var directions = _context.Directions.Where(r => r.RecipeId == id);
                _context.Ratings.RemoveRange(ratings);
                _context.Comments.RemoveRange(comments);
                _context.Ingredients.RemoveRange(ingredients);
                _context.Directions.RemoveRange(directions);

                // Add additional code to delete related records from other tables if needed

                _context.Recipes.Remove(Recipe);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

    }
}
