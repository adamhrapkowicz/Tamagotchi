using Microsoft.Playwright;

namespace PlaywrightTests.PageObjectModels.Sections;

public class GameControlsSection
{
    private readonly IPage _page;

    public GameControlsSection(IPage page)
    {
        _page = page;
    }

    public ILocator PetButton() => _page.GetByRole(AriaRole.Button, new() { Name = "Pet" });
    public ILocator FeedButton() => _page.GetByRole(AriaRole.Button, new() { Name = "Feed" });
    public ILocator DragonPicture() => _page.Locator("#dragonPic");
    public ILocator LoveYouToast() => _page.GetByText("I love you!");
    public ILocator LeaveMeAloneToast() => _page.GetByText("Leave me alone!");
    public ILocator ThatWasYummyToast() => _page.GetByText("That was yummy!");
    public ILocator ImNotHungryToast() => _page.GetByText("I'm not hungry!");
}