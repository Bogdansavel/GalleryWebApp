using Autofac;
using FluentValidation;
using FluentValidation.AspNetCore;
using GalleryWebApp.Data;
using GalleryWebApp.Data.JWT;
using GalleryWebApp.Data.JWT.Contracts;
using GalleryWebApp.DI;
using GalleryWebApp.Middlewares;
using GalleryWebApp.Models;
using GalleryWebApp.Models.Validations;
using GalleryWebApp.Repositories;
using GalleryWebApp.Repositories.Contracts;
using GalleryWebApp.Services;
using GalleryWebApp.Services.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GalleryWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            */

            services.AddCustomCors(Configuration.GetPolicyConfigs());

            string connection = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<DataContext>(options => options.UseNpgsql(connection));
            services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("InMem"));

            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    )
                    .AddFluentValidation();

            var builder = services.AddIdentityCore<User>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddRoles<IdentityRole>();
            identityBuilder.AddRoleManager<RoleManager<IdentityRole>>();
            identityBuilder.AddEntityFrameworkStores<DataContext>();
            identityBuilder.AddSignInManager<SignInManager<User>>();

            var authOptions = Configuration.GetSection("AuthOptions").Get<AuthOptions>();
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(token =>
            {
                token.RequireHttpsMetadata = false;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = authOptions.ValidateIssuer,
                    ValidIssuer = authOptions.ValidIssuer,
                    ValidateAudience = authOptions.ValidateAudience,
                    ValidAudience = authOptions.ValidAudience,
                    ValidateLifetime = authOptions.ValidateLifetime,
                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = authOptions.ValidateIssuerSigningKey
                };
            });
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IJwtGenerator, JwtGenerator>();

            //services.AddTransient<IValidator<LoginQuery>, LoginQueryValidation>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ApplicationModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Images")),
                RequestPath = "/Images"
            });

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("DefaultCorsPolicy");

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
