using System;
using System.Collections.Generic;

namespace Sep2026.LibraryManagement.Database.AppDbContextModels;

public partial class Member
{
    public int MemberId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public DateOnly JoinedDate { get; set; }

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
   
}
