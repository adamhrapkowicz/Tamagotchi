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
    
    [Test]
    public async Task NotDisplayNameFieldLabelAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ExpectNameFieldLabelNotToBeVisible();
    }

    [Test]
    public async Task NotDisplayNameFieldAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ExpectNameFieldNotToBeVisible();
    }
    
    [Test]
    public async Task NotDisplayStartGameButtonAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ExpectStartGameButtonNotToBeVisible();
    }

    [Test]
    public async Task DisplayPetButtonAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ExpectPetButtonToBeVisible();
    }
    
    [Test]
    public async Task DisplayFeedButtonAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ExpectFeedButtonToBeVisible();
    }

    [Test]
    public async Task DisplayDragonPictureAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ExpectDragonPictureToBeVisible();
    }
    
    [Test]
    public async Task DisplayILoveYouToastOnPetButtonClick()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ClickPetButton();
        await indexPage.ExpectILoveYouToastToBeVisible();
    }
    
    [Test]
    public async Task DisplayLeaveMeAloneToastOnPetButtonClick()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        for (var i = 0; i < 2; i++)
        {await indexPage.ClickPetButton();}
        await indexPage.ExpectLeaveMeAloneToastToBeVisible();
    }
    
    [Test]
    public async Task DisplayThatWasYummyToastOnPetButtonClick()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ClickFeedButton();
        await indexPage.ExpectThatWasYummyToastToBeVisible();
    }
    
    [Test]
    public async Task DisplayImNotHungryToastOnPetButtonClick()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        for (var i = 0; i < 2; i++)
        {
            await indexPage.ClickFeedButton();
        }
        await indexPage.ExpectImNotHungryToastToBeVisible();
    }
    
    
}