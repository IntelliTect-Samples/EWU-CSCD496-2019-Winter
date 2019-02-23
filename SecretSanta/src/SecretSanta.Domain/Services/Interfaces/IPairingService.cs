using SecretSanta.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Services
{
    public interface IPairingService
    {
        Task<List<Pairing>> GeneratePairing(int groupId);
        Task<List<Pairing>> GetPairingsList(int groupId);
    }
}