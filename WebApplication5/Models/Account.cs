using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApplication5.Models
{
    public class Account
    {
            [Required]
            [Display(Name = "Логин")]
            public string UserName { get; set; }

            [Required]
            [UIHint("password")]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [Display(Name = "Запомнить меня?")]
            public bool RememberMe { get; set; }
    }
}
