using System;
using System.Collections.Generic;

namespace RecipeApp.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int RecipeId { get; set; }

    public string UserId { get; set; } = null!;

    public string Content { get; set; } = null!;
}
