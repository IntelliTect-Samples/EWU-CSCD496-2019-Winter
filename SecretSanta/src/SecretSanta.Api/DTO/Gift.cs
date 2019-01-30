using System;

namespace SecretSanta.Api.DTO
{
    public class Gift
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int OrderOfImportance { get; set; }
        public string Url { get; set; }

        public Gift()
        {
                
        }

        public Gift(SecretSanta.Domain.Models.Gift gift)
        {
            if (gift == null) throw new ArgumentNullException(nameof(gift));

            Id = gift.Id;
            Title = gift.Title;
            Description = gift.Description;
            OrderOfImportance = gift.WantTier;
            Url = gift.URL;
        }

        public static Domain.Models.Gift GetDomainGift(Gift gift)
        {
            Domain.Models.Gift mGift = new Domain.Models.Gift()
            {
                Id = gift.Id,
                Title = gift.Title,
                Description = gift.Description,
                URL = gift.Url,
                WantTier = gift.OrderOfImportance
            };

            return mGift;
        }
    }
}
