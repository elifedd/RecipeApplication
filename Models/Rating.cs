using System;
using System.Collections.Generic;

namespace RecipeApp.Models;

public partial class Rating
{
    public int RatingId { get; set; }

    public int Value { get; set; }

    public int RecipeId { get; set; }

    public string UserId { get; set; } = null!;
}
