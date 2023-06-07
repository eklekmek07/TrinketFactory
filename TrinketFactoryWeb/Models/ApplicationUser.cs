using Microsoft.AspNetCore.Identity;

namespace TrinketFactoryWeb.Models;

public class ApplicationUser : IdentityUser
{
    public string? Name;
}