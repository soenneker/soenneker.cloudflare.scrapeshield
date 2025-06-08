using Soenneker.Cloudflare.ScrapeShield.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Cloudflare.ScrapeShield.Tests;

[Collection("Collection")]
public sealed class CloudflareScrapeShieldUtilTests : FixturedUnitTest
{
    private readonly ICloudflareScrapeShieldUtil _util;

    public CloudflareScrapeShieldUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<ICloudflareScrapeShieldUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
