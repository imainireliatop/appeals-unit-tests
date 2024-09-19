using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;

namespace SeleniumCSharp
{
    public class Tests
    {

        ChromeDriver driver;
        ChromeOptions options;

        [OneTimeSetUp]
        public void Setup()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            options = new ChromeOptions();
            options.AddArgument("--headless=new");
            ChromeDriver driver = new ChromeDriver(path + @"\drivers\", options);
  
            driver.Navigate().GoToUrl("https://appeals.cuyahogacounty.gov/");

        }

        [Test]
        public void verifyNavbar()
        {
            ReadOnlyCollection<IWebElement> links = driver.FindElements(By.ClassName("nav-link"));
            Assert.That(driver.FindElement(By.Id("navbar")).Displayed && links.Count == 8);
        }

        [Test]
        public void verifyHome()
        {
            IWebElement link = driver.FindElement(By.LinkText("HOME"));
            Assert.That(link.Text.Equals(" HOME"));
            link.Click();
            Assert.That(driver.FindElement(By.LinkText("More News")) != null);
        }

        [Test]
        public void verifyAbout()
        {
            IWebElement link = driver.FindElement(By.LinkText("ABOUT US"));
            Assert.That(link.Text.Equals("ABOUT US"));
            link.Click();
            Assert.That(driver.FindElements(By.TagName("h3")).Count.Equals(6));
        }

        [Test]
        public void verifyCalendar()
        {
            IWebElement link = driver.FindElement(By.LinkText("CALENDAR"));
            Assert.That(link.Text.Equals("CALENDAR"));
            link.Click();
            IWebElement table = driver.FindElement(By.TagName("table"));
            string date = table.FindElement(By.TagName("a")).Text;
            table.FindElement(By.TagName("a")).Click();
            Assert.That(driver.FindElements(By.TagName("h1"))[1].Text == date);
        }

        [Test]
        public void verifyEfiling()
        {
            IWebElement link = driver.FindElement(By.LinkText("E-FILING"));
            Assert.That(link.Text.Equals("E-FILING"));
            link.Click();
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            Assert.That(driver.Title == "E-Filing");
            // close the second tab
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles[0]);
        }

        [Test]
        public void verifyOpinions()
        {
            IWebElement link = driver.FindElement(By.LinkText("OPINIONS AND DOCKET"));
            Assert.That(link.Text.Equals("OPINIONS AND DOCKET"));
            link.Click();
            ReadOnlyCollection<IWebElement> cards = driver.FindElements(By.ClassName("county-card"));
            Assert.That(cards.Count == 3);
        }

        [Test]
        public void verifyRules()
        {
            IWebElement link = driver.FindElement(By.LinkText("RULES"));
            Assert.That(link.Text.Equals("RULES"));
            link.Click();
            ReadOnlyCollection<IWebElement> cards = driver.FindElements(By.ClassName("county-card"));
            Assert.That(cards.Count == 2);
        }

        [Test]
        public void verifyProSe()
        {
            IWebElement link = driver.FindElement(By.LinkText("PRO SE"));
            Assert.That(link.Text.Equals("PRO SE"));
            link.Click();
            ReadOnlyCollection<IWebElement> cards = driver.FindElements(By.ClassName("county-card"));
            Assert.That(cards.Count == 3);
            link = driver.FindElement(By.LinkText("Pro Se Forms"));
            link.Click();
            Assert.That(driver.FindElement(By.TagName("h1")).Text == "Pro Se Forms");
        }

        [Test]
        public void verifyForms()
        {
            var link = driver.FindElement(By.LinkText("FORMS"));
            Assert.That(link.Text.Equals("FORMS"));
            link.Click();
            ReadOnlyCollection<IWebElement> rows = driver.FindElements(By.TagName("h3"));
            Assert.That(rows.Count == 12);
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