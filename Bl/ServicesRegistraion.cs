using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NsTask.Api.Data;
using NsTask.Api.Domain.Enteties;

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

            //services.AddScoped<IBusinessUnitRepository, BusinessUnitManager>();
            //services.AddScoped<IDbEntityRepository, DbEntityManager>();

            return services;
        }
    }
}
