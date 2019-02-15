using System.Threading.Tasks;

namespace SecretSanta.Domain.Services.Interfaces
{
    public interface IPairingService
    {
        Task<bool> GeneratePairings(int groupId);
    }
}