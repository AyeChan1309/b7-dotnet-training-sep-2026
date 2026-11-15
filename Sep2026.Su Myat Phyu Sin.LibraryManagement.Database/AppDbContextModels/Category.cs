using System;
using System.Collections.Generic;

namespace Sep2026.LibraryManagement.Database.AppDbContextModels;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
