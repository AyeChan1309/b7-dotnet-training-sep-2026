namespace Sep2026.LibraryManagementWebApi.Models.Borrow
{
    public class BorrowingDetailReturnRequestModel
    {
        public DateOnly ReturnDate { get; set; }
    }

    public class BorrowingDetailReturnUpsertRequestModel
    {
        public DateOnly ReturnDate { get; set; }
    }


}
