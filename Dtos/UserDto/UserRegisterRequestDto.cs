namespace NetKubernet.Dtos.UserDtos;

public class UserRegisterRequestDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? Username { get; internal set; }
    public string? Password { get; set; }

}