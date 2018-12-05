using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjectFactoryExample
{
    internal class AdminPage
    {
        private readonly IWebDriver _browser;

        public AdminPage(IWebDriver browser)
        {
            _browser = browser;
            PageFactory.InitElements(_browser, this);
        }

        [FindsBy(How=How.CssSelector, Using = "#adminmenu > li")]
        public IList<IWebElement> MenuItems { get; set; }

        [FindsBy(How=How.CssSelector, Using = ".wp-submenu > li")]
        public IList<IWebElement> SubMenuItems { get; set; }

        internal NewNotePage OpenNewNotePage()
        {
            var entries = MenuItems.Single(x => x.Text == "Wpisy");
            entries.Click();
            
            var newPost = SubMenuItems.Where(x => x.Text == "Dodaj nowy");

            newPost.Single().Click();

            return new NewNotePage(_browser);
        }
    }
}