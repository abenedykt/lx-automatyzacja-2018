using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace LearningSpecFlow
{
    [Binding]
    public class OtherSteps
    {
        [Before(Order = 0)]
        private void StartBrowser()
        {
            IWebDriver browser = new ChromeDriver();
            ScenarioContext.Current.Add("WebDriver", browser);
        }

        [Given(@"I open new post page")]
        public void GivenIOpenNewPostPage()
        {
        }

        [Given(@"type in a title and message")]
        public void GivenTypeInATitleAndMessage()
        {
        }

        [When(@"I press publish")]
        public void WhenIPressPublish()
        {
        }

        [Then(@"its published")]
        public void ThenItsPublished()
        {
        }
    }

    [Binding]
    public class UsingBackgroundSteps
    {

        [Before("WithBackground", Order = 0)]
        private void StartBrowser()
        {
            IWebDriver browser = new ChromeDriver();
            ScenarioContext.Current.Add("WebDriver", browser);
        }

        [Given(@"I am logged in as admin")]
        public void GivenIAmLoggedInAsAdmin()
        {
        }
    }
}
