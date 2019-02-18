using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Services.Interfaces
{
    public interface IPairingService
    {
        Task<bool> createPairing(int groupId);
    }
}
