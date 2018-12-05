using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;

namespace LearningSpecFlow
{
    [Binding]
    public sealed class MySteps
    {
        [Given(@"I am loged in as '(.*)' with password '(.*)'")]
        public void GivenIAmLogedInAsWithPassword(string userName, string password)
        {
            IWebDriver browser = new ChromeDriver();
            ScenarioContext.Current.Add("WebDriver", browser);

            browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/wp-admin");

            browser.FindElement(By.Id("user_login")).SendKeys(userName);
            browser.FindElement(By.Id("user_pass")).SendKeys(password);
            browser.FindElement(By.Id("wp-submit")).Click();
            //todo : dopisać sprawdzenie czy na prawde jestemsy zalogowani
        }

        [Given(@"I add a note with title '(.*)' and content '(.*)'")]
        public void GivenIAddANoteWithTitleAndContent(string title, string content)
        {
            IWebDriver browser = ScenarioContext.Current.Get<IWebDriver>("WebDriver");
            var menuItems = browser.FindElements(By.CssSelector("#adminmenu > li"));
            var entries = menuItems.Single(x => x.Text == "Wpisy");
            entries.Click();


            var subMenuItems = browser.FindElements(By.CssSelector(".wp-submenu > li"));
            var newPost = subMenuItems.Where(x => x.Text == "Dodaj nowy");

            newPost.Single().Click();


            browser.FindElement(By.Id("title")).SendKeys(title);

            var switchToHtmlEditor = browser.FindElement(By.Id("content-html"));
            switchToHtmlEditor.Click();

            var contentWebElement = browser.FindElement(By.Id("content"));
            contentWebElement.SendKeys(content);
        }

        [When(@"I publish a note")]
        public void WhenIPublishANote()
        {
            IWebDriver browser = ScenarioContext.Current.Get<IWebDriver>("WebDriver");

            WaitForClickable(browser, By.CssSelector(".edit-slug.button"), 3);

            browser.FindElement(By.Id("publish")).Click();

            WaitForClickable(browser, By.Id("publish"), 5);

            var slug = browser.FindElement(By.CssSelector("#sample-permalink>a"));
            ScenarioContext.Current.Add("noteUrl",slug.GetAttribute("href"));
        }

        [When(@"logout")]
        public void WhenLogout()
        {
            IWebDriver browser = ScenarioContext.Current.Get<IWebDriver>("WebDriver");
            MoveToElement(browser, By.Id("wp-admin-bar-my-account"));
            var logoutElement = By.Id("wp-admin-bar-logout");
            WaitForClickable(browser, logoutElement, 5);
            browser.FindElement(logoutElement).Click();
        }

        [When(@"I open new note link")]
        public void WhenIOpenNewNoteLink()
        {
            IWebDriver browser = ScenarioContext.Current.Get<IWebDriver>("WebDriver");
            var noteUrl = ScenarioContext.Current.Get<string>("noteUrl");
            browser.Navigate().GoToUrl(noteUrl);
        }

        [Then(@"I am logged out")]
        public void ThenIAmLoggedOut()
        {
            IWebDriver browser = ScenarioContext.Current.Get<IWebDriver>("WebDriver");
            Assert.True(browser.FindElements(By.CssSelector("input#author")).Any());
        }

        [Then(@"I should be able to view the title '(.*)' and content '(.*)'")]
        public void ThenIShouldBeAbleToViewTheTitleAndContent(string title, string content)
        {
            IWebDriver browser = ScenarioContext.Current.Get<IWebDriver>("WebDriver");
            Assert.Equal(title, browser.FindElement(By.CssSelector(".entry-title")).Text);
            Assert.Equal(content, browser.FindElement(By.CssSelector(".entry-content")).Text);
        }


        private void WaitForClickable(IWebDriver browser, By by, int seconds)
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }

        private void WaitForClickable(IWebDriver browser, IWebElement element, int seconds)
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        private void MoveToElement(IWebDriver browser, By selector)
        {
            var element = browser.FindElement(selector);
            MoveToElement(browser, element);
        }

        private void MoveToElement(IWebDriver browser, IWebElement element)
        {
            Actions builder = new Actions(browser);
            Actions moveTo = builder.MoveToElement(element);
            moveTo.Build().Perform();
        }
    }
}
