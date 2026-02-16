namespace Discounts.Application.DTOs.Request;

public class UserUpdateDto
{
    public string Email { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
}
