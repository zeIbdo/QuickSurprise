
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuickSurpriseRemake.Models;
using QuickSurpriseRemake.Services;
using QuickSurpriseRemake.Settings;
using System.Configuration;

namespace QuickSurpriseRemake
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<XmlSettings>(builder.Configuration.GetSection("XmlSettings"));
            builder.Services.AddScoped<IChangeService<Person>, XmlChangeService<Person>>(provider =>
            {
                var xmlSettings = provider.GetRequiredService<IOptions<XmlSettings>>().Value;
                return new XmlChangeService<Person>(xmlSettings.PersonXmlFilePath);
            });
            //builder.Services.Configure<JsonSettings>(builder.Configuration.GetSection("JsonSettings"));
            //builder.Services.AddScoped<IChangeService<Person>, JsonChangeService<Person>>(provider =>
            //{
            //    var jsonSettings = provider.GetRequiredService<IOptions<JsonSettings>>().Value;
            //    return new JsonChangeService<Person>(jsonSettings.PersonJsonFilePath);
            //});
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
