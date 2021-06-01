
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

    }
}