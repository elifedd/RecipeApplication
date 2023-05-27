using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;

namespace RecipeApp.Pages.Recipe
{
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
            if (Recipe.UserId == userId)
            { 
                var recipe = await _context.Recipes.FindAsync(id);

                if (recipe != null)
                {
                    Recipe = recipe;
                    _context.Recipes.Remove(Recipe);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
