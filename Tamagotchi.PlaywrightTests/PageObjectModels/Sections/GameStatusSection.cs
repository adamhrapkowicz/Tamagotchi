using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests.PageObjectModels.Sections;

public class GameStatusSection : PageTest
{
    private readonly IPage _page;

    public GameStatusSection(IPage page)
    {
        _page = page;
    }

    private ILocator GameStatusTable() => _page.Locator("#statusTable");

    public async Task ExpectGameStatusTableToBeVisible() => await Expect(GameStatusTable()).ToBeVisibleAsync();

    public async Task ExpectGameStatusTableToBeHidden() => await Expect(GameStatusTable()).ToBeHiddenAsync();
}