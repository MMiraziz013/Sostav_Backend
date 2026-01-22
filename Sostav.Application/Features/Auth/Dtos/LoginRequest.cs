using System.ComponentModel.DataAnnotations;

namespace Sostav.Application.Features.Auth.Dtos;

public class LoginRequest
{
    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
}
