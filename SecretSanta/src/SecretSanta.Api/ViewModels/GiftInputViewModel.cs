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
    }
}
