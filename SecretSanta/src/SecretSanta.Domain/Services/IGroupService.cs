using SecretSanta.Domain.Models;
using System.Collections.Generic;

namespace SecretSanta.Domain.Services
{
    public interface IGroupService
    {
        Group AddGroup(Group @group);
        Group UpdateGroup(Group @group);
        List<Group> FetchAll();
    }
}
