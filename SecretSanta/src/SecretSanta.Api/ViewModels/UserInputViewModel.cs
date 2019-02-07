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
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static User ToModel(UserInputViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            var result = new User
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
            };

            return result;
        }
    }
}
