using System;
using System.Collections.Generic;

namespace Sep2026.LibraryManagement.Database.AppDbContextModels;

public partial class Borrowing
{
    public int BorrowingId { get; set; }

    public int MemberId { get; set; }

    public DateOnly BorrowDate { get; set; }

    public virtual ICollection<BorrowingDetail> BorrowingDetails { get; set; } = new List<BorrowingDetail>();

    public virtual Member Member { get; set; } = null!;
}
