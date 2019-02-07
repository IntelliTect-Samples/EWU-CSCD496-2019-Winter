using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.ViewModels
{
    public class UserInputViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static User ToModel(UserInputViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            var convertedUser = new User
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
            };

            return convertedUser;
        }
    }
}
