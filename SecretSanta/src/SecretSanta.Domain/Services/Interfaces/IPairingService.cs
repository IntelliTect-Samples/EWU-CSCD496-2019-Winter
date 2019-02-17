using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Services
{
    public interface IPairingService
    {
        Task<List<Pairing>> GeneratePairingsForGroup(int groupId);
        Task<List<Pairing>> GetPairingsForGroup(int groupId);
    }
}
