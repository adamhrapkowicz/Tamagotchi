using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.PageObjectModels.Sections;

namespace PlaywrightTests.PageObjectModels.Pages;

public class TamagotchiIndexPage : PageTest
{
    private readonly IPage _page;
    public readonly NameDragonSection NameDragonSection;
    public readonly GameControlsSection GameControlsSection;
    public readonly GameStatusSection GameStatusSection;
    public readonly TopHeadingSection TopHeadingSection;

    public TamagotchiIndexPage(IPage page)
    {
        _page = page;
        NameDragonSection = new NameDragonSection(_page);
        TopHeadingSection = new TopHeadingSection(_page);
        GameControlsSection = new GameControlsSection(_page);
        GameStatusSection = new GameStatusSection(_page);
    }

    public async Task GoToPage()
    {
        await _page.GotoAsync("https://localhost:59039/index.html");
    }
    
    public async Task StartNewGame()
    {
        await NameDragonSection.EnterDragonName();
        await NameDragonSection.SubmitDragonName();
    }

    public async Task ClickPetButton()
    {
        await GameControlsSection.PetButton().ClickAsync();
    }

    public async Task ClickFeedButton()
    {
        await GameControlsSection.FeedButton().ClickAsync();
    }
}