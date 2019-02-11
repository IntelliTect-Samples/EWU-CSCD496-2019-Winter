using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Services
{
    public interface IPairingService
    {
        Task<bool> GeneratePairingsForGroup(int groupId);
    }
}
