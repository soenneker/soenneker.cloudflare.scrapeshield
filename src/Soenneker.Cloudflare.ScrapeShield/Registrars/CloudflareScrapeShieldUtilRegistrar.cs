using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Cloudflare.ScrapeShield.Abstract;
using Soenneker.Cloudflare.Utils.Client.Registrars;

namespace Soenneker.Cloudflare.ScrapeShield.Registrars;

/// <summary>
/// A utility for managing Cloudflare Scrape Shield settings
/// </summary>
public static class CloudflareScrapeShieldUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="ICloudflareScrapeShieldUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddCloudflareScrapeShieldUtilAsSingleton(this IServiceCollection services)
    {
        services.AddCloudflareClientUtilAsSingleton().TryAddSingleton<ICloudflareScrapeShieldUtil, CloudflareScrapeShieldUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="ICloudflareScrapeShieldUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddCloudflareScrapeShieldUtilAsScoped(this IServiceCollection services)
    {
        services.AddCloudflareClientUtilAsSingleton().TryAddScoped<ICloudflareScrapeShieldUtil, CloudflareScrapeShieldUtil>();

        return services;
    }
}
