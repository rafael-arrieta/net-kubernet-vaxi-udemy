using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetKubernet.Data.Users;
using NetKubernet.Dtos.UserDtos;

namespace NetKubernet.Controllers;

[Route("api/[controller]")]

[ApiController]
public class UserController : ControllerBase
{
     private readonly IUserRepository _repository;
     public UserController(IUserRepository repository)
     {
        _repository = repository;
     }

    [AllowAnonymous]
    
    [HttpPost("login")]
    public async Task<ActionResult<UserResponseDto>> Login(
        [FromBody] UserLoginRequestDto request
    ){
        return await _repository.Login(request);
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<ActionResult<UserResponseDto>> Register(
        [FromBody] UserRegisterRequestDto request
    ){
        return await _repository.UserRegister(request);
    }

    [HttpGet]
    public async Task<ActionResult<UserResponseDto>> GetUser(){
        return await _repository.GetUser();
    }
}