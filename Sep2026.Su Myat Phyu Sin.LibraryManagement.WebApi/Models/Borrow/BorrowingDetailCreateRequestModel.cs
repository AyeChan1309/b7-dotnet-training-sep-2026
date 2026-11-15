using Sep2026.LibraryManagement.Database.AppDbContextModels;

namespace Sep2026.LibraryManagementWebApi.Models.Borrow
{
    public class BorrowingDetailCreateRequestModel
    {
      
        public int BorrowingId { get; set; }

        public int BookId { get; set; }

        public DateOnly DueDate { get; set; }

        

    }

    public class BorrowingDetailUpsertRequestModel
    {
       

        public DateOnly DueDate { get; set; }

    
    }

    public class BorrowingDetailUpdateRequestModel
    {
        
        public DateOnly? DueDate { get; set; }

    }
}
