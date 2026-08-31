[![](https://img.shields.io/nuget/v/soenneker.cloudflare.scrapeshield.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.cloudflare.scrapeshield/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cloudflare.scrapeshield/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.cloudflare.scrapeshield/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.cloudflare.scrapeshield.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.cloudflare.scrapeshield/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cloudflare.scrapeshield/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.cloudflare.scrapeshield/actions/workflows/codeql.yml)

# Soenneker.Cloudflare.ScrapeShield

Reads, enables, disables, and updates Cloudflare Hotlink Protection for a zone.

## Installation

```bash
dotnet add package Soenneker.Cloudflare.ScrapeShield
```

## Configuration

```json
{
  "Cloudflare": {
    "ApiKey": "your-api-token"
  }
}
```

The token needs permission to read and edit zone settings for the target zone.

## Registration

```csharp
using Soenneker.Cloudflare.ScrapeShield.Registrars;

services.AddCloudflareScrapeShieldUtilAsScoped();
```

Singleton registration is available with `AddCloudflareScrapeShieldUtilAsSingleton()`.

## Usage

```csharp
using Soenneker.Cloudflare.ScrapeShield.Abstract;
using Soenneker.Cloudflare.OpenApiClient.Models;

ZoneSettingsGetSingleSetting200? current =
    await scrapeShield.GetHotlinkProtectionSettings(zoneId, cancellationToken);

await scrapeShield.EnableHotlinkProtection(zoneId, cancellationToken);

// Later, if the site needs to serve images to other origins:
await scrapeShield.DisableHotlinkProtection(zoneId, cancellationToken);
```

`UpdateHotlinkProtectionSettings` accepts a settings envelope returned by `GetHotlinkProtectionSettings` and applies its Hotlink Protection value. It throws `InvalidOperationException` instead of silently disabling protection when that envelope has no value.

This package wraps only the `hotlink_protection` zone setting; it does not manage Cloudflare's other scraping or bot products. Enabling Hotlink Protection can block images embedded from other sites, so confirm that behavior is appropriate before changing a production zone.

Cloudflare API errors are propagated through the generated client. Successful responses are nullable because the generated model permits an empty response body.
