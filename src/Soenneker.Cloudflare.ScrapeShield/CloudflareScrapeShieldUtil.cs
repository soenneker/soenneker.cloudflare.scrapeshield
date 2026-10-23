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

/// <inheritdoc cref="ICloudflareScrapeShieldUtil" />
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

    public async ValueTask<ZoneSettingsGetSingleSetting200Response?> GetHotlinkProtectionSettings(string zoneId, CancellationToken cancellationToken = default)
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

    public async ValueTask<ZoneSettingsEditSingleSetting200Response?> UpdateHotlinkProtectionSettings(string zoneId, ZoneSettingsGetSingleSetting200Response settings, CancellationToken cancellationToken = default)
    {
        try
        {
            CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
            ZonesHotlinkProtectionValue value = Enum.Parse<ZonesHotlinkProtectionValue>(settings.Result?.ZonesHotlinkProtection?.Value?.ToString() ??
                                                   throw new InvalidOperationException("The supplied settings do not contain a Hotlink Protection value"));

            var request = new ZonesZoneSettingsSingleRequest
            {
                ZonesZoneSettingsSingleRequestOneOf2 = new ZonesZoneSettingsSingleRequestOneOf2
                {
                    Value = new ZonesSettingValue
                    {
                        ZonesHotlinkProtectionValue = value
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

    public async ValueTask<ZoneSettingsEditSingleSetting200Response?> EnableHotlinkProtection(string zoneId, CancellationToken cancellationToken = default)
    {
        try
        {
            CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
            var request = new ZonesZoneSettingsSingleRequest
            {
                ZonesZoneSettingsSingleRequestOneOf2 = new ZonesZoneSettingsSingleRequestOneOf2
                {
                    Value = new ZonesSettingValue
                    {
                        ZonesHotlinkProtectionValue = ZonesHotlinkProtectionValue.On
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

    public async ValueTask<ZoneSettingsEditSingleSetting200Response?> DisableHotlinkProtection(string zoneId, CancellationToken cancellationToken = default)
    {
        try
        {
            CloudflareOpenApiClient client = await _clientUtil.Get(cancellationToken).NoSync();
            var request = new ZonesZoneSettingsSingleRequest
            {
                ZonesZoneSettingsSingleRequestOneOf2 = new ZonesZoneSettingsSingleRequestOneOf2
                {
                    Value = new ZonesSettingValue
                    {
                        ZonesHotlinkProtectionValue = ZonesHotlinkProtectionValue.Off
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
