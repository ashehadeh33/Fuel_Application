using System.ComponentModel.DataAnnotations;

namespace qenergy.Models
{
    public class Quote
    {
        [Key]
        public int Id { get; set; } // Unique identifier and primary key for database

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        public int GallonsRequested { get; set; }

        public string? DeliveryAddress { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }

        // Will have to reconfigure migrations - check later
        // [Required]
        // [DataType(DataType.Date)]
        // public Nullable<DateTime> DeliveryDate { get; set; }


        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal SuggestedPricePerGallon { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalAmountDue { get; set; }

        [Required]
        public int customerId { get; set; } // identifier (user id) of who made the quote
    }
}

