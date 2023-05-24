using System;
using System.Collections.Generic;

namespace RecipeApp.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public string Cuisine { get; set; } = null!;

    public int DifficultyLevel { get; set; }

    public int PreparationTime { get; set; }

    public int CookingTime { get; set; }

    public int Servings { get; set; }

    public string UserId { get; set; } = null!;
}
