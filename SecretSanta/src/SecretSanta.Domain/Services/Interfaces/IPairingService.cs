using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Services.Interfaces
{
    public interface IPairingService
    {
        Task<List<Pairing>> GeneratePairings(int groupId);
    }
}
