using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace SeleniumCSharp
{
    public class Tests
    {

        IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            driver = new ChromeDriver(path + @"\drivers\");
            driver.Navigate().GoToUrl("https://appeals.dev.web.it.cuyahoga.cc/");

        }

        [Test]
        public void verifyNavbar()
        {
            var links = driver.FindElements(By.ClassName("nav-link"));
            Assert.That(driver.FindElement(By.Id("navbar")).Displayed && links.Count == 8);
        }

        [Test]
        public void verifyHome()
        {
            var link = driver.FindElement(By.LinkText("HOME"));
            Assert.That(link.Text.Equals(" HOME"));
            link.Click();
            Assert.That(driver.FindElement(By.LinkText("More News")) != null);
        }

        [Test]
        public void verifyAbout()
        {
            var link = driver.FindElement(By.LinkText("ABOUT US"));
            Assert.That(link.Text.Equals("ABOUT US"));
            link.Click();
            Assert.That(driver.FindElements(By.TagName("h3")).Count.Equals(6));
        }

        [Test]
        public void verifySearch()
        {
            Actions action = new Actions(driver);
            var searchInput = driver.FindElement(By.TagName("input"));
            var search = driver.FindElement(By.ClassName("search-btn"));
            action.MoveToElement(search).Perform();
            searchInput.SendKeys("judge");
            search.Click();
            Assert.That(driver.FindElement(By.TagName("h1")).Text.Contains("judge"));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}