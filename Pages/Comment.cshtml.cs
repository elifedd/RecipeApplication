using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecipeApp.Pages
{
    [Authorize]
    public class CommentModel : PageModel
    {
        private readonly RecipeAppContext _context;

        public CommentModel(RecipeAppContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public List<Comment> Comments { get; set; }

        public IActionResult OnGet()
        {
            // Retrieve the comments for the specified recipe ID
            Comments = _context.Comments.Where(c => c.RecipeId == Id).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string _comment)
        {
            var comment = new Comment();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                comment.UserId = userId;
                comment.RecipeId = Id;
                comment.Content = _comment.ToString();
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Comment");
        }
    }
}
