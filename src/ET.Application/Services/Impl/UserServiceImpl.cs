using ET.Application.Models.UserDtos.Response;
using ET.Application.Models.UserDtos;
using ET.Application.Exceptions;
using ET.Core.Entities;
using ET.DataAccess.Repositories;
using ET.Application.Mappers;

namespace ET.Application.Services.Impl
{
    public class UserServiceImpl : Services.UserService
    {
        private readonly UserRepository _userRepository;
        private readonly UserMapper _userMapper;

        public UserServiceImpl(UserRepository userRepository, UserMapper userMapper)
        {
            _userRepository = userRepository;
            _userMapper = userMapper;
        }

        public UserResponseDto UserDelete(int id)
        {
            throw new NotImplementedException();
        }

        public UserResponseDto UserEdit(int id, UserRegisterDto userRegisterDto)
        {
            throw new NotImplementedException();
        }

        public List<UserResponseDto> UserFilterList(Dictionary<string, string> filters)
        {
            throw new NotImplementedException();
        }

        public List<UserResponseDto> UserList()
        {
            throw new NotImplementedException();
        }

        public UserResponseDto UserLogin(UserLoginDto userLoginDto)
        {
            throw new NotImplementedException();
        }

        public UserResponseDto UserRegister(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto == null) throw new InvalidArgumentsException("Sent user register data cannot be null!");

            if (userRegisterDto.Password != userRegisterDto.PasswordRepeat) throw new PasswordMissmatchException("Sent passwords does not match!");

            var user = _userRepository.FindByEmail(userRegisterDto.Email);
            if (user != null) throw new AlreadyExistsException("User with this email already exists!");

            var newUser = _userRepository.Save(user);

            return _userMapper.UserToUserDto(newUser);
        }

        public bool UserUpdatePassword(int id, PasswordChangeDto passwordChangeDto)
        {
            throw new NotImplementedException();
        }

        public bool UserValidation()
        {
            throw new NotImplementedException();
        }
    }
}
