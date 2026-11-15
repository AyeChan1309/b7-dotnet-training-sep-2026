using System.ComponentModel.DataAnnotations;

namespace Sep2026.LibraryManagementWebApi.Models.Book
{
    public class BookCreateRequestModel
    {

        public int CategoryId { get; set; }


        public string Title { get; set; } 


        public string AuthorName { get; set; } 

        public int? PublishedYear { get; set; }


    

    }

    public class BookUpsertRequestModel
    {

        public int CategoryId { get; set; }

        public string Title { get; set; }

        public string AuthorName { get; set; }
        public int PublishedYear { get; set; }

        
    }

        public class  BookUpdateRequestModel
    {
     

        public int? CategoryId { get; set; }

    
        public string? Title { get; set; } 

      
        public string? AuthorName { get; set; } 

        public int? PublishedYear { get; set; }



    }
}
