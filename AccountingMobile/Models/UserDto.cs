namespace AccountingMobile.Models;

public record UserDto
{
    public int UserId { get; init; }
    public string UserName { get; init; }
    public string Token { get; init; }
}