using Microsoft.Playwright;
using PlaywrightTests.PageObjectModels.Pages;

namespace PlaywrightTests.Fixtures;

public class CustomBaseTest : IDisposable
{
    private readonly Task<IPage> _page;
    private IBrowser? _browser;

    protected CustomBaseTest()
    {
        _page = InitializePage();
    }

    private IPage Page => _page.Result;
    protected TamagotchiIndexPage TamagotchiIndexPage => SetupTamagotchiIndexPage().Result;

    private async Task<IPage> InitializePage()
    {
        var playwright = await Playwright.CreateAsync();
        _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        return await _browser.NewPageAsync();
    }

    private async Task<TamagotchiIndexPage> SetupTamagotchiIndexPage()
    {
        return new TamagotchiIndexPage(Page);
    }

    public void Dispose() => _browser?.CloseAsync();
}