
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Talabat_API.Errors;
using Talabat_API.MiddleWare;
using Talabat_API.ProfileMap;
using Talabat_Core.Models;
using Talabat_Core.Repositories_InterFaces;
using Talabat_Repository.Data;
using Talabat_Repository.RepositoreisClasses;

namespace Talabat_API

{
    public class Program
    {
        public async static Task Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            builder.Services.AddDbContext<StoreContext>(
                 options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                );
            builder.Services.AddScoped<IGenericIcs<Product>, GenericRepo<Product>>();
            builder.Services.AddScoped<IGenericIcs<ProductBrand>, GenericRepo<ProductBrand>>();
            builder.Services.AddScoped<IGenericIcs<ProductType>, GenericRepo<ProductType>>();

            builder.Services.Configure<ApiBehaviorOptions>(
                 options =>
                 {
                     options.InvalidModelStateResponseFactory =
                     (actioncontext) =>
                     {
                         var errors = actioncontext.ModelState.Where(e => e.Value.Errors.Count() > 0)
                         .SelectMany(p => p.Value.Errors).Select(e => e.ErrorMessage).ToList();
                         var response = new ApiValidationErrorResponse()
                         {
                             Errors = errors
                         };
                         return new BadRequestObjectResult(response);   
                     };



                 }
                );

            //builder.Services.AddScoped(typeof(IGenericIcs<>), typeof(GenericRepo<>));

            var app = builder.Build();

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;//resolve the serices that you want to use as a depedndency injection
            var _dbcontext = services.GetRequiredService<StoreContext>();

            var loggerfactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbcontext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbcontext);
            }
            catch (Exception ex)
            {
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex, "Error Occured During Migration");

            }



            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleWare>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseDeveloperExceptionPage();

            app.MapControllers();

            app.Run();
        }
    }
}
