﻿using System;

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

		public Gift(Domain.Models.Gift gift)
		{
			if (gift == null) throw new ArgumentNullException(nameof(gift));

			Id = gift.Id;
			Title = gift.Title;
			Description = gift.Description;
			OrderOfImportance = gift.OrderOfImportance;
			Url = gift.Url;
		}

		public static Domain.Models.Gift ToDomain(Gift dtoGift)
		{
			if (dtoGift == null) throw new ArgumentNullException(nameof(dtoGift));
			return new Domain.Models.Gift()
			{
				Id = dtoGift.Id,
				Title = dtoGift.Title,
				Description = dtoGift.Description,
				OrderOfImportance = dtoGift.OrderOfImportance,
				Url = dtoGift.Url
			};
		}
	}
}