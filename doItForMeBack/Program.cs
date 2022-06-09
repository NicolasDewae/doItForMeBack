using doItForMeBack.Data;
using doItForMeBack.Service;
using doItForMeBack.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
// La m�thode "UseSqlServer" r�cup�re la chaine de connexion pour se connecter � la base de donn�es
var connectionString = builder.Configuration.GetConnectionString("DoItForMeDatabase");
builder.Services.AddDbContext<DataContext>(o =>
{
    o.UseSqlServer(connectionString);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
