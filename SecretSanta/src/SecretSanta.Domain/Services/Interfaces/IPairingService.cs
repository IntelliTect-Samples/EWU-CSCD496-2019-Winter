using System.Threading.Tasks;

namespace SecretSanta.Domain.Services
{
    public interface IPairingService
    {
        Task<bool> GeneratePairing(int groupId);
    }
}