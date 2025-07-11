using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VDG_Web_Api.src;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Repositories;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services;
using VDG_Web_Api.src.Services.Interfaces;

var builder = WebApplication.CreateBuilder();

// Add services to the container.
builder.Services.AddControllers();

//builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument();
// Our App Services
builder.Services.AddDbContext<VdgDbDemoContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();

builder.Services.AddScoped<IVirtualClinicRepository, VirtualClinicRepository>();
builder.Services.AddScoped<IVirtualClinicService, VirtualClinicService>();

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();

builder.Services.AddScoped<ISpecialityRepository, SpecialityRepository>();

builder.Services.AddScoped<IRatingRepository, RatingRepositroy>();
builder.Services.AddScoped<IRatingService, RatingService>();

JWTOptions JwtConfig = builder.Configuration.GetSection("JWT")
	.Get<JWTOptions>()!;

builder.Services.AddSingleton(JwtConfig);

builder.Services.AddAuthentication() // add authentication to the builder
	.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
	{ // add authentication option (JWT Bearer)
		opt.SaveToken = true;
		opt.TokenValidationParameters = new()
		{ // setting up validation params
			ValidateIssuer = true, // Ensures that the issuer of the token matches the expected issuer
			ValidIssuer = JwtConfig.Issuer,
			ValidateAudience = true,
			ValidAudience = JwtConfig.Audience,
			ValidateIssuerSigningKey = true, //  Validates that the signing key used to sign the token matches our signing key
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.SigningKey!)),
			ValidateLifetime = true,
			ClockSkew = TimeSpan.FromMinutes(1) // allowing only 1min difference
		};
	});


builder.Services.AddScoped<IAuthService, JWTAuthService>();

builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication()
	.AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("Basic", null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	//app.UseOpenApi();
	app.UseSwagger();
	app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
