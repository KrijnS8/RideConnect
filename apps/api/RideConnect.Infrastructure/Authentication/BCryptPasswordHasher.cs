using RideConnect.Application.Features.Authentication.Interfaces;

namespace RideConnect.Infrastructure.Authentication;

public class BCryptPasswordHasher : IPasswordHasher
{
	public string Hash(string password) =>
		BCrypt.Net.BCrypt.HashPassword(password);

	public bool Verify(string hashedPassword, string providedPassword) =>
		BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
}
