using System;
using System.Collections.Generic;

namespace RecipeApp.Models;

public partial class Direction
{
    public int DirectionId { get; set; }

    public int StepNumber { get; set; }

    public string Description { get; set; } = null!;

    public int RecipeId { get; set; }
}
