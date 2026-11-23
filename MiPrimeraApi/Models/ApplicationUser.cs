using Microsoft.AspNetCore.Identity;

namespace MiPrimeraApi.Models;

public class ApplicationUser : IdentityUser
{
    public string NombreCompleto { get; set; } = string.Empty;
}