using System.ComponentModel.DataAnnotations;

namespace qenergy.Models
{
    public class Quote
    {
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        public int GallonsRequested { get; set; }

        public string DeliveryAddress { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal SuggestedPricePerGallon { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalAmountDue { get; set; }
    }
}

