using qenergy.Models;
using System.ComponentModel.DataAnnotations;

namespace qenergy.Models
{
    // Principal (parent)
    // One to one relationship (user <--> profile)
    public class User
    {
        [Key]
        public int Id { get; set; } // Unique identifier and primary key for database
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public Profile? profile { get; set; } // Reference navigation to dependent (child)
    }
}