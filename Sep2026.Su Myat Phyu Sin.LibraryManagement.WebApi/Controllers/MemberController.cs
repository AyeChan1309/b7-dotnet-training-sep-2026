using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Sep2026.LibraryManagement.Database.AppDbContextModels;
using Sep2026.LibraryManagementWebApi.Models.Member;

namespace Sep2026.LibraryManagementWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : Controller
    {
        private readonly AppDbContext _db;
        public MemberController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetMembers()
        {
            var lst = _db.Members.ToList();
            return Ok(lst);
        }


        [HttpGet("Edit/{id}")]

        public IActionResult GetMember(int id)
        {
            var member = _db.Members.FirstOrDefault(s => s.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }
        [HttpPost]

        public IActionResult CreateMember(MemberCreateRequestModel requestModel)
        {
            _db.Members.Add(new Member
            {
                Name = requestModel.Name.Trim(),
                Email = requestModel.Email.Trim(),
                Phone = requestModel.Phone,
                Address = requestModel.Address.Trim()
            });
            
            int result = _db.SaveChanges();
            string message = result > 0 ? "Member created successfully." : "Failed to create member.";
            return Ok(message);
         
        }

        [HttpPut("{id}")]

        public IActionResult UpsertMember(int id, MemberUpsertRequestModel requestModel)
        {
            var item = _db.Members.FirstOrDefault(s => s.MemberId == id);
            if (item is null)
            {
                return NotFound();

            }
            item.Name = requestModel.Name.Trim();
            item.Email = requestModel.Email.Trim();
            item.Phone = requestModel.Phone;
            item.Address = requestModel.Address.Trim();
            int result = _db.SaveChanges();
            string message = result > 0 ? "Member updated successfully." : "Failed to update member.";

            return Ok(message);
        }

        [HttpPatch("{id}")]

        public IActionResult UpdateMember(int id, MemberUpdateRequestModel requestModel)
        {
            var item = _db.Members.FirstOrDefault(s => s.MemberId == id);
            if (item is null)
            {
                return NotFound();
            }
            if (requestModel.Name != null)
            {
                item.Name = requestModel.Name.Trim();
            }
            if (requestModel.Email != null)
            {
                item.Email = requestModel.Email.Trim();
            }
            if (requestModel.Phone != null)
            {
                item.Phone = requestModel.Phone;
            }
            if (requestModel.Address != null)
            {
                item.Address = requestModel.Address.Trim();
            }
            int result = _db.SaveChanges();
            string message = result > 0 ? "Member updated successfully." : "Failed to update member.";
            return Ok(message);
        }

        [HttpDelete("{id}")]

   

        public IActionResult DeleteMember(int id)
        {
            var member = _db.Members.FirstOrDefault(s => s.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            _db.Members.Remove(member);
            int result = _db.SaveChanges();
            string message = result > 0 ? "Member deleted successfully." : "Failed to delete member.";

            return Ok(message);
        }


    }
}
