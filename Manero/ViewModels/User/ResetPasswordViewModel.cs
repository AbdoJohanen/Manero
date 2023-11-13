using System.ComponentModel.DataAnnotations;

public class ResetPasswordViewModel
{
    [Display(Name = "E-mail Address")]
    [Required(ErrorMessage = "You must enter an e-mail address.")]
    [RegularExpression(@"^[a-zA-Z0-9._+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Your must enter a valid e-mail address.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    public string Token { get; set; } = null!;

    [Display(Name = "Password")]
    [Required(ErrorMessage = "You must enter a password.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "Your must enter a valid password.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    
    [Display(Name = "Confirm password")]
    [Required(ErrorMessage = "You must confirm your password.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = null!;
}
