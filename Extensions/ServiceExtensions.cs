
using System.Collections.Generic;
using AspNetCoreRateLimit;
using dotnet_rpg.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_rpg.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                // To Send api-version in headers instead of send it in url
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");

                // If we have a lot of versions of a single controller, we can assign these
                // versions in the configuration instead:
                // Now, we can remove the [ApiVersion] attribute from the controllers.
                // opt.Conventions.Controller<CharacterController>().HasApiVersion(new ApiVersion(1, 0));
                // opt.Conventions.Controller<CharacterV2Controller>().HasDeprecatedApiVersion(new ApiVersion(2, 0));
            });
        }

        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            // it will show in header response too
            var rateLimitRules = new List<RateLimitRule>
                {
                    new RateLimitRule
                        {
                        Endpoint = "*",//any endpoint in our API
                        Limit= 50, //50 request
                        Period = "5m" // per 5 minutes
                        }
                };
            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.GeneralRules = rateLimitRules;
            });
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }


    }
}