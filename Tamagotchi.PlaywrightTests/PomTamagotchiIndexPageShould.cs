using Microsoft.Playwright.NUnit;
using PlaywrightTests.PageObjectModels.Pages;

namespace PlaywrightTests;

public class PomTamagotchiIndexPageShould : PageTest
{
    [Test]
    public async Task DisplayWelcomeToTamagotchiHeadingOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.TopHeadingSection.ExpectWelcomeToTamagotchiHeadingToBeVisible();
    }

    [Test]
    public async Task DisplayWelcomeToTamagotchiHeadingAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.TopHeadingSection.ExpectWelcomeToTamagotchiHeadingToBeVisible();
    }
    
    [Test]
    public async Task DisplayWelcomeToTamagotchiHeadingAfterPetButtonClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ClickPetButton();
        await indexPage.TopHeadingSection.ExpectWelcomeToTamagotchiHeadingToBeVisible();
    }
    
    [Test]
    public async Task DisplayWelcomeToTamagotchiHeadingAfterFeedButtonClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ClickFeedButton();
        await indexPage.TopHeadingSection.ExpectWelcomeToTamagotchiHeadingToBeVisible();
    }
    
    [Test]
    public async Task NotDisplayWelcomeToTamagotchiHeadingAfterDragonDied()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await Task.Delay(50000);
        await indexPage.TopHeadingSection.ExpectWelcomeToTamagotchiHeadingToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayDragonJustDiedHeadingOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.TopHeadingSection.ExpectDragonJustDiedHeadingToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayDragonJustDiedHeadingAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.TopHeadingSection.ExpectDragonJustDiedHeadingToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayDragonJustDiedHeadingAfterFeedButtonClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ClickFeedButton();
        await indexPage.TopHeadingSection.ExpectDragonJustDiedHeadingToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayDragonJustDiedHeadingAfterPetButtonClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ClickPetButton();
        await indexPage.TopHeadingSection.ExpectDragonJustDiedHeadingToBeHidden();
    }
    
    [Test]
    public async Task DisplayDragonJustDiedHeadingWhenDragonDied()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await Task.Delay(50000);
        await indexPage.TopHeadingSection.ExpectDragonJustDiedHeadingToBeVisible();
    }
    
    [Test]
    public async Task DisplayNameFieldLabelOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.NameDragonSection.ExpectNameFieldLabelToBeVisible();
    }

    [Test]
    public async Task DisplayNameFieldOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.NameDragonSection.ExpectNameFieldToBeVisible();
    }
    
    [Test]
    public async Task DisplayStartGameButtonOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.NameDragonSection.ExpectStartGameButtonToBeVisible();
    }

    [Test]
    public async Task NotDisplayNameFieldLabelAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.NameDragonSection.ExpectNameFieldLabelToBeHidden();
    }

    [Test]
    public async Task NotDisplayNameFieldAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.NameDragonSection.ExpectNameFieldToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayStartGameButtonAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.NameDragonSection.ExpectStartGameButtonToBeHidden();
    }

    [Test]
    public async Task DisplayPetButtonAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.GameControlsSection.ExpectPetButtonToBeVisible();
    }
    
    [Test]
    public async Task DisplayFeedButtonAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.GameControlsSection.ExpectFeedButtonToBeVisible();
    }

    [Test]
    public async Task DisplayDragonPictureAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.GameControlsSection.ExpectDragonPictureToBeVisible();
    }
    
    [Test]
    public async Task DisplayILoveYouToastOnPetButtonClick()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ClickPetButton();
        await indexPage.GameControlsSection.ExpectILoveYouToastToBeVisible();
    }
    
    [Test]
    public async Task DisplayLeaveMeAloneToastOnPetButtonClick()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        for (var i = 0; i < 2; i++)
        {await indexPage.ClickPetButton();}
        await indexPage.GameControlsSection.ExpectLeaveMeAloneToastToBeVisible();
    }
    
    [Test]
    public async Task DisplayThatWasYummyToastOnFeedButtonClick()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ClickFeedButton();
        await indexPage.GameControlsSection.ExpectThatWasYummyToastToBeVisible();
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
        await indexPage.GameControlsSection.ExpectImNotHungryToastToBeVisible();
    }
    
    [Test]
    public async Task NotDisplayPetButtonOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.GameControlsSection.ExpectPetButtonToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayFeedButtonOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.GameControlsSection.ExpectFeedButtonToBeHidden();
    }

    [Test]
    public async Task NotDisplayDragonPictureOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.GameControlsSection.ExpectDragonPictureToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayILoveYouToastOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.GameControlsSection.ExpectILoveYouToastToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayLeaveMeAloneToastOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.GameControlsSection.ExpectLeaveMeAloneToastToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayThatWasYummyToastOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.GameControlsSection.ExpectThatWasYummyToastToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayImNotHungryToastOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.GameControlsSection.ExpectImNotHungryToastToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayILoveYouToastAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.GameControlsSection.ExpectILoveYouToastToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayLeaveMeAloneToastAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.GameControlsSection.ExpectLeaveMeAloneToastToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayThatWasYummyToastAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.GameControlsSection.ExpectThatWasYummyToastToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayImNotHungryToastAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.GameControlsSection.ExpectImNotHungryToastToBeHidden();
    }

    [Test]
    public async Task HideGameStatusTableOnLaunch()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.GameStatusSection.ExpectGameStatusTableToBeHidden();
    }
    
    [Test]
    public async Task DisplayGameStatusTableAfterStartGameClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.GameStatusSection.ExpectGameStatusTableToBeVisible();
    }
    
    [Test]
    public async Task DisplayGameStatusTableAfterFeedButtonClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ClickFeedButton();
        await indexPage.GameStatusSection.ExpectGameStatusTableToBeVisible();
    }
    
    [Test]
    public async Task DisplayGameStatusTableAfterPetButtonClicked()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await indexPage.ClickPetButton();
        await indexPage.GameStatusSection.ExpectGameStatusTableToBeVisible();
    }
    
    [Test]
    public async Task HideGameStatusTableAfterDragonDied()
    {
        var indexPage = new TamagotchiIndexPage(Page);
        await indexPage.GoToPage();
        await indexPage.StartNewGame();
        await Task.Delay(50000);
        await indexPage.GameStatusSection.ExpectGameStatusTableToBeHidden();
    }
}