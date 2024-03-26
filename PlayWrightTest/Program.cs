// See https://aka.ms/new-console-template for more information

using PlayWrightTest;

Console.WriteLine("Hello, World!");


//PwController pw = new();



//bool doesIthave = await pw.DoesItHave();

await MyPlaywrightTest.CheckPageTitle();

string myText = await MyPlaywrightTest.GetTextByClassName("heroTitle_ohkl");
//Console.WriteLine($"doesIthave: {doesIthave}");


List<string> allLinks = await MyPlaywrightTest.GetAllLinks(@"https://Playwright.dev");

Console.WriteLine("\r\n********  LINKS ********");

foreach (string link in allLinks)
{
    Console.WriteLine(link);
}

Console.WriteLine("MyTextFromClass:" + myText);

Console.ReadLine();