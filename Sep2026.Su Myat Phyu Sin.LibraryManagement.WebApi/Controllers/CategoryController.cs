using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sep2026.LibraryManagement.Database.AppDbContextModels;
using Sep2026.LibraryManagementWebApi.Models;

namespace Sep2026.LibraryManagementWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
       
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;
        }

    
        [HttpGet]

        public IActionResult GetCategories()
        {
            var lst = _db.Categories.ToList();
            return Ok(lst);
        }

        [HttpGet("Edit/{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _db.Categories.FirstOrDefault(s => s.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }




        [HttpPost]

        public IActionResult CreateCategory(CategoryCreateRequestModel requestModel)
        {

            _db.Categories.Add(new Category
            {
                CategoryName = requestModel.CategoryName.Trim(),
                Description = requestModel.Description.Trim()
            });

            bool exists =  _db.Categories
    .Any(c => c.CategoryName == requestModel.CategoryName.Trim());

            if (exists)
            {
                return Conflict("Category already exists.");
            }


            int result = _db.SaveChanges();
            string message = result > 0 ? "Category created successfully." : "Failed to create category.";
            return Ok(message);
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteCategory(int id)
        {
            var category = _db.Categories.FirstOrDefault(s => s.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(category);
            int result = _db.SaveChanges();
            string message = result > 0 ? "Category deleted successfully." : "Failed to delete category.";

            return Ok(message);
        }

        [HttpPut("{id}")]

        public IActionResult UpsertCategory(int id, CategoryUpsertRequestModel requestModel)
        {
            var item = _db.Categories.FirstOrDefault(s => s.CategoryId == id);
            if (item is null)
            {
                return NotFound();
            }
            item.CategoryName = requestModel.CategoryName.Trim();
            item.Description = requestModel.Description.Trim();
            int result = _db.SaveChanges();
            string message = result > 0 ? "Category updated successfully." : "Failed to update category.";
            return Ok(message);
        }


        [HttpPatch("{id}")]


        public IActionResult UpdateCategory(int id, CategoryUpdateRequestModel requestModel)
        {

            var item = _db.Categories.FirstOrDefault(s => s.CategoryId == id);
            if (item is null)
            {
                return NotFound();

            }

            if (requestModel.CategoryName != null && !requestModel.CategoryName.IsNullOrEmpty())
            {
                item.CategoryName = requestModel.CategoryName.Trim();
            }

            if (requestModel.Description != null && !requestModel.Description.IsNullOrEmpty())
            {
                item.Description = requestModel.Description.Trim();
            }
            int result = _db.SaveChanges();
            string message = result > 0 ? "Category updated successfully." : "Failed to update category.";

            return Ok(message);
        }



        }



    }

