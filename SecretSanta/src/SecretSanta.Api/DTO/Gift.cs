using System;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.DTO
{
    public class Gift
    {
        public int Id { get; set; }
        public string Title { get; set; }
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
            OrderOfImportance = modelsGift.OrderOfImportance;
            Url = modelsGift.Url;
        }

        public static SecretSanta.Domain.Models.Gift ToEntity(DTO.Gift dtoGift)
        {
            if (dtoGift == null)
            {
                throw new ArgumentNullException(nameof(dtoGift));
            }
            Domain.Models.Gift entity = new Domain.Models.Gift//same arguements as constructor
            {
                Id = dtoGift.Id,
                Title = dtoGift.Title,
                OrderOfImportance = dtoGift.OrderOfImportance,
                Url = dtoGift.Url
            };

            return entity;
        }
    }
}