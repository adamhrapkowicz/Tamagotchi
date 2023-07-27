using Microsoft.Playwright;

namespace PlaywrightTests.PageObjectModels.Sections;

public class NameDragonSection
{
    private readonly IPage _page;

    public NameDragonSection(IPage page)
    {
        _page = page;
    }

    public ILocator NameFieldLabel() => _page.GetByText("Name your Dragon!");
    public ILocator NameField() => _page.GetByLabel("Name your Dragon!");
    public ILocator StartGameButton() =>
        _page.GetByRole(AriaRole.Button, new() { Name = "Start game" });

    public async Task EnterDragonName()
    {
        await NameField().FillAsync("FirstDragon");
    }

    public async Task SubmitDragonName()
    {
        await StartGameButton().ClickAsync();
    }

}