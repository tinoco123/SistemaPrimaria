using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaPrimaria.Areas.Identity.Data;
using SistemaPrimaria.Data;

[assembly: HostingStartup(typeof(SistemaPrimaria.Areas.Identity.IdentityHostingStartup))]
namespace SistemaPrimaria.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SistemaPrimariaDBContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SistemaPrimariaDBContextConnection")));

                services.AddDefaultIdentity<SistemaPrimariaAdministrator>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<SistemaPrimariaDBContext>();
            });
        }
    }
}