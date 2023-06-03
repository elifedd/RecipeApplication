using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApp.Models;
using System.Security.Claims;
namespace RecipeApp.Pages
{
    [Authorize]
    public class RateModel : PageModel
    {
        private readonly RecipeApp.Models.RecipeAppContext _context;

        public RateModel(RecipeApp.Models.RecipeAppContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        public async Task<IActionResult> OnPostAsync(int _rate)
        {
            var rating = new Rating();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId != null)
            {
                rating.UserId = userId;
                rating.Value = _rate;
                rating.RecipeId = id;
                _context.Ratings.Add(rating);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Recipe/Index");
        }
    }
}
