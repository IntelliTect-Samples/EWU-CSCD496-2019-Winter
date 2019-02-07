using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public GroupViewModel() { }

        public static GroupViewModel ToViewModel(Group group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            var result = new GroupViewModel
            {
                Id = group.Id,
                Name = group.Name,
            };

            return result;
        }

        public static Group ToModel(GroupViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            var result = new Group
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
            };

            return result;
        }
    }
}
