using GalleryWebApp.Configs;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp
{
    public static class Extensions
    {
        private static readonly string CorsSectionName = "corsPolicies";

        public static IServiceCollection AddCustomCors(this IServiceCollection services, PolicyConfig[] policyConfigs)
        {
            services.AddCors(options =>
            {
                foreach (var policyConfig in policyConfigs)
                {
                    options.AddPolicy(policyConfig.Name, cors =>
                    {
                        cors = SetupCorsPolicyBuilder(policyConfig, cors);
                    });
                };
            });

            return services;
        }

        public static PolicyConfig[] GetPolicyConfigs(this IConfiguration configuration)
            => configuration.GetSection(CorsSectionName).Get<PolicyConfig[]>();

        private static CorsPolicyBuilder SetupCorsPolicyBuilder(PolicyConfig policyConfig, CorsPolicyBuilder cors)
        {
            if (policyConfig.Origins is null)
            {
                cors.AllowAnyOrigin();
            }
            else
            {
                cors.WithOrigins(policyConfig.Origins);
            }

            if (policyConfig.Methods is null)
            {
                cors.AllowAnyMethod();
            }
            else
            {
                cors.WithMethods(policyConfig.Methods);
            }

            if (policyConfig.Headers is null)
            {
                cors.AllowAnyHeader();
            }
            else
            {
                cors.WithHeaders(policyConfig.Headers);
            }

            if (policyConfig.AllowCredentials)
            {
                cors.AllowCredentials();
            }

            if (policyConfig.ExposedHeaders != null)
            {
                cors.WithExposedHeaders(policyConfig.ExposedHeaders);
            }

            return cors;
        }
    }
}
