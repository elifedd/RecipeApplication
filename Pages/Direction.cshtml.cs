using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApp.Models;
using System.Security.Claims;

namespace RecipeApp.Pages
{
    public class DirectionModel : PageModel
    {
        private readonly RecipeAppContext _context;

        public DirectionModel(RecipeAppContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public List<Direction> Directions { get; set; }

        public IActionResult OnGet()
        {
            Directions = _context.Directions.Where(c => c.RecipeId == Id).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int _stepNumber, string _description)
        {
            var direction = new Direction();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                direction.StepNumber = _stepNumber;
                direction.Description = _description;
                direction.RecipeId = Id;
                _context.Directions.Add(direction);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Direction");
        }

    }
}
