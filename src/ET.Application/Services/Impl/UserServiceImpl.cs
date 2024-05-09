using ET.Application.Models.UserDtos.Response;
using ET.Application.Models.UserDtos;
using ET.Application.Exceptions;
using ET.DataAccess.Repositories;
using ET.Application.Mappers;
using ET.Application.Utilities;
using Microsoft.AspNetCore.Http;

namespace ET.Application.Services.Impl
{
    public class UserServiceImpl : UserService
    {
        private readonly UserRepository _userRepository;
        private readonly UserMapper _userMapper;
        private readonly JwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserServiceImpl(UserRepository userRepository, UserMapper userMapper, JwtService jwtService, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _userMapper = userMapper;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }

        public UserResponseDto UserDelete(Guid id)
        {
            throw new NotImplementedException();
        }

        public UserResponseDto UserEdit(Guid id, UserRegisterDto userRegisterDto)
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

        public LoginResponseDto UserLogin(UserLoginDto userLoginDto)
        {
            if (userLoginDto == null) throw new InvalidArgumentsException("Sent user login data cannot be null!");

            var user = _userRepository.FindByEmail(userLoginDto.Email);
            if (user == null) throw new AlreadyExistsException("User with this email doesn't exist!");

            //if (BCrypt.Net.BCrypt.HashPassword(userLoginDto.Password) != user.Password) throw new PasswordMissmatchException("Password used for login is invalid!");

            string jwtToken = _jwtService.GenerateJwtToken(user.Role.ToString(), user.Id.ToString());

            _httpContextAccessor.HttpContext.Session.SetString("_JwtToken", jwtToken);

            var response = new LoginResponseDto
            {
                JwtToken = jwtToken,
                UserResponseDto = _userMapper.UserToUserDto(user)
            };

            return response;
        }

        public UserResponseDto UserRegister(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto == null) throw new InvalidArgumentsException("Sent user register data cannot be null!");

            if (userRegisterDto.Password != userRegisterDto.PasswordRepeat) throw new PasswordMissmatchException("Sent passwords does not match!");

            var user = _userRepository.FindByEmail(userRegisterDto.Email);
            if (user != null) throw new AlreadyExistsException("User with this email already exists!");

            userRegisterDto.Password = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password);

            var newUser = _userRepository.Save(_userMapper.UserDtoToUser(userRegisterDto));

            return _userMapper.UserToUserDto(newUser);
        }

        public bool UserUpdatePassword(Guid id, PasswordChangeDto passwordChangeDto)
        {
            throw new NotImplementedException();
        }

        public bool UserValidation()
        {
            throw new NotImplementedException();
        }
    }
}
