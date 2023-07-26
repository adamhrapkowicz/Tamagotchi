using PlaywrightTests.Fixtures;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class UsingFixturesTamagotchiIndexPageShould : CustomBaseTest
{
    [Test]
    public async Task HaveNameFieldLabelVisibleOnLaunch()
    {
        var tamagotchiIndexPage = await SetupTamagotchiIndexPage();
        await tamagotchiIndexPage.GoToPage();
        await tamagotchiIndexPage.ExpectNameFieldLabelToBeVisible();
    }
}