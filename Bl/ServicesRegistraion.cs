using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NsTask.Api.Bl.Interfaces;
using NsTask.Api.Bl.Services;
using NsTask.Api.Data;
using NsTask.Api.Domain.Enteties;
using NsTask.Api.Helpers;
using System.Configuration;
using System.Text;

namespace NsTask.Api.Bl
{
    public static class ServicesRegistraion
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("NsasDb")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"), o => o.EnableRetryOnFailure())
                .EnableSensitiveDataLogging(true)
                .EnableDetailedErrors(true));

            services.AddIdentity<ApplicationUser, IdentityRole>(
               options => options.SignIn.RequireConfirmedAccount = false)
               .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<INsasTaskRepository, NsasTaskRepository>();
            services.AddScoped<IAuthServices, AuthServices>();

            // to map between class JWT in Helpers  and the section JWT in appsettings.json
            services.Configure<JWT>(configuration.GetSection("JWT"));



            // to register JWT service
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(o =>
              {
                  o.RequireHttpsMetadata = false;
                  o.SaveToken = false;
                  o.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidIssuer = configuration["JWT:Issuer"],
                      ValidAudience = configuration["JWT:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                  };
              });

            //// to all accessing data for outside origin (vue js)
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("foo",
            //    builder =>
            //    {
            //        // Not a permanent solution, but just trying to isolate the problem
            //        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

            //    });
            //});

            return services;
        }
    }
}
