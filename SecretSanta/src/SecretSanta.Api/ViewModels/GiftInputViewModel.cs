using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.ViewModels
{
    public class GiftInputViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int OrderOfImportance { get; set; }
        public string Url { get; set; }

        public static Gift ToModel(GiftInputViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            var convertedGift = new Gift
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                OrderOfImportance = viewModel.OrderOfImportance,
                Url = viewModel.Url
            };

            return convertedGift;
        }
    }
}
