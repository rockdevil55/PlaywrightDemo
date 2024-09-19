using Microsoft.Playwright;
using NUnit.Framework.Internal;
using PlaywrightDemo.Config;
using PlaywrightDemo.Driver;
using PlaywrightDemo.pages;
using PlaywrightDemo.utils;

namespace PlaywrightDemo
{
    public class Tests
    {
        private PlaywrightDriver driver;
        private IPage page;
        private LoginPage loginPage;
        private DashboardPage dashboardPage;
        private AddTeamMemberPage addTeamMemberPage;
        private TeamMembersPage teamMembersPage;
        private CommonMethods commonMethods;

        private const string userName = "testautomation";
        private const string password = "Welcome123";
        private const string firstName = "test";
        private const string lastName = "Automation";
        private const string newPassword = "Tug54@1467";
        private const string mobileNumber = "6545655566";

        [SetUp]
        public async Task Setup()
        {
            var testSettings = new TestSettings
            {
                Channel = "chrome",
                DevTools = false,
                Headless = false,
                SlowMo = 1500,
            };

            driver = new PlaywrightDriver();
            page = await driver.InitializePlaywrightAsync(testSettings);
            loginPage = new LoginPage(page);
            await loginPage.VerifyLoginAsync(userName, password);
            dashboardPage = new DashboardPage(page);
            addTeamMemberPage = new AddTeamMemberPage(page);
            teamMembersPage = new TeamMembersPage(page);
            commonMethods = new CommonMethods();
        }

        [Test]
        public async Task CreateUpdateAndDeleteTeamMember()
        {
            // await dashboardPage.ClickClosePopupAsync();
            await VerifyDashboardTitleAsync(userName);
            await NavigateToManageTeamMembersAsync();

            // Add New Team Member
            string rndString = commonMethods.GetRandomString();
            int rndNumber = commonMethods.GetRandomNumber();
            string phNumber = commonMethods.RandomDigits(10);
            await addTeamMemberPage.CreateNewTeamMemberAsync(
                $"{firstName}{rndNumber}",
                $"{lastName}{rndNumber}",
                $"{rndString}{lastName}{commonMethods.GetRandomNumber()}@gmail.com",
                $"{phNumber}",
                $"{rndString}{rndNumber}",
                newPassword
            );

            await VerifySaveSuccessAsync();

            // Verify Team Member
            await teamMembersPage.EnterSearchRecordAsync($"{firstName}{rndNumber}");
            var userNameLocator = teamMembersPage.GetSearchedUser($"{lastName}{rndNumber}, {firstName}{rndNumber}");
            await Assertions.Expect(userNameLocator).ToBeVisibleAsync();

            // Update Team Member
            await UpdateTeamMemberAsync(rndNumber);

            // Delete Existing Team Member
            await DeleteTeamMemberAsync(rndNumber);
        }

        private async Task VerifyDashboardTitleAsync(string userName)
        {
            var dashboardTitle = dashboardPage.GetDashboardPageTitle();
            await Assertions.Expect(dashboardTitle).ToHaveTextAsync($" Welcome, {userName}");
        }

        private async Task NavigateToManageTeamMembersAsync()
        {
            await dashboardPage.ClickOnSetupMenuAsync();
            await dashboardPage.ClickOnManageTeamMembersAsync();
            var dashboardTitle = dashboardPage.GetDashboardPageTitle();
            await Assertions.Expect(dashboardTitle).ToHaveTextAsync("Team Members");
            await teamMembersPage.AddTeamMemberIconClickAsync();
            var expPageTitle = addTeamMemberPage.GetPageTitle();
            await Assertions.Expect(expPageTitle).ToHaveTextAsync("Add Team Member");
        }

        private async Task VerifySaveSuccessAsync()
        {
            var loader = addTeamMemberPage.WaitForLoaderInvisible();
            await loader.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Hidden });
            var saveMessage = addTeamMemberPage.GetSavedStudentRecord();
            await Assertions.Expect(saveMessage).ToHaveTextAsync("Saved");
        }



        private async Task UpdateTeamMemberAsync(int rndNumber)
        {
            await teamMembersPage.ClickEditLinkAsync();
            await addTeamMemberPage.FillFirstNameAsync($"Update{firstName}{rndNumber}");
            await addTeamMemberPage.FillUserNameAsync($"Update{userName}{rndNumber}{rndNumber}");
            await addTeamMemberPage.ClickSaveAndCloseAsync();
            await VerifySaveSuccessAsync();

            await teamMembersPage.EnterSearchRecordAsync($"{firstName}{rndNumber}");
            var updatedUserNameLocator = teamMembersPage.GetSearchedUser($"{lastName}{rndNumber}, Update{firstName}{rndNumber}");
            await Assertions.Expect(updatedUserNameLocator).ToBeVisibleAsync();
        }

        private async Task DeleteTeamMemberAsync(int rndNumber)
        {
            await teamMembersPage.ClickEditLinkAsync();
            var loader = addTeamMemberPage.WaitForLoaderInvisible();
            await loader.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Hidden });
            await addTeamMemberPage.ClickDeleteBtnAsync();

            var expDeleteMessage = addTeamMemberPage.GetDeletePopupMessage();
            await Assertions.Expect(expDeleteMessage).ToHaveTextAsync($"Are you sure you want to delete the Team Member {firstName}{rndNumber} Update{lastName}{rndNumber}?");
            await addTeamMemberPage.ClickConfirmDeleteBtnAsync();
            await loader.WaitForAsync();

            var expDeleteDetailsMessage = addTeamMemberPage.GetSavedStudentRecord();
            await Assertions.Expect(expDeleteDetailsMessage).ToHaveTextAsync("Deleted");

            await teamMembersPage.EnterSearchRecordAsync($"{firstName}{rndNumber}");
            var deletedUserNameLocator = teamMembersPage.GetSearchedUser($"{lastName}{rndNumber}, Updated{firstName}{rndNumber}");
            await Assertions.Expect(deletedUserNameLocator).ToHaveCountAsync(0);
        }
    }
}
