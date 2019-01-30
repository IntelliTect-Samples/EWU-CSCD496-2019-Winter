using System;

namespace SecretSanta.Api.DTO
{
    public class Gift
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int OrderOfImportance { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }

        public Gift()
        {

        }

        public Gift(SecretSanta.Domain.Models.Gift gift)
        {
            if (gift == null) throw new ArgumentNullException(nameof(gift));

            Id = gift.Id;
            Title = gift.Title;
            OrderOfImportance = gift.OrderOfImportance;
            URL = gift.URL;
            Description = gift.Description;
        }
    }
}
