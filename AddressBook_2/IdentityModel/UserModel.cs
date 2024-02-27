using System.ComponentModel.DataAnnotations;

namespace AddressBook_2mvc.IdentityModel
{
    public class UserModel
    {
        [Required]
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Роль")]
        public string UserRole { get; set; }
    }


    

    public class RegisterViewModelForAdmin : RegisterViewModel
    {


        public string UserRole { get; set; }
    }



    


    public class UserRole
    {
        public string Id { get; set; }
        public string Role { get; set; }

    }


    public class UserModelForAdmin
    {

        public string Id { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
    }
}
