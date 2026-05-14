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

    public async ValueTask<ZoneSettingsGetSingleSetting200?> GetHotlinkProtectionSettings(string zoneId, CancellationToken cancellationToken = default)
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

    public async ValueTask<ZoneSettingsEditSingleSetting200?> UpdateHotlinkProtectionSettings(string zoneId, ZoneSettingsGetSingleSetting200 settings, CancellationToken cancellationToken = default)
    {
        try
        {
            CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
            var request = new ZonesZoneSettingsSingleRequest
            {
                ZonesZoneSettingsSingleRequestMember2 = new ZonesZoneSettingsSingleRequestMember2
                {
                    Value = new ZonesSettingValue
                    {
                        ZonesHotlinkProtectionValueWrapper = new ZonesHotlinkProtectionValue_Wrapper
                        {
                            Value = settings.Result?.ZonesHotlinkProtection?.Value == ZonesHotlinkProtectionValue.On ? ZonesHotlinkProtectionValue.On : ZonesHotlinkProtectionValue.Off
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

    public async ValueTask<ZoneSettingsEditSingleSetting200?> EnableHotlinkProtection(string zoneId, CancellationToken cancellationToken = default)
    {
        try
        {
            CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
            var request = new ZonesZoneSettingsSingleRequest
            {
                ZonesZoneSettingsSingleRequestMember2 = new ZonesZoneSettingsSingleRequestMember2
                {
                    Value = new ZonesSettingValue
                    {
                        ZonesHotlinkProtectionValueWrapper = new ZonesHotlinkProtectionValue_Wrapper
                        {
                            Value = ZonesHotlinkProtectionValue.On
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

    public async ValueTask<ZoneSettingsEditSingleSetting200?> DisableHotlinkProtection(string zoneId, CancellationToken cancellationToken = default)
    {
        try
        {
            CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
            var request = new ZonesZoneSettingsSingleRequest
            {
                ZonesZoneSettingsSingleRequestMember2 = new ZonesZoneSettingsSingleRequestMember2
                {
                    Value = new ZonesSettingValue
                    {
                        ZonesHotlinkProtectionValueWrapper = new ZonesHotlinkProtectionValue_Wrapper
                        {
                            Value = ZonesHotlinkProtectionValue.Off
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