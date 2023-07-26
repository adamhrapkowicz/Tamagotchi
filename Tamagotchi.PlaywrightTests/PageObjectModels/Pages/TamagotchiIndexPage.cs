using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.PageObjectModels.Sections;

namespace PlaywrightTests.PageObjectModels.Pages;

public class TamagotchiIndexPage : PageTest
{
    private readonly IPage _page;
    private readonly NameDragonSection _nameDragonSection;

    public TamagotchiIndexPage(IPage page)
    {
        _page = page;
        _nameDragonSection = new(_page);
    }

    public async Task GoToPage()
    {
        await _page.GotoAsync("https://localhost:59039/index.html");
    }
    public async Task ExpectNameFieldLabelToBeVisible()
    {
        await Expect(_nameDragonSection.NameFieldLabel()).ToBeVisibleAsync();
    }

    public async Task ExpectNameFieldToBeVisible()
    {
        await Expect(_nameDragonSection.NameField()).ToBeVisibleAsync();
    }
    
    public async Task ExpectStartGameButtonToBeVisible()
    {
        await Expect(_nameDragonSection.StartGameButton()).ToBeVisibleAsync();
    }

    public async Task StartNewGame()
    {
        await _nameDragonSection.EnterDragonName();
        await _nameDragonSection.SubmitDragonName();
    }
}