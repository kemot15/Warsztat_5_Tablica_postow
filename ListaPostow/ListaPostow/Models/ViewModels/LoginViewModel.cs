using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListaPostow.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Pole wymagane")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Pole wymagane"), DataType("Password")]
        public string Password { get; set; }
    }
}
