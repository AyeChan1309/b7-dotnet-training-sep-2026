using System.ComponentModel.DataAnnotations;

namespace Sep2026.LibraryManagementWebApi.Models
{
    public class CategoryCreateRequestModel
    {

        public string CategoryName { get; set; }

     
        public string Description { get; set; }


    }

    public class CategoryUpsertRequestModel
    {
        public string CategoryName { get; set; }


        public string Description { get; set; }
    }

    public class CategoryUpdateRequestModel
    {
          public int? Id { get; set; }

        public string? CategoryName { get; set; }
        public string? Description { get; set; }
    }




}
