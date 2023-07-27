﻿using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.PageObjectModels.Sections;

namespace PlaywrightTests.PageObjectModels.Pages;

public class TamagotchiIndexPage : PageTest
{
    private readonly IPage _page;
    private readonly NameDragonSection _nameDragonSection;
    private readonly GameControlsSection _gameControlsSection;
    private readonly GameStatusSection _gameStatusSection;
    private readonly TopHeadingSection _topHeadingSection;

    public TamagotchiIndexPage(IPage page)
    {
        _page = page;
        _nameDragonSection = new(_page);
        _topHeadingSection = new(_page);
        _gameControlsSection = new(_page);
        _gameStatusSection = new(_page);
    }

    public async Task GoToPage()
    {
        await _page.GotoAsync("https://localhost:59039/index.html");
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

    public async Task ExpectWelcomeToTamagotchiHeadingToBeVisible()
    {
        await Expect(_topHeadingSection.WelcomeToTamagotchiHeading()).ToBeVisibleAsync();
    }
    public async Task ExpectWelcomeToTamagotchiHeadingNotToBeVisible()
    {
        await Expect(_topHeadingSection.WelcomeToTamagotchiHeading()).ToBeHiddenAsync();
    }
    
    public async Task ExpectDragonJustDiedHeadingToBeVisible()
    {
        await Expect(_topHeadingSection.DragonHasJustDiedHeading()).ToBeVisibleAsync();
    }
    public async Task ExpectDragonJustDiedHeadingNotToBeVisible()
    {
        await Expect(_topHeadingSection.DragonHasJustDiedHeading()).ToBeHiddenAsync();
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

    public async Task ExpectPetButtonNotToBeVisible()
    {
        await Expect(_gameControlsSection.PetButton()).ToBeHiddenAsync();
    }
    
    public async Task ExpectFeedButtonNotToBeVisible()
    {
        await Expect(_gameControlsSection.FeedButton()).ToBeHiddenAsync();
    }

    public async Task ExpectDragonPictureNotToBeVisible()
    {
        await Expect(_gameControlsSection.DragonPicture()).ToBeHiddenAsync();
    }

    public async Task ExpectILoveYouToastNotToBeVisible()
    {
        await Expect(_gameControlsSection.LoveYouToast()).ToBeHiddenAsync();
    }

    public async Task ExpectLeaveMeAloneToastNotToBeVisible()
    {
        await Expect(_gameControlsSection.LeaveMeAloneToast()).ToBeHiddenAsync();
    }

    public async Task ExpectThatWasYummyToastNotToBeVisible()
    {
        await Expect(_gameControlsSection.ThatWasYummyToast()).ToBeHiddenAsync();
    }

    public async Task ExpectImNotHungryToastNotToBeVisible()
    {
        await Expect(_gameControlsSection.ImNotHungryToast()).ToBeHiddenAsync();
    }

    public async Task ExpectGameStatusTableToBeVisible()
    {
        await Expect(_gameStatusSection.GameStatusTable()).ToBeVisibleAsync();
    }

    public async Task ExpectGameStatusTableToBeHidden()
    {
        await Expect(_gameStatusSection.GameStatusTable()).ToBeHiddenAsync();
    }
}