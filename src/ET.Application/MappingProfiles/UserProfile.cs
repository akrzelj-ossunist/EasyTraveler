using AutoMapper;
using ET.Application.Models.User;
using ET.DataAccess.Identity;

namespace ET.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserModel, ApplicationUser>();
    }
}
