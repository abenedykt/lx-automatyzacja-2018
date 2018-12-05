using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;
using System.Linq;

namespace PageObjectFactoryExample
{
    internal class AdminPage : BasePage
    {
        public AdminPage(IWebDriver browser) : base(browser)
        {
            _browser = browser;
        }

        internal NewNotePage OpenNewNotePage()
        {
            var entries = MenuItems.Single(x => x.Text == "Wpisy");
            entries.Click();

            var newPost = SubMenuItems.Where(x => x.Text == "Dodaj nowy");

            newPost.Single().Click();

            return new NewNotePage(_browser);
        }

        internal void Logout()
        {
            MoveToElement(AccountTopBar);
            WaitForClickable(LogoutButton, 5);
            LogoutButton.Click();
        }

        [FindsBy(How = How.Id, Using = "wp-admin-bar-my-account")]
        private IWebElement AccountTopBar { get; set; }

        [FindsBy(How = How.Id, Using = "wp-admin-bar-logout")]
        private IWebElement LogoutButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#adminmenu > li")]
        private IList<IWebElement> MenuItems { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".wp-submenu > li")]
        private IList<IWebElement> SubMenuItems { get; set; }
    }
}