using Microsoft.Playwright;

namespace PlaywrightTests.PageObjectModels.Sections;

public class TopHeadingSection
{
    private readonly IPage _page;

    public TopHeadingSection(IPage page)
    {
        _page = page;
    }

    public ILocator WelcomeToTamagotchiHeading() =>
        _page.GetByRole(AriaRole.Heading, new() { Name = "Welcome to Tamagotchi" });

    public ILocator DragonHasJustDiedHeading() => _page.GetByText("has just died, because");
}