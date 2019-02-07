using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.ViewModels
{
    public class GiftViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? OrderOfImportance { get; set; }
        public string Url { get; set; }

        public GiftViewModel()
        {

        }

        public static GiftViewModel ToViewModel(Gift gift)
        {
            if (gift == null) throw new ArgumentNullException(nameof(gift));

            var result = new GiftViewModel
            {
                Id = gift.Id,
                Title = gift.Title,
                Description = gift.Description,
                OrderOfImportance = gift.OrderOfImportance,
                Url = gift.Url,
            };

            return result;
        }

        public static Gift ToModel(GiftViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            var result = new Gift
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Description = viewModel.Description,
                OrderOfImportance = viewModel.OrderOfImportance,
                Url = viewModel.Url,
            };

            return result;
        }
    }
}
