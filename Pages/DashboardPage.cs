using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightDemo.pages
{
    public class DashboardPage
    {
        private readonly IPage _page;

        private readonly ILocator _setUpMenu;
        private readonly ILocator _manageTeamMemberLink;
        private readonly ILocator _closeBtn;
        private readonly ILocator _pageTitle;


        public DashboardPage(IPage page)
        {
            _page = page;
            _setUpMenu = _page.Locator("li[role='tablist'] a[aria-label='Setup']");
            _manageTeamMemberLink = _page.Locator("a[aria-label='Manage Team Members']");
            _closeBtn = _page.Locator("button[aria-label='close']");
            _pageTitle = _page.Locator("div[class*='text-center'] h2");
        }

        public async Task ClickClosePopupAsync() => await _closeBtn.ClickAsync();

        public async Task ClickOnSetupMenuAsync() => await _setUpMenu.ClickAsync();

        public async Task ClickOnManageTeamMembersAsync() => await _manageTeamMemberLink.ClickAsync();

        public ILocator GetDashboardPageTitle() => _pageTitle;
       

    }
}
