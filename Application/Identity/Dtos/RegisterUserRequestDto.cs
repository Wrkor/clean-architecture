namespace Application.Identity.Dtos;

public record RegisterUserRequestDto(
    string FullName,
    string UserName,
    string Email,
    string Password
);
