using AutoMapper;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.Models
{
    public class AutoMapperProfileConfigs : Profile
    {
        public AutoMapperProfileConfigs()
        {
            CreateMap<Gift, GiftViewModel>();
            CreateMap<GiftViewModel, Gift>();

            CreateMap<Group, GroupViewModel>();
            CreateMap<GroupViewModel, Group>();
            CreateMap<GroupInputViewModel, Group>();
            CreateMap<Group, GroupInputViewModel>();

            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
            CreateMap<UserInputViewModel, User>();
            CreateMap<User, UserInputViewModel>();
        }
    }
}
