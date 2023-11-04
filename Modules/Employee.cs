using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Modules
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        [Required]
        public string EmpName { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        public String Password { get; set; }
    }
}
