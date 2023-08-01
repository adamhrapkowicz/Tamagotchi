using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests.PageObjectModels.Sections;

public class GameControlsSection : PageTest
{
    private readonly IPage _page;

    public GameControlsSection(IPage page)
    {
        _page = page;
    }

    public ILocator PetButton() => _page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Pet" });
    public ILocator FeedButton() => _page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Feed" });
    private ILocator DragonPicture() => _page.Locator("#dragonPic");
    private ILocator LoveYouToast() => _page.GetByText("I love you!");
    private ILocator LeaveMeAloneToast() => _page.GetByText("Leave me alone!");
    private ILocator ThatWasYummyToast() => _page.GetByText("That was yummy!");
    private ILocator ImNotHungryToast() => _page.GetByText("I'm not hungry!");
    
    public async Task ExpectPetButtonToBeVisible() => await Expect(PetButton()).ToBeVisibleAsync();

    public async Task ExpectFeedButtonToBeVisible() => await Expect(FeedButton()).ToBeVisibleAsync();

    public async Task ExpectDragonPictureToBeVisible() => await Expect(DragonPicture()).ToBeVisibleAsync();

    public async Task ExpectILoveYouToastToBeVisible() => await Expect(LoveYouToast()).ToBeVisibleAsync();

    public async Task ExpectLeaveMeAloneToastToBeVisible() => await Expect(LeaveMeAloneToast()).ToBeVisibleAsync();

    public async Task ExpectThatWasYummyToastToBeVisible() => await Expect(ThatWasYummyToast()).ToBeVisibleAsync();

    public async Task ExpectImNotHungryToastToBeVisible() => await Expect(ImNotHungryToast()).ToBeVisibleAsync();

    public async Task ExpectPetButtonToBeHidden() => await Expect(PetButton()).ToBeHiddenAsync();

    public async Task ExpectFeedButtonToBeHidden() => await Expect(FeedButton()).ToBeHiddenAsync();

    public async Task ExpectDragonPictureToBeHidden() => await Expect(DragonPicture()).ToBeHiddenAsync();

    public async Task ExpectILoveYouToastToBeHidden() => await Expect(LoveYouToast()).ToBeHiddenAsync();

    public async Task ExpectLeaveMeAloneToastToBeHidden() => await Expect(LeaveMeAloneToast()).ToBeHiddenAsync();

    public async Task ExpectThatWasYummyToastToBeHidden() => await Expect(ThatWasYummyToast()).ToBeHiddenAsync();

    public async Task ExpectImNotHungryToastToBeHidden() => await Expect(ImNotHungryToast()).ToBeHiddenAsync();
}