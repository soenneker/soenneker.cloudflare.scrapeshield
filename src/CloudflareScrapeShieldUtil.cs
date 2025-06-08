using Microsoft.Extensions.Logging;
using Soenneker.Cloudflare.OpenApiClient;
using Soenneker.Cloudflare.OpenApiClient.Models;
using Soenneker.Cloudflare.ScrapeShield.Abstract;
using Soenneker.Cloudflare.Utils.Client.Abstract;
using Soenneker.Extensions.Task;
using Soenneker.Extensions.ValueTask;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.ScrapeShield;

/// <summary>
/// Implementation of <see cref="ICloudflareScrapeShieldUtil"/> for managing Cloudflare Hotlink Protection settings
/// </summary>
public sealed class CloudflareScrapeShieldUtil : ICloudflareScrapeShieldUtil
{
    private readonly ICloudflareClientUtil _clientUtil;
    private readonly ILogger<CloudflareScrapeShieldUtil> _logger;
    private const string HotlinkProtectionSettingId = "hotlink_protection";

    public CloudflareScrapeShieldUtil(ICloudflareClientUtil clientUtil, ILogger<CloudflareScrapeShieldUtil> logger)
    {
        _clientUtil = clientUtil;
        _logger = logger;
    }

    public async ValueTask<Zone_settings_get_single_setting_Response_200_application_json> GetHotlinkProtectionSettings(string zoneId, CancellationToken cancellationToken = default)
    {
        try
        {
            CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
            return await client.Zones[zoneId].Settings[HotlinkProtectionSettingId].GetAsync(cancellationToken: cancellationToken).NoSync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting Hotlink Protection settings for zone {ZoneId}", zoneId);
            throw;
        }
    }

    public async ValueTask<Zone_settings_edit_single_setting_Response_200_application_json> UpdateHotlinkProtectionSettings(string zoneId, Zone_settings_get_single_setting_Response_200_application_json settings, CancellationToken cancellationToken = default)
    {
        try
        {
            CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
            var request = new Zones_zone_settings_single_request
            {
                Type = settings.Result?.Type == "on" ? "on" : "off"
            };
            return await client.Zones[zoneId].Settings[HotlinkProtectionSettingId].PatchAsync(request, cancellationToken: cancellationToken).NoSync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating Hotlink Protection settings for zone {ZoneId}", zoneId);
            throw;
        }
    }

    public async ValueTask<Zone_settings_edit_single_setting_Response_200_application_json> EnableHotlinkProtection(string zoneId, CancellationToken cancellationToken = default)
    {
        try
        {
            CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
            var request = new Zones_zone_settings_single_request
            {
                Type = "on"
            };
            return await client.Zones[zoneId].Settings[HotlinkProtectionSettingId].PatchAsync(request, cancellationToken: cancellationToken).NoSync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error enabling Hotlink Protection for zone {ZoneId}", zoneId);
            throw;
        }
    }

    public async ValueTask<Zone_settings_edit_single_setting_Response_200_application_json> DisableHotlinkProtection(string zoneId, CancellationToken cancellationToken = default)
    {
        try
        {
            CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
            var request = new Zones_zone_settings_single_request
            {
                Type = "off"
            };
            return await client.Zones[zoneId].Settings[HotlinkProtectionSettingId].PatchAsync(request, cancellationToken: cancellationToken).NoSync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error disabling Hotlink Protection for zone {ZoneId}", zoneId);
            throw;
        }
    }
}