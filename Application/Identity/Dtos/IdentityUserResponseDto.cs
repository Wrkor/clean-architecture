namespace Application.Identity.Dtos;

public record IdentityUserResponseDto(
    string Username,
    string Email,
    string Token
);
