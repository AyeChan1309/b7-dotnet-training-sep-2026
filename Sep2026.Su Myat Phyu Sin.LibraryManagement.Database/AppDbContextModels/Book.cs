using System;
using System.Collections.Generic;

namespace Sep2026.LibraryManagement.Database.AppDbContextModels;

public partial class Book
{
    public int BookId { get; set; }

    public int CategoryId { get; set; }

    public string Title { get; set; } = null!;

    public string AuthorName { get; set; } = null!;

    public int? PublishedYear { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<BorrowingDetail> BorrowingDetails { get; set; } = new List<BorrowingDetail>();

    public virtual Category Category { get; set; } = null!;
}
