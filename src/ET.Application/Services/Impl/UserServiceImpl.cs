using ET.Application.Models.UserDtos.Response;
using ET.Application.Models.UserDtos;
using ET.Application.Exceptions;
using ET.DataAccess.Repositories;
using ET.Application.Mappers;
using ET.Application.Utilities;
using Microsoft.AspNetCore.Http;
using ET.Application.Models;

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

        public bool UserDelete(Guid id)
        {
            var user = _userRepository.FindById(id);
            if (user == null) throw new NotFoundException("User with sent id doesnt exist!");

            _httpContextAccessor.HttpContext.Session.Remove("_JwtToken");

            return _userRepository.Delete(user);
        }

        public UserResponseDto UserEdit(Guid id, UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto == null) throw new InvalidArgumentsException("Sent user register data cannot be null!");

            var user = _userRepository.FindById(id);
            if (user == null) throw new NotFoundException("User with sent id doesnt exist!");

            user.FirstName = userRegisterDto.FirstName;
            user.LastName = userRegisterDto.LastName;

            var editedUser = _userRepository.Update(user);

            return _userMapper.UserToUserDto(editedUser);
        }

        public LoginResponseDto UserLogin(LoginDto userLoginDto)
        {
            if (userLoginDto == null) throw new InvalidArgumentsException("Sent user login data cannot be null!");

            var user = _userRepository.FindByEmail(userLoginDto.Email);
            if (user == null) throw new AlreadyExistsException("User with this email doesn't exist!");

            if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password)) throw new PasswordMissmatchException("Password used for login is invalid!");

            string jwtToken = _jwtService.GenerateJwtToken(user.Role.ToString(), user.Id.ToString());

            _httpContextAccessor.HttpContext.Session.SetString("_JwtToken", jwtToken);

            var response = new LoginResponseDto
            {
                JwtToken = jwtToken,
                UserResponseDto = _userMapper.UserToUserDto(user),
                IsSuccess = true
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
            if (passwordChangeDto == null) throw new InvalidArgumentsException("Sent user register data cannot be null!");

            var user = _userRepository.FindById(id);
            if (user == null) throw new NotFoundException("User with sent id doesnt exist!");

            if (passwordChangeDto.NewPassword != passwordChangeDto.NewPasswordRepeat) throw new PasswordMissmatchException("Sent passwords doesnt match!");

            if(!BCrypt.Net.BCrypt.Verify(passwordChangeDto.CurrentPassword, user.Password)) return false;

            user.Password = BCrypt.Net.BCrypt.HashPassword(passwordChangeDto.NewPassword);

            _userRepository.Update(user);

            return true;
        }

        public UserResponseDto FindUserById(Guid id)
        {
            var user = _userRepository.FindById(id);

            if (user == null) throw new NotFoundException("User with sent id doesnt exist!");

            return _userMapper.UserToUserDto(user);
        }

        public List<UserResponseDto> UserList()
        {
            throw new NotImplementedException();
        }

        public List<UserResponseDto> UserFilterList(Dictionary<string, string> filters)
        {
            throw new NotImplementedException();
        }

    }
}
