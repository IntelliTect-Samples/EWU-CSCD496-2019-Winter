using AutoMapper;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.Models
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<UserInputViewModel, User>();
            CreateMap<User, UserInputViewModel>();

            CreateMap<User, UserViewModel>();

            CreateMap<GiftInputViewModel, Gift>();
            CreateMap<Gift, GiftInputViewModel>();

            CreateMap<Gift, GiftViewModel>();

            CreateMap<GroupInputViewModel, Group>();
            CreateMap<Group, GroupInputViewModel>();

            CreateMap<Group, GroupViewModel>();
        }
    }
}
