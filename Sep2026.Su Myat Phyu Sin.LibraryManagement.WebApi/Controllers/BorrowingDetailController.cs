using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sep2026.LibraryManagement.Database.AppDbContextModels;
using Sep2026.LibraryManagementWebApi.Models.Borrow;

namespace Sep2026.LibraryManagementWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingDetailController : Controller
    {

        private readonly AppDbContext _db;
        public BorrowingDetailController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]

        public IActionResult GetBorrowingDetail()
        {
            //    var lst = _db.BorrowingDetails.Include(x => x.Book).ThenInclude(x=>x.Category)
            //.Include(x => x.Borrowing).ThenInclude(x => x.Member).ToList();
            var lst = _db.BorrowingDetails.ToList();
            return Ok(lst);
        }

        [HttpGet("Edit/{id}")]
        public IActionResult EditBorrowingDetails(int id)
        {
            var borrowing = _db.BorrowingDetails.FirstOrDefault(b => b.BorrowingId == id);
            if (borrowing == null)
            {
                return NotFound();
            }
            return Ok(borrowing);
        }


        [HttpPost]

        public IActionResult CreateBorrowingDetail(BorrowingDetailCreateRequestModel requestModel)
        {
            bool borrowingExists =  _db.Borrowings
               .Any(x => x.BorrowingId == requestModel.BorrowingId);

            if (!borrowingExists)
            {
                return BadRequest("Borrowing does not exist.");
            }

            bool bookExists =  _db.Books
                .Any(x => x.BookId == requestModel.BookId);

            if (!bookExists)
            {
                return BadRequest("Book does not exist.");
            }

            bool alreadyBorrowed =  _db.BorrowingDetails
                .Any(x =>
                    x.BookId == requestModel.BookId &&
                    x.ReturnDate == null);

            if (alreadyBorrowed)
            {
                return Conflict("This book is currently borrowed.");
            }

            var item = new BorrowingDetail
            {
                BorrowingId = requestModel.BorrowingId,
                BookId = requestModel.BookId,
                DueDate = requestModel.DueDate
            };

            _db.BorrowingDetails.Add(item);

         
           int result = _db.SaveChanges();
            string message = result > 0 ? "Category created successfully." : "Failed to create category.";
            return Ok(message);

      
        }


        //[HttpPatch("{id}/return")]
        //public IActionResult UpsertBorrowingDetail(
        //    int id,
        //    BorrowingDetailReturnUpsertRequestModel requestModel)
        //{
        //    var item = _db.BorrowingDetails
        //        .FirstOrDefault(x => x.BorrowingDetailId == id);

        //    if (item == null)
        //    {
        //        return NotFound("Borrowing detail not found.");
        //    }

        //    if (item.ReturnDate.HasValue)
        //    {
        //        item.ReturnDate = requestModel.ReturnDate;

        //       int result =  _db.SaveChanges();

        //      string message = result > 0 ? "Borrowing detail updated successfully." : "Failed to update borrowing detail.";

        //        return Ok(message);
        //    }

        //    return BadRequest("This book has already been returned.");
        //}
        [HttpPatch("{id}/return")]
        public IActionResult UpdateBorrowingDetail(
    int id,
    BorrowingDetailReturnUpsertRequestModel requestModel)
        {
            var item = _db.BorrowingDetails
                .FirstOrDefault(x => x.BorrowingDetailId == id);

            if (item == null)
            {
                return NotFound("Borrowing detail not found.");
            }

           
      
            item.ReturnDate = requestModel.ReturnDate;

            int result = _db.SaveChanges();

            string message = result > 0
                ? "Book returned updated successfully."
                : "Failed to update borrowing detail.";

            return Ok(message);
        }


    }
}
