
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text;
using Talabat_API.Errors;
using Talabat_API.MiddleWare;
using Talabat_API.ProfileMap;
using Talabat_Core;
using Talabat_Core.Models;
using Talabat_Core.Models.Identity;
using Talabat_Core.Repositories_InterFaces;
using Talabat_Core.ServiceInterfaces;
using Talabat_Repository;
using Talabat_Repository.Data;
using Talabat_Repository.Data.Identity;
using Talabat_Repository.RepositoreisClasses;
using Talabat_Service;

namespace Talabat_API

{
    public class Program
    {
        public async static Task Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);
            var _configuration=builder.Configuration;


            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson(options=>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }
            );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            builder.Services.AddDbContext<StoreContext>(
                 options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                );
            builder.Services.AddDbContext<AppIdentityDBContext>(
              options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))
          );
            builder.Services.AddSingleton<IConnectionMultiplexer>(
                (serviceprovider)=>
                {
                    var connection = builder.Configuration.GetConnectionString("Redis");
                    return ConnectionMultiplexer.Connect(connection);
                }
                );
            builder.Services.AddScoped(typeof(IBasketRepo), typeof(BasketRepo));

            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            builder.Services.AddScoped(typeof(IOrderRepo), typeof(OrderService));
            builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
            builder.Services.AddScoped(typeof(IPaymentService),typeof(PaymentService));
            builder.Services.AddScoped<IGenericIcs<Product>, GenericRepo<Product>>();
            builder.Services.AddScoped<IGenericIcs<ProductBrand>, GenericRepo<ProductBrand>>();
            builder.Services.AddScoped<IGenericIcs<ProductType>, GenericRepo<ProductType>>();

            builder.Services.AddSingleton<IResponseCacheService, ResponseCachedService>();

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
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:AuthKey"]?? string.Empty)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = _configuration["JWT:ValidIssuer"],
                        ValidAudience = _configuration["JWT:ValidAudience"]
                    };
              });

            builder.Services.AddIdentity<AppUser, IdentityRole>(
                options =>
                {

                }
                ).AddEntityFrameworkStores<AppIdentityDBContext>().AddDefaultTokenProviders();
            //builder.Services.AddScoped(typeof(IGenericIcs<>), typeof(GenericRepo<>));

            var app = builder.Build();

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;//resolve the serices that you want to use as a depedndency injection
            var _dbcontext = services.GetRequiredService<StoreContext>();
            var _identitydbccontext=services.GetRequiredService<AppIdentityDBContext>();
            var _usermanager = services.GetRequiredService<UserManager<AppUser>>();
            var loggerfactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await AppIdentityDBcontextSeed.SeedUserAsync(_usermanager);
                await _dbcontext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbcontext);
                await _identitydbccontext.Database.MigrateAsync();
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

            app.UseStatusCodePagesWithRedirects("/Errors/{0}");

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseDeveloperExceptionPage();

            app.MapControllers();

            app.Run();
        }
    }
}
