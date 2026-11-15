using Sep2026.LibraryManagement.Database.AppDbContextModels;
using System.ComponentModel.DataAnnotations;

namespace Sep2026.LibraryManagementWebApi.Models.Borrow
{
    public class BorrowingCreateRequestModel
    {

      
        public int MemberId { get; set; }


        public DateOnly BorrowDate { get; set; }

        public List<BorrowingDetailCreateRequestModel> Books { get; set; }
           = new();
    }

    public class BorrowingUpdateRequestModel
    {

        public int BorrowingId { get; set; }
        public int MemberId { get; set; }

        public DateOnly? BorrowDate { get; set; }
        
    }

    public class BorrowingUpsertRequestModel
    {
        public int BorrowingId { get; set; }
        public int MemberId { get; set; }
     
        public DateOnly BorrowDate { get; set; }
      
       


    }
}
