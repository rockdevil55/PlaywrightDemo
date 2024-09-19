using Microsoft.Playwright;
using PlaywrightDemo.Config;
using System.Configuration;
using System.Security.Policy;
using System.Threading.Tasks;

namespace PlaywrightDemo.Driver
{
    public class PlaywrightDriver
    {
        public async Task<IPage> InitializePlaywrightAsync(TestSettings testSettings)
        {
            var browser = await GetBrowserAsync(testSettings);
            var browserContext = await browser.NewContextAsync();
            var page = await browserContext.NewPageAsync();        

            await page.GotoAsync("https://rta-edu-dev-web.azurewebsites.net/login/");

            return page;
        }

        private async Task<IBrowser> GetBrowserAsync(TestSettings testSettings)
        {
            var playwright = await Playwright.CreateAsync();

            var browserOptions = new BrowserTypeLaunchOptions
            {
                Headless = testSettings.Headless,
                Devtools = testSettings.DevTools,
                SlowMo = testSettings.SlowMo,
                Channel = testSettings.Channel
            };

            return testSettings.DriverType switch
            {
                DriverType.Chrome => await playwright.Chromium.LaunchAsync(browserOptions),
                DriverType.Edge => await playwright.Chromium.LaunchAsync(browserOptions),
                DriverType.Firefox => await playwright.Firefox.LaunchAsync(browserOptions),
                _ => await playwright.Chromium.LaunchAsync(browserOptions) // Default to Chromium
            };
        }
    }
}