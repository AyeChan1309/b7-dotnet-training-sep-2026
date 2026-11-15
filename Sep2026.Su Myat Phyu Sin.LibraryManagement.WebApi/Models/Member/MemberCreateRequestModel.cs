using System.ComponentModel.DataAnnotations;

namespace Sep2026.LibraryManagementWebApi.Models.Member
{
    public class MemberCreateRequestModel
    {
        public string Name { get; set; } 

        public string Email { get; set; } 

         public string Phone { get; set; }
         public string Address { get; set; }
    }

    public class MemberUpsertRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }


    public class MemberUpdateRequestModel
    {
        public int? MemberId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
