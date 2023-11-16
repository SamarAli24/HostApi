
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Services;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using HostProject.Mapping;
using Microsoft.AspNetCore.Authorization;
using Sieve.Services;
using Data.Models;

namespace HostProjectAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new GeneralProfile());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
   
            var getConnection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<hosteduContext>(options => options.UseSqlServer(getConnection));
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            services.AddScoped<SieveProcessor>();

            services.AddControllers();
            services.AddSharedInfrastructure(Configuration);

            services.AddSwaggerGen(config =>
            {
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,

        },
        new List<string>()
    }
});

                var titleBase = "Students API`s";
                var description = "This is a Open-API for Test operations";
                var License = new OpenApiLicense()
                {
                    Name = "License: Students"
                };
          

                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = titleBase + " v1",
                    Description = description,
                    License = License,
                });

                config.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = titleBase + " v2",
                    Description = description,
                    License = License,
                });


            });

            var provider = services.BuildServiceProvider();
            var configuration = provider.GetRequiredService<IConfiguration>();

            //services.AddCors(option =>
            //{
            //    var reactURl = configuration.GetValue<string>("reactUrl");
            //    option.AddDefaultPolicy(pol =>
            //    {
            //        pol.WithOrigins(reactURl).AllowAnyMethod().AllowAnyHeader();
            //    });
            //});
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Test v1");
                //config.SwaggerEndpoint("/swagger/v2/swagger.json", "Test v2");
            });
            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
