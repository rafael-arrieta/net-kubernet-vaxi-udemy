using NetKubernet.Dtos.UserDtos;
namespace NetKubernet.Data.Users;
public interface IUserRepository 
{
    Task<UserResponseDto> GetUser();
    Task<UserResponseDto> Login(UserLoginRequestDto request); 
    Task<UserResponseDto> UserRegister(UserRegisterRequestDto request);
}