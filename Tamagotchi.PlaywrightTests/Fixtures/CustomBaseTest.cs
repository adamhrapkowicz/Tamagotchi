using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.PageObjectModels.Pages;

namespace PlaywrightTests.Fixtures;

public class CustomBaseTest : PlaywrightTest
{
    protected async Task<TamagotchiIndexPage> SetupTamagotchiIndexPage()
    {
        await using var browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            Channel = "msedge"
        });
        await using var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();
        return new TamagotchiIndexPage(page);
    }
}