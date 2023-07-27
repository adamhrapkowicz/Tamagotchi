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
    public async Task DisplayThatWasYummyToastOnFeedButtonClick()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ClickFeedButton();
        await indexPage.ExpectThatWasYummyToastToBeVisible();
    }
    
    [Test]
    public async Task DisplayImNotHungryToastOnFeedButtonClick()
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
    
    [Test]
    public async Task NotDisplayPetButtonOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.ExpectPetButtonNotToBeVisible();
    }
    
    [Test]
    public async Task NotDisplayFeedButtonOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.ExpectFeedButtonNotToBeVisible();
    }

    [Test]
    public async Task NotDisplayDragonPictureOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.ExpectDragonPictureNotToBeVisible();
    }
    
    [Test]
    public async Task NotDisplayILoveYouToastOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.ExpectILoveYouToastNotToBeVisible();
    }
    
    [Test]
    public async Task NotDisplayLeaveMeAloneToastOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.ExpectLeaveMeAloneToastNotToBeVisible();
    }
    
    [Test]
    public async Task NotDisplayThatWasYummyToastOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.ExpectThatWasYummyToastNotToBeVisible();
    }
    
    [Test]
    public async Task NotDisplayImNotHungryToastOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.ExpectImNotHungryToastNotToBeVisible();
    }
    
    [Test]
    public async Task NotDisplayILoveYouToastAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ExpectILoveYouToastNotToBeVisible();
    }
    
    [Test]
    public async Task NotDisplayLeaveMeAloneToastAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ExpectLeaveMeAloneToastNotToBeVisible();
    }
    
    [Test]
    public async Task NotDisplayThatWasYummyToastAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ExpectThatWasYummyToastNotToBeVisible();
    }
    
    [Test]
    public async Task NotDisplayImNotHungryToastAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ExpectImNotHungryToastNotToBeVisible();
    }
}