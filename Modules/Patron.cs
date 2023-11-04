using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Modules
{
    public class User
    {
        [Key]
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string UserEmail { get; set;}

        [Required]
        public string UserPassword { get; set;}

        [Phone]
        public string phoneNumber { get; set; }

        [StringLength(3)]
        public int age { get; set; }

        
        public string bank_account { get; set; }

        public string user_type { get; set; }
       
    }
}
