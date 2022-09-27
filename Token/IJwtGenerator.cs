using NetKubernet.Models;

namespace NetKubernet.Token;

public interface IJwtGenerator

{
    string BuildToken(User user);
    string CreateToken(User user);
}