using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightDemo.pages
{
    public class LoginPage
    {
        private readonly IPage _page;
        private readonly ILocator _userNameLocator;
        private readonly ILocator _passwordLocator;
        private readonly ILocator _signInBtnLocator;

        public LoginPage(IPage page)
        {
            _page = page;
            _userNameLocator = _page.Locator("#UserName");
            _passwordLocator = _page.Locator("#Password");
            _signInBtnLocator = _page.Locator("#loginBtn");
        }

        public async Task EnterUserNameAsync(string userName) => await _userNameLocator.FillAsync(userName);

        public async Task EnterPasswordAsync(string password) => await _passwordLocator.FillAsync(password);

        public async Task ClickSignInBtnAsync() => await _signInBtnLocator.ClickAsync();

        public async Task VerifyLoginAsync(string username, string password)
        {
            await EnterUserNameAsync(username);
            await EnterPasswordAsync(password);
            await ClickSignInBtnAsync();
        }
    }
}
