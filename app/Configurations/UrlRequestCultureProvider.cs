using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace TasteUfes.Configurations
{
    // https://riptutorial.com/asp-net-core/example/10301/set-request-culture-via-url-path
    public class UrlRequestCultureProvider : RequestCultureProvider
    {
        private static readonly Regex LocalePattern = new Regex(@"^[a-z]{2}(-[a-z]{2,4})?$", RegexOptions.IgnoreCase);

        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var url = httpContext.Request.Path;

            // Right now it's not possible to use httpContext.GetRouteData()
            // since it uses IRoutingFeature placed in httpContext.Features when
            // Routing Middleware registers. It's not set when the Localization Middleware
            // is called, so this example simply assumes the locale will always 
            // be located in the second segment of a path, like in /api/en-US/products
            var parts = httpContext.Request.Path.Value.Split('/');

            if (parts.Length < 3)
            {
                return Task.FromResult<ProviderCultureResult>(null);
            }

            if (!LocalePattern.IsMatch(parts[1]))
            {
                return Task.FromResult<ProviderCultureResult>(null);
            }

            var culture = parts[1];

            return Task.FromResult(new ProviderCultureResult(culture));
        }
    }
}