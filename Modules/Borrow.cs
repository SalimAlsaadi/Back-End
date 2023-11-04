using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Modules
{
    public class Borrow
    {
        [Key]
        public int transID { get; set; }

        [ForeignKey("patron")]
        public int patronId { get; set; }

        [ForeignKey("book")]
        public int bookId { get; set; }

        //[Timestamp]
        public string borrowDate { get; set; }

        //[Timestamp]
        public string returnDate { get; set; }

        public User patron { get; set; }

        public Book book { get; set; }

        
    }
}
