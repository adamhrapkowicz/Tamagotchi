using Microsoft.Playwright;

namespace PlaywrightTests.PageObjectModels.Sections;

public class GameStatusSection
{
    private readonly IPage _page;

    public GameStatusSection(IPage page)
    {
        _page = page;
    }
}