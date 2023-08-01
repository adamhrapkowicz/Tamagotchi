using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests.PageObjectModels.Sections;

public class TopHeadingSection : PageTest
{
    private readonly IPage _page;

    public TopHeadingSection(IPage page)
    {
        _page = page;
    }

    private ILocator WelcomeToTamagotchiHeading() =>
        _page.GetByRole(AriaRole.Heading, new PageGetByRoleOptions { Name = "Welcome to Tamagotchi" });

    private ILocator DragonHasJustDiedHeading() => _page.GetByText("has just died, because");
    
    public async Task ExpectWelcomeToTamagotchiHeadingToBeVisible() => 
        await Expect(WelcomeToTamagotchiHeading()).ToBeVisibleAsync();
    

    public async Task ExpectWelcomeToTamagotchiHeadingToBeHidden() =>
        await Expect(WelcomeToTamagotchiHeading()).ToBeHiddenAsync();
    
    public async Task ExpectDragonJustDiedHeadingToBeVisible() => 
        await Expect(DragonHasJustDiedHeading()).ToBeVisibleAsync();
    
    public async Task ExpectDragonJustDiedHeadingToBeHidden() => 
        await Expect(DragonHasJustDiedHeading()).ToBeHiddenAsync();
}