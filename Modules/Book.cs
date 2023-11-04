using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Modules
{
    public class Book
    {
        [Key]
        public int bookId { get; set; }

        [Required]
        [StringLength(50)]
        public string bookName { get; set; }

        [Required]
        [StringLength(50)]
        public string author { get; set; }

        [Required]
        public string pub_year { get; set; }

        [Required]
        public string available_status { get; set; }

        [Required]
        public int price { get; set; }  


        public string srcImages { get; set; }
    }
}
