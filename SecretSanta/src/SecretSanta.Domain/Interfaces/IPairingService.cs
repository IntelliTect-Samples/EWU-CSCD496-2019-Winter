using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Interfaces
{
    public interface IPairingService
    {
        void CreatePairing(User santa, User recipient);
        void CreatePairing(Pairing pair);
        Pairing FindPairing(int id);
    }
}
