using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace BlogEngineProject.Models
{
    public class CreateUserViewModel
    {
            [Required]
            public string Name { get; set; }

            [Required]
            public string Email { get; set; }

            [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", 
            ErrorMessage = "Must be 8 characters minimum and have an uppercase and lowercase letter, with a digit and special character")]
            [Required]
            public String Password { get; set; }

            [Compare("Password")]
            [Required]
            public String ConfirmPassword { get; set; }
    }
}
