
using LabbAPI.Data;
using LabbAPI.Repository.Irepository;
using Microsoft.EntityFrameworkCore;
using LabbAPI.Repository;
using LabbAPI.Models;

namespace LabbAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IRepository<Person>, Repository<Person>>();
            builder.Services.AddScoped<IRepository<Interest>, Repository<Interest>>();
            builder.Services.AddScoped<IRepository<PersonInterest>, Repository<PersonInterest>>();
            builder.Services.AddScoped<IRepository<WebURL>, Repository<WebURL>>();
            builder.Services.AddControllers().AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingConfig));  // Mappning lagt i en egen class ovanför program.cs
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
        }
    }
}