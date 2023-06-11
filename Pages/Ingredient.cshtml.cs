using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApp.Models;
using System.Security.Claims;
using System.Xml.Linq;

namespace RecipeApp.Pages
{
    public class IngredientModel : PageModel
    {
        private readonly RecipeAppContext _context;
        
        public IngredientModel(RecipeAppContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public IActionResult OnGet()
        {
            Ingredients = _context.Ingredients.Where(c => c.RecipeId == Id).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string _name, int _quantity, string _unit)
        {
            var ingredient = new Ingredient();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                ingredient.Name = _name;
                ingredient.Quantity = _quantity;
                ingredient.Unit = _unit;
                ingredient.RecipeId = Id;
                _context.Ingredients.Add(ingredient);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Ingredient");
        }
    }
}
