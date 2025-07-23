using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VDG_Web_Api.src;
using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;
using VDG_Web_Api.src.Services.JWTService;

public class JWTAuthService : IAuthService
{
	private readonly JWTOptions _jwtOptions;
	private readonly IUserRepository _usersRepository;
	private readonly SymmetricSecurityKey _key;

	public JWTAuthService(JWTOptions options, IUserRepository usersRepository)
	{
		_jwtOptions = options;
		_usersRepository = usersRepository;
		_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey));
	}

	public async Task<AuthResponse> AuthenticateAsync(UserLogin login)
	{
		var user = await ValidateUser(login);

		if (user == null)
		{
			throw new AuthenticationFailureException("Invalid username or password");
		}

		var tokenHandler = new JwtSecurityTokenHandler();

		Claim[] claims = [new (ClaimTypes.NameIdentifier, user.Id.ToString()),
			new (ClaimTypes.Name, $"{user.Person.FirstName};{user.Person.LastName}"),
			new (ClaimTypes.Email, user.Email),
			new (ClaimTypes.Role, user.Role.ToString())];

		var tokenDiscriptor = new SecurityTokenDescriptor()
		{
			Issuer = _jwtOptions.Issuer,
			Audience = _jwtOptions.Audience,
			Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.Expiration),
			SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256),
			Subject = new ClaimsIdentity(claims)
		};

		var token = tokenHandler.CreateToken(tokenDiscriptor);

		AuthResponse response = new()
		{
			Token = new()
			{
				AccessToken = tokenHandler.WriteToken(token),
				ExpiresIn = _jwtOptions.Expiration
			},
			User = user.ToDto()
		};

		return response;
	}

	public async Task<User?> ValidateUser(UserLogin userLogin)
	{
		var user = await _usersRepository.GetByEmail(userLogin.Email);

		if (user == null)
		{
			return null!;
		}

		if (user.PasswordHash != userLogin.Password)
		{
			return null!;
		}

		return user;
	}
}
