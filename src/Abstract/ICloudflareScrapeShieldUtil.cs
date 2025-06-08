using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Soenneker.Cloudflare.Utils.Client.Abstract;
using Soenneker.Cloudflare.OpenApiClient.Models;

namespace Soenneker.Cloudflare.ScrapeShield.Abstract;

/// <summary>
/// Utility for managing Cloudflare Scrape Shield settings
/// </summary>
public interface ICloudflareScrapeShieldUtil
{
    /// <summary>
    /// Gets the current Scrape Shield settings for a zone
    /// </summary>
    /// <param name="zoneId">The zone identifier</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>The current Scrape Shield settings</returns>
    ValueTask<Zone_settings_get_single_setting_Response_200_application_json> GetSettings(string zoneId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the Scrape Shield settings for a zone
    /// </summary>
    /// <param name="zoneId">The zone identifier</param>
    /// <param name="settings">The new settings to apply</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>The updated Scrape Shield settings</returns>
    ValueTask<Zone_settings_edit_single_setting_Response_200_application_json> UpdateSettings(string zoneId, Zone_settings_get_single_setting_Response_200_application_json settings, CancellationToken cancellationToken = default);

    /// <summary>
    /// Enables Scrape Shield for a zone
    /// </summary>
    /// <param name="zoneId">The zone identifier</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>The updated Scrape Shield settings</returns>
    ValueTask<Zone_settings_edit_single_setting_Response_200_application_json> Enable(string zoneId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Disables Scrape Shield for a zone
    /// </summary>
    /// <param name="zoneId">The zone identifier</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>The updated Scrape Shield settings</returns>
    ValueTask<Zone_settings_edit_single_setting_Response_200_application_json> Disable(string zoneId, CancellationToken cancellationToken = default);
}