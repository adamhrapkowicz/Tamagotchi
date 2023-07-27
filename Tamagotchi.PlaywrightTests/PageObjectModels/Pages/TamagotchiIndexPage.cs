using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.PageObjectModels.Sections;

namespace PlaywrightTests.PageObjectModels.Pages;

public class TamagotchiIndexPage : PageTest
{
    private readonly IPage _page;
    private readonly NameDragonSection _nameDragonSection;
    private readonly GameControlsSection _gameControlsSection;

    public TamagotchiIndexPage(IPage page)
    {
        _page = page;
        _nameDragonSection = new(_page);
        _gameControlsSection = new(_page);
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
    
    public async Task ExpectNameFieldLabelNotToBeVisible()
    {
        await Expect(_nameDragonSection.NameFieldLabel()).ToBeHiddenAsync();
    }

    public async Task ExpectNameFieldNotToBeVisible()
    {
        await Expect(_nameDragonSection.NameField()).ToBeHiddenAsync();
    }
    
    public async Task ExpectStartGameButtonNotToBeVisible()
    {
        await Expect(_nameDragonSection.StartGameButton()).ToBeHiddenAsync();
    }

    public async Task ExpectPetButtonToBeVisible()
    {
        await Expect(_gameControlsSection.PetButton()).ToBeVisibleAsync();
    }
    
    public async Task ExpectFeedButtonToBeVisible()
    {
        await Expect(_gameControlsSection.FeedButton()).ToBeVisibleAsync();
    }

    public async Task ExpectDragonPictureToBeVisible()
    {
        await Expect(_gameControlsSection.DragonPicture()).ToBeVisibleAsync();
    }

    public async Task ExpectILoveYouToastToBeVisible()
    {
        await Expect(_gameControlsSection.LoveYouToast()).ToBeVisibleAsync();
    }

    public async Task ExpectLeaveMeAloneToastToBeVisible()
    {
        await Expect(_gameControlsSection.LeaveMeAloneToast()).ToBeVisibleAsync();
    }

    public async Task ExpectThatWasYummyToastToBeVisible()
    {
        await Expect(_gameControlsSection.ThatWasYummyToast()).ToBeVisibleAsync();
    }

    public async Task ExpectImNotHungryToastToBeVisible()
    {
        await Expect(_gameControlsSection.ImNotHungryToast()).ToBeVisibleAsync();
    }

    public async Task StartNewGame()
    {
        await _nameDragonSection.EnterDragonName();
        await _nameDragonSection.SubmitDragonName();
    }
    
    public async Task ClickPetButton()
    {
        await _gameControlsSection.PetButton().ClickAsync();
    }

    public async Task ClickFeedButton()
    {
        await _gameControlsSection.FeedButton().ClickAsync();
    }
}