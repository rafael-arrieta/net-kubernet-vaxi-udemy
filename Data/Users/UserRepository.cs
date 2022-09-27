using Microsoft.AspNetCore.Identity;
using NetKubernet.Dtos.UserDtos;
using NetKubernet.Token;
using NetKubernet.Models;
using NetKubernet.Middleware;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace NetKubernet.Data.Users;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    private readonly IJwtGenerator _jwtGenerator;

    private readonly AppDbContext _context;

    private readonly IUserSession _userSession;

    public UserRepository(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IJwtGenerator jwtGenerator,
        AppDbContext context,
        IUserSession userSession
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _context = context;
        _userSession = userSession;
    }

    private UserResponseDto TransformerUserToUserDto(User user){

        return new UserResponseDto{
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Phone = user.Phone,
            Email = user.Email,
            UserName = user.UserName,
            Token = _jwtGenerator.CreateToken(user)
        };
    }
    public async Task<UserResponseDto> GetUser()
    {
        var user = await _userManager.FindByNameAsync(_userSession.GetUserSession());
        if (user == null)
        {
            throw new MiddlewareException(
                HttpStatusCode.Unauthorized, 
                new{message = "The User token don't exist on database" }
            );
        }
        return TransformerUserToUserDto(user!);
    }

    public async Task<UserResponseDto> Login(UserLoginRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email!);

        if (user == null)
        {
            throw new MiddlewareException(
                HttpStatusCode.Unauthorized, 
                new{message = "The User email don't exist on database" }
            );
        }


        var result = await _signInManager.CheckPasswordSignInAsync(user!, request.Password!, false);

        if(result.Succeeded)
        {
            return TransformerUserToUserDto(user);
        }

        throw new MiddlewareException(
            HttpStatusCode.Unauthorized,
            new {message = "The credentials are incorrect"}
        );

    }

    public async Task<UserResponseDto> UserRegister(UserRegisterRequestDto request)
    {
        var emailExist = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
        if(emailExist){
            throw new MiddlewareException(
            HttpStatusCode.BadRequest,
            new {message = "The user Email already exists on database"}
        );
        }

        var usernameExist = await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
        if(usernameExist){
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new {message = "The username already exists on database"}
        );
        } 
        var user = new User {
            Name = request.Name,
            Surname = request.Surname,
            Phone = request.Phone,
            Email = request.Email,
            UserName = request.UserName
        };

        var result = await _userManager.CreateAsync(user!, request.Password!);
        if(result.Succeeded)
        {
        return TransformerUserToUserDto(user);
        }

        throw new Exception ("Can't register the user");
    }
}