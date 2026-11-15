using System;
using System.Collections.Generic;

namespace Sep2026.LibraryManagement.Database.AppDbContextModels;

public partial class Borrow
{
    public int BorrowId { get; set; }

    public int MemberId { get; set; }

    public int BookId { get; set; }

    public DateOnly BorrowDate { get; set; }

    public DateOnly DueDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public int Quantity { get; set; }

    public int ReturnedQuantity { get; set; }

    public string Status { get; set; } = null!;

    public virtual Book Book { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;
}
