using System.ComponentModel.DataAnnotations;

namespace WebForum.ViweModels
{
    public class RegisterVM
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage ="Email required")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Password confirmation is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
