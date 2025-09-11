using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using VDG_Web_Api.src;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.DTOs;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.FileHandler;
using VDG_Web_Api.src.Hubs;
using VDG_Web_Api.src.Repositories;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services;
using VDG_Web_Api.src.Services.Interfaces;
using VDG_Web_Api.src.Services.LocalizationService;
using VDG_Web_Api.src.Services.SearchService;

var builder = WebApplication.CreateBuilder();

// Add services to the container.
builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	});
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
//builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument();
// Our App Services
var cnnStr = builder.Configuration.GetConnectionString("Remote");
builder.Services.AddDbContext<VdgDbDemoContext>(opt => opt.UseSqlServer(cnnStr));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IVirtualClinicRepository, VirtualClinicRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ISpecialityRepository, SpecialityRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepositroy>();
builder.Services.AddScoped<IPromotionRequestRepository, PromotionRequestRepository>();
builder.Services.AddTransient<ILocalizationService, LocalizationService>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ISupportRepository, SupportRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

builder.Services.AddScoped<IClaimService, ClaimService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IVirtualClinicService, VirtualClinicService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ISpecialityService, SpecialityService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IPromotionRequestService, PromotionRequestService>();
builder.Services.AddScoped<ISearchingService, SearchingService>();
builder.Services.AddTransient<IFileHandler, FileHandler>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ISupportService, SupportService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
EmailConf emailConf = builder.Configuration.GetSection("Email").Get<EmailConf>()!;
builder.Services.AddFluentEmail(emailConf.SenderEmail)
	.AddSmtpSender(host: emailConf.Host, port: emailConf.Port);

JWTOptions JwtConfig = builder.Configuration.GetSection("JWT")
	.Get<JWTOptions>()!;

builder.Services.AddSingleton(JwtConfig);
builder.Services.AddHangfire(conf =>
{
	conf.UseSqlServerStorage("Server=db25465.public.databaseasp.net; Database=db25465; User Id=db25465; Password=q+2Z6nQ-pC@4; Encrypt=False; MultipleActiveResultSets=True;");
});
builder.Services.AddHangfireServer();

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

builder.Services.AddSignalR();


builder.Services.AddScoped<IAuthService, JWTAuthService>();
builder.Services.AddCors(x =>
{
	x.AddPolicy("Any", x => x.AllowAnyHeader()
	.AllowAnyMethod()
	.AllowAnyOrigin());
});
builder.Services.AddSwaggerGen(c =>
{

	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter token 'Bearer {token}'",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "bearer",
		BearerFormat = "JWT"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});

builder.Services.AddAuthorization(x => x.AddPolicy("Doctor-Admin", p =>
{
	p.RequireRole([UserRole.Doctor.ToString(), UserRole.Admin.ToString()]);
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//	//app.UseOpenApi();
app.UseSwagger();
app.UseSwaggerUi();
//}
app.UseHangfireDashboard();

app.UseHttpsRedirection();
app.MapHub<ChatHub>("chathub");
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.UseCors("Any");

app.Run();
