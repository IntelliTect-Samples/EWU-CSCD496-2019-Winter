using System;

namespace SecretSanta.Api.DTO
{
    public class Gift
    {
        public int GiftId { get; set; }
        public string GiftTitle { get; set; }
        public string GiftDescription { get; set; }
        public int GiftOrderOfImportance { get; set; }
        public string GiftUrl { get; set; }

        public Gift()
        {
                
        }

        public Gift(SecretSanta.Domain.Models.Gift gift)
        {
            if (gift == null)
            {
                throw new ArgumentNullException(nameof(gift));
            }

            GiftId = gift.Id;
            GiftTitle = gift.Title;
            GiftDescription = gift.Description;
            GiftOrderOfImportance = gift.OrderOfImportance;
            GiftUrl = gift.Url;
        }


        public static Domain.Models.Gift ToModelGift(DTO.Gift gift)
        {
            return new Domain.Models.Gift
            {
                Id = gift.GiftId,
                Title = gift.GiftTitle,
                Description = gift.GiftDescription,
                OrderOfImportance = gift.GiftOrderOfImportance,
                Url = gift.GiftUrl
             };
        }

    }
}
