using AutoMapper;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Models
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<UserInputViewModel, UserViewModel>();
            CreateMap<GiftInputViewModel, GiftViewModel>();
        }
    }
}