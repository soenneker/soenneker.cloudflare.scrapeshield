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
    private const string _hotlinkProtectionSettingId = "hotlink_protection";

    public CloudflareScrapeShieldUtil(ICloudflareClientUtil clientUtil, ILogger<CloudflareScrapeShieldUtil> logger)
    {
        _clientUtil = clientUtil;
        _logger = logger;
    }

    public async ValueTask<Zone_settings_get_single_setting_200?> GetHotlinkProtectionSettings(string zoneId, CancellationToken cancellationToken = default)
    {
        try
        {
            CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
            return await client.Zones[zoneId].Settings[_hotlinkProtectionSettingId].GetAsync(cancellationToken: cancellationToken).NoSync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting Hotlink Protection settings for zone {ZoneId}", zoneId);
            throw;
        }
    }

    public async ValueTask<Zone_settings_edit_single_setting_200?> UpdateHotlinkProtectionSettings(string zoneId, Zone_settings_get_single_setting_200 settings, CancellationToken cancellationToken = default)
    {
        try
        {
            CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
            var request = new Zones_zone_settings_single_request
            {
                ZonesZoneSettingsSingleRequestMember2 = new Zones_zone_settings_single_requestMember2
                {
                    Value = new Zones_setting_value
                    {
                        ZonesHotlinkProtectionValueWrapper = new Zones_hotlink_protection_value_Wrapper
                        {
                            Value = settings.Result?.ZonesHotlinkProtection?.Value == Zones_hotlink_protection_value.On ? Zones_hotlink_protection_value.On : Zones_hotlink_protection_value.Off
                        }
                    }
                }
            };
            return await client.Zones[zoneId].Settings[_hotlinkProtectionSettingId].PatchAsync(request, cancellationToken: cancellationToken).NoSync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating Hotlink Protection settings for zone {ZoneId}", zoneId);
            throw;
        }
    }

    public async ValueTask<Zone_settings_edit_single_setting_200?> EnableHotlinkProtection(string zoneId, CancellationToken cancellationToken = default)
    {
        try
        {
            CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
            var request = new Zones_zone_settings_single_request
            {
                ZonesZoneSettingsSingleRequestMember2 = new Zones_zone_settings_single_requestMember2
                {
                    Value = new Zones_setting_value
                    {
                        ZonesHotlinkProtectionValueWrapper = new Zones_hotlink_protection_value_Wrapper
                        {
                            Value = Zones_hotlink_protection_value.On
                        }
                    }
                }
            };
            return await client.Zones[zoneId].Settings[_hotlinkProtectionSettingId].PatchAsync(request, cancellationToken: cancellationToken).NoSync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error enabling Hotlink Protection for zone {ZoneId}", zoneId);
            throw;
        }
    }

    public async ValueTask<Zone_settings_edit_single_setting_200?> DisableHotlinkProtection(string zoneId, CancellationToken cancellationToken = default)
    {
        try
        {
            CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
            var request = new Zones_zone_settings_single_request
            {
                ZonesZoneSettingsSingleRequestMember2 = new Zones_zone_settings_single_requestMember2
                {
                    Value = new Zones_setting_value
                    {
                        ZonesHotlinkProtectionValueWrapper = new Zones_hotlink_protection_value_Wrapper
                        {
                            Value = Zones_hotlink_protection_value.Off
                        }
                    }
                }
            };
            return await client.Zones[zoneId].Settings[_hotlinkProtectionSettingId].PatchAsync(request, cancellationToken: cancellationToken).NoSync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error disabling Hotlink Protection for zone {ZoneId}", zoneId);
            throw;
        }
    }
}