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
    ValueTask<Zone_settings_get_single_setting_200> GetHotlinkProtectionSettings(string zoneId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the Hotlink Protection settings for a zone
    /// </summary>
    /// <param name="zoneId">The zone identifier</param>
    /// <param name="settings">The new settings to apply</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>The updated Hotlink Protection settings</returns>
    ValueTask<Zone_settings_edit_single_setting_200> UpdateHotlinkProtectionSettings(string zoneId, Zone_settings_get_single_setting_200 settings, CancellationToken cancellationToken = default);

    /// <summary>
    /// Enables Hotlink Protection for a zone
    /// </summary>
    /// <param name="zoneId">The zone identifier</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>The updated Hotlink Protection settings</returns>
    ValueTask<Zone_settings_edit_single_setting_200> EnableHotlinkProtection(string zoneId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Disables Hotlink Protection for a zone
    /// </summary>
    /// <param name="zoneId">The zone identifier</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>The updated Hotlink Protection settings</returns>
    ValueTask<Zone_settings_edit_single_setting_200> DisableHotlinkProtection(string zoneId, CancellationToken cancellationToken = default);
}