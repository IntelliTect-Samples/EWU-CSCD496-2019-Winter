using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SecretSanta.Import.FileImport
{
    public class FileImport
    {
        public StreamReader streamReader { get; set; }

        public User readUser()
        {
            User user = new User();
            return user;
        }
    }
}
