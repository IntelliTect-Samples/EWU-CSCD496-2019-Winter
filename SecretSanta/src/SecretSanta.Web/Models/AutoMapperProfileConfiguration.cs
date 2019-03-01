using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SecretSanta.Web.ApiModels;

namespace SecretSanta.Web.Models
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<UserInputViewModel, UserViewModel>();
            CreateMap<GroupViewModel, GroupInputViewModel>();
        }
    }
}
