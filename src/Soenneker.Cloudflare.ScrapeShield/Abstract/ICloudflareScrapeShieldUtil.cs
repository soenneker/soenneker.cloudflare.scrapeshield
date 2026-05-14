using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Soenneker.Cloudflare.Utils.Client.Abstract;
using Soenneker.Cloudflare.OpenApiClient.Models;

namespace Soenneker.Cloudflare.ScrapeShield.Abstract;

/// <summary>
/// Interface for managing Cloudflare Hotlink Protection settings
/// </summary>
public interface ICloudflareScrapeShieldUtil
{
    /// <summary>
    /// Gets the current Hotlink Protection settings for a zone
    /// </summary>
    /// <param name="zoneId">The zone identifier</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>The current Hotlink Protection settings</returns>
    ValueTask<ZoneSettingsGetSingleSetting200?> GetHotlinkProtectionSettings(string zoneId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the Hotlink Protection settings for a zone
    /// </summary>
    /// <param name="zoneId">The zone identifier</param>
    /// <param name="settings">The new settings to apply</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>The updated Hotlink Protection settings</returns>
    ValueTask<ZoneSettingsEditSingleSetting200?> UpdateHotlinkProtectionSettings(string zoneId, ZoneSettingsGetSingleSetting200 settings, CancellationToken cancellationToken = default);

    /// <summary>
    /// Enables Hotlink Protection for a zone
    /// </summary>
    /// <param name="zoneId">The zone identifier</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>The updated Hotlink Protection settings</returns>
    ValueTask<ZoneSettingsEditSingleSetting200?> EnableHotlinkProtection(string zoneId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Disables Hotlink Protection for a zone
    /// </summary>
    /// <param name="zoneId">The zone identifier</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>The updated Hotlink Protection settings</returns>
    ValueTask<ZoneSettingsEditSingleSetting200?> DisableHotlinkProtection(string zoneId, CancellationToken cancellationToken = default);
}