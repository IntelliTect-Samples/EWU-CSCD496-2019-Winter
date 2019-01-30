using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests
{
    public class TestableGroupService : IGroupService
    {
        public List<Group> ToReturn { get; set; }

        public List<Group> GetAllGroups()
        {
            return ToReturn;
        }
    }
}
