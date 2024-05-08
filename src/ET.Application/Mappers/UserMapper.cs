using AutoMapper;
using ET.Application.Models.UserDtos.Response;
using ET.Application.Models.UserDtos;
using ET.Core.Entities;
namespace ET.Application.Mappers;

public class UserMapper
{
    private readonly IMapper _mapper;

    public UserMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserRegisterDto, User>();
            cfg.CreateMap<User, UserResponseDto>();
        });

        _mapper = config.CreateMapper();
    }

    public User UserDtoToUser(UserRegisterDto userRegisterDto)
    {
        return _mapper.Map<User>(userRegisterDto);
    }

    public UserResponseDto UserToUserDto(User user)
    {
        return _mapper.Map<UserResponseDto>(user);
    }
}

