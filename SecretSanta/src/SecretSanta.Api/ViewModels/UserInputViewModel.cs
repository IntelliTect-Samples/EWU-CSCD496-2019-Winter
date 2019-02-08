using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.ViewModels
{
    public class UserInputViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
