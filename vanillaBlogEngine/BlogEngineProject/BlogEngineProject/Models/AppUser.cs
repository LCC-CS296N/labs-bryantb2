using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BlogEngineProject.Models
{
    public class AppUser : IdentityUser
    {
        public String FirstName {get; set;}
        public DateTime BirthDate {get; set;}
    }
}
