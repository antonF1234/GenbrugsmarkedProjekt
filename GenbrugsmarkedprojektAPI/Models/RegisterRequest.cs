using System.ComponentModel.DataAnnotations;

namespace GenbrugsmarkedprojektAPI.Models;

public class OpretBrugerRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Navn { get; set; } = string.Empty;

    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;
}
