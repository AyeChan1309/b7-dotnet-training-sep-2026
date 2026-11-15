using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sep2026.LibraryManagement.Database.AppDbContextModels;
using Sep2026.LibraryManagementWebApi.Models;
using Sep2026.LibraryManagementWebApi.Models.Book;

namespace Sep2026.LibraryManagementWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly AppDbContext _db;
        public BookController(AppDbContext db)
        {
            _db = db;
        }


        [HttpGet]

        public IActionResult GetBooks()
        {
            var lst = _db.Books.ToList();
            return Ok(lst);
        }

        [HttpGet("Edit/{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _db.Books.FirstOrDefault(s => s.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }




        [HttpPost]

        public IActionResult CreateBook( BookCreateRequestModel requestModel)
        {

            _db.Books.Add(new Book
            {
                CategoryId = requestModel.CategoryId,
                Title = requestModel.Title.Trim(),
                AuthorName = requestModel.AuthorName.Trim(),
                PublishedYear = requestModel.PublishedYear

                 
            });

            bool exists = _db.Books
    .Any(b => b.Title == requestModel.Title.Trim());

            if (exists)
            {
                return Conflict("Book already exists.");
            }


            int result = _db.SaveChanges();
            string message = result > 0 ? "Book created successfully." : "Failed to create book.";
            return Ok(message);
        }


        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {
            var book = _db.Books.FirstOrDefault(s => s.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            _db.Books.Remove(book);
            int result = _db.SaveChanges();
            string message = result > 0 ? "Book deleted successfully." : "Failed to delete book.";

            return Ok(message);
        }

        [HttpPut("{id}")]

        public IActionResult UpsertBook(int id, BookUpsertRequestModel requestModel)
        {
            var item = _db.Books.FirstOrDefault(s => s.BookId == id);
            if (item is null)
            {
                return NotFound();
            }


            item.CategoryId = requestModel.CategoryId;
            item.Title = requestModel.Title.Trim();
            item.AuthorName = requestModel.AuthorName.Trim();
            item.PublishedYear = requestModel.PublishedYear;
            int result = _db.SaveChanges();
            string message = result > 0 ? "Book updated successfully." : "Failed to update book.";
            return Ok(message);
        }


        [HttpPatch("{id}")]


        public IActionResult UpdateBook(int id, BookUpdateRequestModel requestModel)
        {

            var item = _db.Books.FirstOrDefault(s => s.BookId == id);

            if (item is null)
            {
                return NotFound();

            }

            if (requestModel.CategoryId.HasValue)
            {
                bool categoryExists =  _db.Categories
           .Any(c => c.CategoryId == requestModel.CategoryId.Value);

                if (!categoryExists)
                {
                    return BadRequest("Category does not exist.");
                }
                item.CategoryId = (int)requestModel.CategoryId;

            }
            

            if (requestModel.Title != null && !requestModel.Title.IsNullOrEmpty())
            {
                item.Title = requestModel.Title.Trim();
            }

            if(requestModel.AuthorName != null && !requestModel.AuthorName.IsNullOrEmpty())
            {
                item.AuthorName = requestModel.AuthorName.Trim();
            }

            if(requestModel.PublishedYear != null && !requestModel.PublishedYear.HasValue)
            {
                item.PublishedYear = requestModel.PublishedYear;
            }


            int result = _db.SaveChanges();
            string message = result > 0 ? "Book updated successfully." : "Failed to update book.";

            return Ok(message);
        }



    }

}

