using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Services.Interfaces;

[Obsolete("will be removed")]
public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
	private readonly IAuthService _authService;

	public BasicAuthHandler(IAuthService authService, IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
	{
		_authService = authService;
	}

	protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
	{
		if (!Request.Headers.ContainsKey("Authorization"))
		{
			return await Task.FromResult(AuthenticateResult.Fail("Must provide authorization header"));
		}

		var authHeader = Request.Headers["Authorization"].ToString();

		if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
		{
			return await Task.FromResult(AuthenticateResult.Fail("Auth type unsupported"));
		}

		// Email: creds[0], password: creds[1]
		string authCreds = authHeader["Basic ".Length..];
		var creds = Encoding.UTF8.GetString(Convert.FromBase64String(authCreds)).Split(':');

		if (creds is null || creds.Length < 2)
		{
			return await Task.FromResult(AuthenticateResult.Fail("Auth format is incorrect"));
		}

		UserLogin userLogin = new()
		{
			Email = creds[0],
			Password = creds[1]
		};

		try
		{
			User? user = await _authService.ValidateUser(userLogin);

			if (user == null)
			{
				throw new InvalidOperationException();
			}

			var userFullName = $"{user.Person.FirstName} {user.Person.LastName}";

			var claimsIdentity = new ClaimsIdentity(
			[
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
			new Claim(ClaimTypes.Name, userFullName),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Role, user.Role.ToString())
			], "Basic");

			var userPrincipal = new ClaimsPrincipal(claimsIdentity);

			var authTicket = new AuthenticationTicket(userPrincipal, "Basic");

			return await Task.FromResult(AuthenticateResult.Success(authTicket));
		}
		catch (Exception)
		{
			return await Task.FromResult(AuthenticateResult.Fail("Invalid email or password"));
		}
	}

}