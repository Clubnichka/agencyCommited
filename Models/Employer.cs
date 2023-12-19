using System.ComponentModel.DataAnnotations;

namespace agency.Models
{
    public class Employer:Client
    {
        [Required(ErrorMessage = "Введите название компании")]
        [Display(Name = "Введите название компании")]
        public String companyName {  get; set; }
        public String Description { get; set; }
        public const bool accessLevel1=true;
    }
}
