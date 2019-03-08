using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.ViewModels
{
    public class GiftInputViewModel
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public int? OrderOfImportance { get; set; }
        public Uri Url { get; set; }
        public int UserId { get; set; }
    }
}
