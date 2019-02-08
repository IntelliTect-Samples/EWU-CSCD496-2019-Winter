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
            CreateMap<UserInputViewModel, User>();
            CreateMap<User, UserInputViewModel>();
            CreateMap<User, UserViewModel>();
        }

    }
}
