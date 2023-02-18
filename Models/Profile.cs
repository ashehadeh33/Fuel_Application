using System.ComponentModel.DataAnnotations;

public class Profile
{
    [Required(ErrorMessage = "Full Name is required.")]
    [StringLength(50, ErrorMessage = "Full Name cannot be longer than 50 characters.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Address 1 is required.")]
    [StringLength(100, ErrorMessage = "Address 1 cannot be longer than 100 characters.")]
    public string Address1 { get; set; }

    [StringLength(100, ErrorMessage = "Address 2 cannot be longer than 100 characters.")]
    public string Address2 { get; set; }

    [Required(ErrorMessage = "City is required.")]
    [StringLength(100, ErrorMessage = "City cannot be longer than 100 characters.")]
    public string City { get; set; }

    [Required(ErrorMessage = "State is required.")]
    public string State { get; set; }

    [Required(ErrorMessage = "Zipcode is required.")]
    [RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = "Invalid Zipcode format.")]
    public string Zipcode { get; set; }
}
