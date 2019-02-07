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
            CreateMap<UserViewModel, User>();

            CreateMap<GroupInputViewModel, Group>();
            CreateMap<Group, GroupInputViewModel>();

            CreateMap<Group, GroupViewModel>();
            CreateMap<GroupViewModel, Group>();

            CreateMap<GiftViewModel, Gift>();
            CreateMap<Gift, GiftViewModel>();

           /* CreateMap<Gift, GiftViewModel>();
            CreateMap<GiftViewModel, Gift>();
            deleteThisLine*/

        }
    }
}
