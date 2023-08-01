using PlaywrightTests.Fixtures;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class UsingFixturesTamagotchiIndexPageShould : CustomBaseTest
{
    [Test]
    public async Task DisplayWelcomeToTamagotchiHeadingOnLaunch()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.TopHeadingSection.ExpectWelcomeToTamagotchiHeadingToBeVisible();
    }

    [Test]
    public async Task DisplayWelcomeToTamagotchiHeadingAfterStartGameClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.TopHeadingSection.ExpectWelcomeToTamagotchiHeadingToBeVisible();
    }
    
    [Test]
    public async Task DisplayWelcomeToTamagotchiHeadingAfterPetButtonClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.ClickPetButton();
        await TamagotchiIndexPage.TopHeadingSection.ExpectWelcomeToTamagotchiHeadingToBeVisible();
    }
    
    [Test]
    public async Task DisplayWelcomeToTamagotchiHeadingAfterFeedButtonClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.ClickFeedButton();
        await TamagotchiIndexPage.TopHeadingSection.ExpectWelcomeToTamagotchiHeadingToBeVisible();
    }
    
    [Test]
    public async Task NotDisplayWelcomeToTamagotchiHeadingAfterDragonDied()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await Task.Delay(50000);
        await TamagotchiIndexPage.TopHeadingSection.ExpectWelcomeToTamagotchiHeadingToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayDragonJustDiedHeadingOnLaunch()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.TopHeadingSection.ExpectDragonJustDiedHeadingToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayDragonJustDiedHeadingAfterStartGameClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.TopHeadingSection.ExpectDragonJustDiedHeadingToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayDragonJustDiedHeadingAfterFeedButtonClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.ClickFeedButton();
        await TamagotchiIndexPage.TopHeadingSection.ExpectDragonJustDiedHeadingToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayDragonJustDiedHeadingAfterPetButtonClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.ClickPetButton();
        await TamagotchiIndexPage.TopHeadingSection.ExpectDragonJustDiedHeadingToBeHidden();
    }
    
    [Test]
    public async Task DisplayDragonJustDiedHeadingWhenDragonDied()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await Task.Delay(50000);
        await TamagotchiIndexPage.TopHeadingSection.ExpectDragonJustDiedHeadingToBeVisible();
    }
    
    [Test]
    public async Task DisplayNameFieldLabelOnLaunch()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.NameDragonSection.ExpectNameFieldLabelToBeVisible();
    }

    [Test]
    public async Task DisplayNameFieldOnLaunch()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.NameDragonSection.ExpectNameFieldToBeVisible();
    }
    
    [Test]
    public async Task DisplayStartGameButtonOnLaunch()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.NameDragonSection.ExpectStartGameButtonToBeVisible();
    }

    [Test]
    public async Task NotDisplayNameFieldLabelAfterStartGameClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.NameDragonSection.ExpectNameFieldLabelToBeHidden();
    }

    [Test]
    public async Task NotDisplayNameFieldAfterStartGameClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.NameDragonSection.ExpectNameFieldToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayStartGameButtonAfterStartGameClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.NameDragonSection.ExpectStartGameButtonToBeHidden();
    }

    [Test]
    public async Task DisplayPetButtonAfterStartGameClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.GameControlsSection.ExpectPetButtonToBeVisible();
    }
    
    [Test]
    public async Task DisplayFeedButtonAfterStartGameClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.GameControlsSection.ExpectFeedButtonToBeVisible();
    }

    [Test]
    public async Task DisplayDragonPictureAfterStartGameClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.GameControlsSection.ExpectDragonPictureToBeVisible();
    }
    
    [Test]
    public async Task DisplayILoveYouToastOnPetButtonClick()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.ClickPetButton();
        await TamagotchiIndexPage.GameControlsSection.ExpectILoveYouToastToBeVisible();
    }
    
    [Test]
    public async Task DisplayLeaveMeAloneToastOnPetButtonClick()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        for (var i = 0; i < 2; i++)
        {await TamagotchiIndexPage.ClickPetButton();}
        await TamagotchiIndexPage.GameControlsSection.ExpectLeaveMeAloneToastToBeVisible();
    }
    
    [Test]
    public async Task DisplayThatWasYummyToastOnFeedButtonClick()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.ClickFeedButton();
        await TamagotchiIndexPage.GameControlsSection.ExpectThatWasYummyToastToBeVisible();
    }
    
    [Test]
    public async Task DisplayImNotHungryToastOnFeedButtonClick()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        for (var i = 0; i < 2; i++)
        {
            await TamagotchiIndexPage.ClickFeedButton();
        }
        await TamagotchiIndexPage.GameControlsSection.ExpectImNotHungryToastToBeVisible();
    }
    
    [Test]
    public async Task NotDisplayPetButtonOnLaunch()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.GameControlsSection.ExpectPetButtonToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayFeedButtonOnLaunch()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.GameControlsSection.ExpectFeedButtonToBeHidden();
    }

    [Test]
    public async Task NotDisplayDragonPictureOnLaunch()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.GameControlsSection.ExpectDragonPictureToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayILoveYouToastOnLaunch()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.GameControlsSection.ExpectILoveYouToastToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayLeaveMeAloneToastOnLaunch()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.GameControlsSection.ExpectLeaveMeAloneToastToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayThatWasYummyToastOnLaunch()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.GameControlsSection.ExpectThatWasYummyToastToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayImNotHungryToastOnLaunch()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.GameControlsSection.ExpectImNotHungryToastToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayILoveYouToastAfterStartGameClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.GameControlsSection.ExpectILoveYouToastToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayLeaveMeAloneToastAfterStartGameClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.GameControlsSection.ExpectLeaveMeAloneToastToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayThatWasYummyToastAfterStartGameClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.GameControlsSection.ExpectThatWasYummyToastToBeHidden();
    }
    
    [Test]
    public async Task NotDisplayImNotHungryToastAfterStartGameClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.GameControlsSection.ExpectImNotHungryToastToBeHidden();
    }

    [Test]
    public async Task HideGameStatusTableOnLaunch()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.GameStatusSection.ExpectGameStatusTableToBeHidden();
    }
    
    [Test]
    public async Task DisplayGameStatusTableAfterStartGameClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.GameStatusSection.ExpectGameStatusTableToBeVisible();
    }
    
    [Test]
    public async Task DisplayGameStatusTableAfterFeedButtonClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.ClickFeedButton();
        await TamagotchiIndexPage.GameStatusSection.ExpectGameStatusTableToBeVisible();
    }
    
    [Test]
    public async Task DisplayGameStatusTableAfterPetButtonClicked()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await TamagotchiIndexPage.ClickPetButton();
        await TamagotchiIndexPage.GameStatusSection.ExpectGameStatusTableToBeVisible();
    }
    
    [Test]
    public async Task HideGameStatusTableAfterDragonDied()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.StartNewGame();
        await Task.Delay(50000);
        await TamagotchiIndexPage.GameStatusSection.ExpectGameStatusTableToBeHidden();
    }
}