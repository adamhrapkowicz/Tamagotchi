using PlaywrightTests.Fixtures;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class UsingFixturesTamagotchiIndexPageShould : CustomBaseTest
{
    [Test]
    public async Task HaveNameFieldLabelVisibleOnLaunch()
    {
        await TamagotchiIndexPage.GoToPage();
        await TamagotchiIndexPage.ExpectNameFieldLabelToBeVisible();
    }
}