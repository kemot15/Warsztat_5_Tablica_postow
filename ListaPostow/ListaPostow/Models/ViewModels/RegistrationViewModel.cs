using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ListaPostow.Models.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name ="Login")]
        public string Name { get; set;}
        [Required(ErrorMessage = "Pole wymagane")]
        [EmailAddress(ErrorMessage = "Nie poprawny adres email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Pole wymagane"), DataType("Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Pole wymagane"), DataType("Password"), Compare("Password")]
        public string ReapeatPassword { get; set; }
    }
}
