using System.Collections.Generic;
using System.Threading.Tasks;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services.Interfaces
{
    public interface IPairingService
    {
        Task<List<Pairing>> GeneratePairings(int groupId);
    }
}