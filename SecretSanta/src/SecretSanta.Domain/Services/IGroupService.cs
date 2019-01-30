using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public interface IGroupService
    {
        List<Group> GetAllGroups();
    }
}
