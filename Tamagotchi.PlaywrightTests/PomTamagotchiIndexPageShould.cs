using Microsoft.Playwright.NUnit;
using PlaywrightTests.PageObjectModels.Pages;

namespace PlaywrightTests;

public class PomTamagotchiIndexPageShould : PageTest
{
    [Test]
    public async Task DisplayNameFieldLabelOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.ExpectNameFieldLabelToBeVisible();
    }

    [Test]
    public async Task DisplayNameFieldOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.ExpectNameFieldToBeVisible();
    }
    
    [Test]
    public async Task DisplayStartGameButtonOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.ExpectStartGameButtonToBeVisible();
    }
}