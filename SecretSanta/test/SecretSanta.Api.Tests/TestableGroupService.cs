using System.Collections.Generic;
using SecretSanta.Domain.Services;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Tests
{
    public class TestableGroupService : IGroupService
    {
        public List<Group> GetListOfGroup_Return { get; set; }

        public Group AddGroup_Return { get; set; }
        public Group AddGroup_Group { get; set; }

        public Group RemoveGroup_Return { get; set; }
        public Group RemoveGroup_Group { get; set; }

        public Group UpdateGroup_Return { get; set; }
        public Group UpdateGroup_Group { get; set; }

        public Group AddGroup(Group group)
        {
            AddGroup_Group = group;
            return AddGroup_Return;
        }

        public List<Group> FetchAll()
        {
            return GetListOfGroup_Return;
        }

        public Group RemoveGroup(Group group)
        {
            RemoveGroup_Group = group;
            return RemoveGroup_Return;
        }

        public Group UpdateGroup(Group group)
        {
            UpdateGroup_Group = group;
            return UpdateGroup_Return;
        }
    }
}
