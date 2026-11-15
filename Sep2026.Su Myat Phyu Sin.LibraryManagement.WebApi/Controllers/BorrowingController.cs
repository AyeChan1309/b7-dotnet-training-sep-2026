using Microsoft.AspNetCore.Mvc;
using Sep2026.LibraryManagement.Database.AppDbContextModels;
using Sep2026.LibraryManagementWebApi.Models.Borrow;

namespace Sep2026.LibraryManagementWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : Controller
    {
        private readonly AppDbContext _db;
        public BorrowingController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetBorrowings()
        {
            var lst = _db.Borrowings.ToList();
            return Ok(lst);
        }

        [HttpGet("Edit/{id}")]
        public IActionResult EditBorrowing(int id)
        {
            var borrowing = _db.Borrowings.FirstOrDefault(b => b.BorrowingId == id);
            if (borrowing == null)
            {
                return NotFound();
            }
            return Ok(borrowing);
        }




        [HttpPost]
        public IActionResult CreateBorrowing(
    BorrowingCreateRequestModel requestModel)
        {

            var memberExists = _db.Members
                .Any(m => m.MemberId == requestModel.MemberId);

            if (!memberExists)
            {
                return BadRequest("Member does not exist.");
            }


            if (requestModel.Books == null || !requestModel.Books.Any())
            {
                return BadRequest("Please select at least one book.");
            }

            var bookIds = requestModel.Books.Select(b => b.BookId).ToList();


            if (bookIds.Distinct().Count() != bookIds.Count)
            {
                return BadRequest("Duplicate books are not allowed.");
            }


            var existingBookIds = _db.Books
                .Where(b => bookIds.Contains(b.BookId))
                .Select(b => b.BookId)
                .ToList();

            if (existingBookIds.Count != bookIds.Count)
            {
                return BadRequest("One or more books do not exist.");
            }


            var unavailableBookIds = _db.BorrowingDetails
                .Where(bd => bookIds.Contains(bd.BookId)
                          && bd.ReturnDate == null)
                .Select(bd => bd.BookId)
                .ToList();

            if (unavailableBookIds.Any())
            {
                return BadRequest(
                    $"These books are already borrowed: {string.Join(", ", unavailableBookIds)}");
            }


            var borrowing = new Borrowing
            {
                MemberId = requestModel.MemberId,
                BorrowDate = requestModel.BorrowDate,

                BorrowingDetails = requestModel.Books.Select(b =>
                    new BorrowingDetail
                    {
                        BookId = b.BookId,
                        DueDate = b.DueDate,
                        ReturnDate = null
                    }).ToList()
            };


            _db.Borrowings.Add(borrowing);

            int result = _db.SaveChanges();

            if (result > 0)
            {
                return Ok(new
                {
                    borrowing.BorrowingId,
                    borrowing.MemberId,
                    borrowing.BorrowDate,

                    Books = borrowing.BorrowingDetails.Select(d => new
                    {
                        d.BorrowingDetailId,
                        d.BookId,
                        d.DueDate,
                        d.ReturnDate
                    })
                });
            }

            return BadRequest("Failed to create borrowing.");
        }
    

    [HttpPut("{id}")]

        public IActionResult UpsertBorrowing(int id, BorrowingUpsertRequestModel requestModel)
        {
            var borrowing = _db.Borrowings.FirstOrDefault(b => b.BorrowingId == id);
            if (borrowing == null)
            {
                return NotFound();
            }
            borrowing.MemberId = requestModel.MemberId;
            borrowing.BorrowDate = requestModel.BorrowDate;
            int result = _db.SaveChanges();
            string message = result > 0 ? "Borrowing updated successfully." : "Failed to update borrowing.";
            return Ok(message);
        }


 

 


    }

}