using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services.Interfaces
{
    public interface IRandomService
    {
        int Next();
        int Next(int max);
    }
}
