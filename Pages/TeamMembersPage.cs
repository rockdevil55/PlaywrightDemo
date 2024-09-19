using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightDemo.pages
{
    public class TeamMembersPage
    {
        private readonly IPage _page;
        private readonly ILocator _editLinkLocator;
        private readonly ILocator _searchRecordLocator;
        private readonly ILocator _addTeamMemberBtnLocator;

        public TeamMembersPage(IPage page)
        {
            _page = page;
            _editLinkLocator = _page.Locator("//a[text()='Edit']");
            _searchRecordLocator = _page.Locator("input[placeholder='Search team members']");
            _addTeamMemberBtnLocator = _page.Locator("a[title='Add Team Member']");
        }

        public async Task AddTeamMemberIconClickAsync() => await _addTeamMemberBtnLocator.ClickAsync();

        public async Task EnterSearchRecordAsync(string studentName)
        {
            await _searchRecordLocator.FillAsync(studentName);
            await _page.Keyboard.PressAsync("Enter");
        }

        public async Task ClickEditLinkAsync()
        {
            await _editLinkLocator.ClickAsync();
            // Removed Thread.Sleep; consider using an appropriate wait instead.
        }

        public ILocator GetSearchedUser(string name) => _page.GetByText(name);
    }
}
