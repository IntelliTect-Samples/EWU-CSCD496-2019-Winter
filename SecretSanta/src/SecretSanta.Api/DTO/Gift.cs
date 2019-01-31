using System;
using SecretSanta.Domain.Models;

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

        public Gift(SecretSanta.Domain.Models.Gift modelsGift)
        {
            if (modelsGift == null) throw new ArgumentNullException(nameof(modelsGift));

            Id = modelsGift.Id;
            Title = modelsGift.Title;
            Description = modelsGift.Description;
            OrderOfImportance = modelsGift.OrderOfImportance;
            Url = modelsGift.Url;
        }

        public static SecretSanta.Domain.Models.Gift ToEntity(DTO.Gift dtoGift)
        {
            //Pretend this is implemented
            return null;
        }
    }
}