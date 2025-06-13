using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Repositories;
using VDG_Web_Api.src.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder();

// Add services to the container.
builder.Services.AddControllers();

//builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument();
// Our App Services
builder.Services.AddDbContext<VdgDbDemoContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddSwaggerGen();

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
