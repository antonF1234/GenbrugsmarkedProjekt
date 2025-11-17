using System.ComponentModel.DataAnnotations;

namespace GenbrugsmarkedprojektAPI.Models;

public class LogIndRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
