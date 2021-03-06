using System.ComponentModel.DataAnnotations;

namespace Project22.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Логін")]
        public string UserName { get; set; }

        [Required]
        [UIHint("password")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам`ятати дані?")]
        public bool RememberMe { get; set; }
    }
}