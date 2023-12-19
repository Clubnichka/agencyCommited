using System.ComponentModel.DataAnnotations;

namespace agency.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Введите имя")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Введите почту")]
        [Display(Name = "Введите почту")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Введите пароль")]
        public string Password { get; set; }
    }
}
