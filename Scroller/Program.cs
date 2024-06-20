using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace Scroller
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("input URL: ");
            string url = Console.ReadLine();
            Console.Write("input scroll speed (pixels per milisecond): ");
            string speed = Console.ReadLine();

            //https://www.w3.org/TR/webdriver/#keyboard-actions

            var driver = new ChromeDriver();
            
            driver.Url = url;
            string prevUrl = driver.Url;

            Final(driver, speed);
            while (true)
            {
                while (driver.Url != prevUrl)
                {
                    prevUrl = driver.Url;
                    Final(driver, speed);
                }
            }
            
        }

        public static void ScrollDown(WebDriver driver, int height, string speed)
        {
            string exeString = "window.scrollBy(0,)";
            string modExeString = exeString.Insert(18, speed);
            int intSpeed = Int32.Parse(speed);

            for (int i = 0; i < height/intSpeed; i++)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript(modExeString, "");
            }
        }

        public static void SnapTop(WebDriver driver, int height)
        {
            new Actions(driver)
                .ScrollByAmount(0, -height + 1000)
                .Perform();
        }

        public static void Final(WebDriver driver, string speed)
        {
            var h = driver.ExecuteScript("return document.body.scrollHeight");
            int height = int.Parse(h.ToString());

            SnapTop(driver, height);

            Thread.Sleep(1000);
            ScrollDown(driver, height, speed);
        }
    }
}
