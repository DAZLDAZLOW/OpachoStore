using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OpachoStore.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Необходимо указать логин")]
        [DisplayName("Логин")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Необходимо указать пароль")]
        [DisplayName("Пароль")]
        public string? Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}
