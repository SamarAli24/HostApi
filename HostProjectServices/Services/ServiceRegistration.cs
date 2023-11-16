using Services.Services.Authenticate;
using Services.Services;

using Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Services
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.AddTransient<IAuthenticateUserToken, AuthenticateUserToken>();
           
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
           
        }
    }
}
