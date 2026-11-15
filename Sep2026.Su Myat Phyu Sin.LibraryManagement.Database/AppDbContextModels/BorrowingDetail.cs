using System;
using System.Collections.Generic;

namespace Sep2026.LibraryManagement.Database.AppDbContextModels;

public partial class BorrowingDetail
{
    public int BorrowingDetailId { get; set; }

    public int BorrowingId { get; set; }

    public int BookId { get; set; }

    public DateOnly DueDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Borrowing Borrowing { get; set; } = null!;
}
