﻿using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SecretSanta.Import.Services
{
    public class GiftImportService
    {
        public NameParsingService NameParsingService { get; }
        public GiftImportService()
        {
            this.NameParsingService = new NameParsingService();
        }
        
        public void ImportGifts(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            string[] firstLast = NameParsingService.ParseHeader(lines[0]);
            User user = new User { FirstName = firstLast[0], LastName = firstLast[1] };

        }
    }
}
