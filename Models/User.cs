using Microsoft.AspNetCore.Identity;

namespace NetKubernet.Models;

public class User : IdentityUser {

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Phone { get; set; }

}