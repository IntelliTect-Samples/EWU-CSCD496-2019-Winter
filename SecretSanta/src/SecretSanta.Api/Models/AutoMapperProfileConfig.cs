using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SecretSanta.Api.Models;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Models
{
    public class AutoMapperProfileConfig : Profile
    {

        public AutoMapperProfileConfig()
        {
            CreateMap<Gift, GiftViewModel>();

            CreateMap<User, UserViewModel>();
            CreateMap<UserInputViewModel, User>();

            CreateMap<Group, GroupViewModel>();
            CreateMap<GroupInputViewModel, Group>();
            //CreateMap<GroupUser, GroupUserViewModel>();

        }

    }
}
