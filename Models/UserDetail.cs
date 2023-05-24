using System;
using System.Collections.Generic;

namespace RecipeApp.Models;

public partial class UserDetail
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public byte[]? Photo { get; set; }
}
