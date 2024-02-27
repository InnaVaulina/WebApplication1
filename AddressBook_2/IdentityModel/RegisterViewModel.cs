using System.ComponentModel.DataAnnotations;

namespace AddressBook_2mvc.IdentityModel
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "LoginProp")]
        public string LoginProp { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string ConfirmPassword { get; set; }
    }
}
