using System.ComponentModel.DataAnnotations;

namespace AddressBook_2mvc.IdentityModel
{
    public class EntryViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string LoginProp { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

       
    }
}
