using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels.Auth
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email Is Required !!")]
        public string Email { get; set; }
    }
}
