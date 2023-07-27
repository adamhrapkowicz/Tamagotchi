using Microsoft.Playwright;

namespace PlaywrightTests.PageObjectModels.Sections;

public class TopBannerSection
{
    private readonly IPage _page;

    public TopBannerSection(IPage page)
    {
        _page = page;
    }
}