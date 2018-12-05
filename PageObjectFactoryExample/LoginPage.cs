using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjectFactoryExample
{
    internal class LoginPage
    {
        private readonly IWebDriver _browser;

        public LoginPage(IWebDriver browser)
        {
            _browser = browser;
            _browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/wp-admin");

            PageFactory.InitElements(_browser, this);
        }

        [FindsBy(How=How.Id, Using ="user_login")]
        public IWebElement User { get; set; }

        [FindsBy(How = How.Id, Using = "user_pass")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "wp-submit")]
        public IWebElement LogIn { get; set; }

        internal AdminPage Login(string user, string password)
        {
            User.SendKeys(user);
            Password.SendKeys(password);
            LogIn.Click();

            return new AdminPage(_browser);
        }
    }
}