using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests.PageObjectModels.Sections;

public class NameDragonSection : PageTest
{
    private readonly IPage _page;

    public NameDragonSection(IPage page)
    {
        _page = page;
    }

    private ILocator NameFieldLabel() => _page.GetByText("Name your Dragon!");
    private ILocator NameField() => _page.GetByLabel("Name your Dragon!");

    private ILocator StartGameButton() =>
        _page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Start game" });

    public async Task EnterDragonName() => await NameField().FillAsync("FirstDragon");
    
    public async Task SubmitDragonName() => await StartGameButton().ClickAsync();
    
    public async Task ExpectNameFieldLabelToBeVisible() => await Expect(NameFieldLabel()).ToBeVisibleAsync();

    public async Task ExpectNameFieldToBeVisible() => await Expect(NameField()).ToBeVisibleAsync();
    
    public async Task ExpectStartGameButtonToBeVisible() => await Expect(StartGameButton()).ToBeVisibleAsync();
    
    public async Task ExpectNameFieldLabelToBeHidden() => await Expect(NameFieldLabel()).ToBeHiddenAsync();
    
    public async Task ExpectNameFieldToBeHidden() => await Expect(NameField()).ToBeHiddenAsync();
    
    public async Task ExpectStartGameButtonToBeHidden() => await Expect(StartGameButton()).ToBeHiddenAsync();
}