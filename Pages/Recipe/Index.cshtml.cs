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
    public class IndexModel : PageModel
    {
        private readonly RecipeApp.Models.RecipeAppContext _context;

        public IndexModel(RecipeApp.Models.RecipeAppContext context)
        {
            _context = context;
        }

        public IList<Models.Recipe> Recipe { get;set; } = default!;

        public string userId { get; set; }

        public async Task OnGetAsync()
        {
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_context.Recipes != null)
            {
                Recipe = await _context.Recipes.ToListAsync();
            }
        }
    }
}
