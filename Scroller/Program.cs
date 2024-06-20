using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Scroller
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var driver = new ChromeDriver();
            Console.WriteLine("---------------------");
            Console.WriteLine("input URL: ");
            string url = Console.ReadLine();

            driver.Url = "https://www.nuget.org/packages/Selenium.WebDriver";

            var h = driver.ExecuteScript("return document.body.scrollHeight");
            int height = int.Parse(h.ToString());

            scrollDown(driver, height);
            snapTop(driver, height);
        }
        public static void scrollDown(WebDriver driver, int height)
        {
            for (int i = 0; i < height; i++)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,1)", "");
            }
        }
        public static void snapTop(WebDriver driver, int height)
        {
            new Actions(driver)
                .ScrollByAmount(0, -height)
                .Perform();
        }
    }
}
