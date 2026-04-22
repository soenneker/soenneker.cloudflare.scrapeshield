using Soenneker.Cloudflare.ScrapeShield.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Cloudflare.ScrapeShield.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class CloudflareScrapeShieldUtilTests : HostedUnitTest
{
    private readonly ICloudflareScrapeShieldUtil _util;

    public CloudflareScrapeShieldUtilTests(Host host) : base(host)
    {
        _util = Resolve<ICloudflareScrapeShieldUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
