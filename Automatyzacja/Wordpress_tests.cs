using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using Xunit;

namespace Automatyzacja
{
    public class Wordpress_tests : IDisposable
    {
        private IWebDriver _browser;

        public Wordpress_tests()
        {
            _browser = new ChromeDriver();
        }

        [Fact]
        public void Can_publish_new_note()
        {
            const string ExampleNoteTitle = "my example note";
            const string ExampleNoteContent = "lorem ipsum dolor sit amet";
            
            _browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/wp-admin/");
            _browser.FindElement(By.Id("user_login")).SendKeys("automatyzacja");
            _browser.FindElement(By.Id("user_pass")).SendKeys("jesien2018");
            _browser.FindElement(By.Id("wp-submit")).Click();

            var menuItems = _browser.FindElements(By.CssSelector("#adminmenu > li"));
            var entries = menuItems.Single(x => x.Text == "Wpisy");
            entries.Click();


            var subMenuItems = _browser.FindElements(By.CssSelector(".wp-submenu > li"));
            var newPost = subMenuItems.Where(x => x.Text == "Dodaj nowy");

            newPost.Single().Click();


            _browser.FindElement(By.Id("title")).SendKeys(ExampleNoteTitle);

            var switchToHtmlEditor = _browser.FindElement(By.Id("content-html"));
            switchToHtmlEditor.Click();

            var content = _browser.FindElement(By.Id("content"));
            content.SendKeys(ExampleNoteContent);

            WaitForClickable(By.CssSelector(".edit-slug.button"),3);

            _browser.FindElement(By.Id("publish")).Click();

            WaitForClickable(By.Id("publish"), 5);

            var slug = _browser.FindElement(By.CssSelector("#sample-permalink>a"));
            var noteUrl = slug.GetAttribute("href");

            MoveToElement(By.Id("wp-admin-bar-my-account"));
            var logoutElement = By.Id("wp-admin-bar-logout");
            WaitForClickable(logoutElement, 5);
            _browser.FindElement(logoutElement).Click();

            _browser.Navigate().GoToUrl(noteUrl);

            Assert.True(_browser.FindElements(By.CssSelector("input#author")).Any());

            Assert.Equal(ExampleNoteTitle, _browser.FindElement(By.CssSelector(".entry-title")).Text);
            Assert.Equal(ExampleNoteContent, _browser.FindElement(By.CssSelector(".entry-content")).Text);
        }

        protected void WaitForClickable(By by, int seconds)
        {
            var wait = new WebDriverWait(_browser, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }

        protected void WaitForClickable(IWebElement element, int seconds)
        {
            var wait = new WebDriverWait(_browser, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        protected void MoveToElement(By selector)
        {
            var element = _browser.FindElement(selector);
            MoveToElement(element);
        }

        protected void MoveToElement(IWebElement element)
        {
            Actions builder = new Actions(_browser);
            Actions moveTo = builder.MoveToElement(element);
            moveTo.Build().Perform();
        }

        public void Dispose()
        {
            _browser.Quit();
        }
    }
}
