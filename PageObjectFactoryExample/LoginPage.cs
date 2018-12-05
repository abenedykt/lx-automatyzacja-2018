using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjectFactoryExample
{
    internal class LoginPage : BasePage
    {
        public LoginPage(IWebDriver browser) : base(browser)
        {
            _browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/wp-admin");
        }

        internal AdminPage Login(string user, string password)
        {
            User.SendKeys(user);
            Password.SendKeys(password);
            LogIn.Click();

            return new AdminPage(_browser);
        }

        [FindsBy(How = How.Id, Using = "user_login")]
        private IWebElement User { get; set; }

        [FindsBy(How = How.Id, Using = "user_pass")]
        private IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "wp-submit")]
        private IWebElement LogIn { get; set; }
    }
}