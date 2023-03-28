using qenergy.Models;
using System.ComponentModel.DataAnnotations;

namespace qenergy.Models
{
    public class User
    {
        public int Id { get; set; } // Unique identifier and primary key for database
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public Profile? profile { get; set; }
    }
}