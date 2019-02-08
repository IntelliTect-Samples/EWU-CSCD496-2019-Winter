﻿namespace SecretSanta.Api.ViewModels
{
    public class GiftViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int OrderOfImportance { get; set; }
        public string Url { get; set; }

        public GiftViewModel()
        {

        }
    }
}
