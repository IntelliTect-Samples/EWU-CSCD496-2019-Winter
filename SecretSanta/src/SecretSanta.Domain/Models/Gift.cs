using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Gift
    {
        string Title { set; get; }
        int OrderOfImportance { set; get; }
        string Url { set; get; }
        string description { set; get; }
        User User { set; get; }
    }
}
