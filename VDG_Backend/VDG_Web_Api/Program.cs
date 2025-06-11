using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Repositories;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services;
using VDG_Web_Api.src.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument();
// Our App Services
builder.Services.AddDbContext<VdgDbDemoContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IVirtualClinicService, VirtualClinicService>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IAuthService, BasicAuthService>();

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
