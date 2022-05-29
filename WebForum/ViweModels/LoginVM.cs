using System.ComponentModel.DataAnnotations;

namespace WebForum.ViweModels
{
    public class LoginVM
    {
        [Display(Name = "Email Adress" )]
        [Required(ErrorMessage ="Email is required!")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }    
    }
}
