using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.ViewModels
{
    public class GroupInputViewModel
    {
        [Required]
        public string Name { get; set; }

        public static Group ToModel(GroupInputViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            var result = new Group
            {
                Name = viewModel.Name,
            };

            return result;
        }
    }
}
