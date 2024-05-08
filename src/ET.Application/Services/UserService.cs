using ET.Application.Models.UserDtos;
using ET.Application.Models.UserDtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Application.Services;

public interface UserService
{
    public UserResponseDto UserRegister(UserRegisterDto userRegisterDto);

    public UserResponseDto UserLogin(UserLoginDto userLoginDto);

    public bool UserValidation();

    public bool UserUpdatePassword(int id, PasswordChangeDto passwordChangeDto);

    public UserResponseDto UserEdit(int id, UserRegisterDto userRegisterDto);

    public UserResponseDto UserDelete(int id);

    public List<UserResponseDto> UserList();

    public List<UserResponseDto> UserFilterList(Dictionary<string, string> filters);

}
