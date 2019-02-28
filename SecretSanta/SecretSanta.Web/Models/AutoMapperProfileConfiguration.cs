﻿using AutoMapper;
using SecretSanta.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Web.Models
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