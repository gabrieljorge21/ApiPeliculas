using ApiPelículas.Data;
using ApiPelículas.PeliculasMappers;
using ApiPelículas.Repository;
using ApiPelículas.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

// Add repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(PeliculasMapper));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
