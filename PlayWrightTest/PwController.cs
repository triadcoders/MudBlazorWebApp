using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlayWrightTest;

// pwsh C:\Users\ctysinger.CORP\RiderProjects\MudBlazorWebApp\PlayWrightTest\bin\Debug\net8.0\playwright.ps1 install

namespace PlayWrightTest;




[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PwController : PageTest
{
    [Test]
    public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
    {
        await Page.GotoAsync("https://playwright.dev");

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));

        // create a locator
        var getStarted = Page.GetByRole(AriaRole.Link, new() { Name = "Get started" });

        // Expect an attribute "to be strictly equal" to the value.
        await Expect(getStarted).ToHaveAttributeAsync("href", "/docs/intro");

        // Click the get started link.
        await getStarted.ClickAsync();

        // Expects the URL to contain intro.
        await Expect(Page).ToHaveURLAsync(new Regex(".*intro"));

    }
}




public static class PlaywrightUtils
{
    public static async Task<bool> HasTitleAsync(string url, string expectedTitle)
    {
        using var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(); // Launch Chromium

        var page = await browser.NewPageAsync();
        await page.GotoAsync(url); // Navigate to the URL

        var actualTitle = await page.TitleAsync(); // Get page title
        Console.WriteLine("actualTitle: " + actualTitle);
        await browser.CloseAsync(); // Close browser

        return actualTitle.Equals(expectedTitle, StringComparison.OrdinalIgnoreCase);
    }
    
    public static async Task<string?> GetTextByClassAsync(string url, string className)
    {
        using var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(); // Launch Chromium
        var page = await browser.NewPageAsync();
        await page.GotoAsync(url); // Navigate to the URL

        string? myString = "This is a string result";
        Task<string?> emptyTask = Task.FromResult<string>(myString);

        var element = await page.QuerySelectorAsync($".{className}"); // Use f-string for selector

        if (element != null)
        {
            return await element?.TextContentAsync(); // Use null-conditional operator for potential null element
        }

        return emptyTask.Result;

    }
}

public class MyPlaywrightTest
{
    [Test]
    public static async Task CheckPageTitle()
    {
        var hasExpectedTitle = await PlaywrightUtils.HasTitleAsync("https://playwright.dev/docs/intro", "Fast and reliable end-to-end testing for modern web apps | Playwright");
       // var hasExpectedTitle = await PlaywrightUtils.HasTitleAsync("file:///C:/tmp/test.html", "Fast and reliable end-to-end testing for modern web apps | Playwright");

        if (hasExpectedTitle)
        {
            Console.WriteLine("Page title matches!");
        }
        else
        {
            Console.WriteLine("Page title doesn't match!");
        }
    }
    
    public static async Task<string> GetTextByClassName(string className)
    {
        string classText  = await PlaywrightUtils.GetTextByClassAsync("https://Playwright.dev", className) ?? "NA";

        return classText;
        //  Console.WriteLine("ClassText: " + classText);
    }

    public static async Task<List<string>> GetAllLinks(string url)
    {
        using var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(); // Launch Chromium
        var page = await browser.NewPageAsync();
        await page.GotoAsync(url); // Navigate to the URL

// Option 1: Using page.QuerySelectorAllAsync and GetAttribute
        var links1 = await page.QuerySelectorAllAsync("a"); // Find all anchor tags (links)
        var linkUrls1 = new List<string>();
        foreach (var link in links1)
        {
            var href = await link.GetAttributeAsync("href");
            if (href != null)
            {
                linkUrls1.Add(href);
            }
        }

        return linkUrls1;
    }

    public static async Task NavigateLink(string url)
    {
        using var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(); // Launch Chromium
        var page = await browser.NewPageAsync();
        await page.GotoAsync(url); // Navigate to the URL
   
        
        
        
        var link = await page.QuerySelectorAsync("a:text('About Us')"); // Find by text content

        if (link != null)
        {
            await link.ClickAsync(); // Click the link
        }
        else
        {
            Console.WriteLine("Link with text 'About Us' not found!");
        }

        await browser.CloseAsync();
        
    }
}
