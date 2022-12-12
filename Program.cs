using Microsoft.EntityFrameworkCore;
using dotnet_test.Models;
using dotnet_test.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add dependencies
// scoped
builder.Services.AddScoped<IUserService, UserService>();


// Add database connection for postgresql // run 'dotnet ef database update' if you've changed the table 
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<UserContext>(options => { //
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
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
