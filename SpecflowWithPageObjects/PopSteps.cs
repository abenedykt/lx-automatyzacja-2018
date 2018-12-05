using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PageObjectFactoryExample;
using System;
using TechTalk.SpecFlow;
using Xunit;

namespace SpecflowWithPageObjects
{
    [Binding]
    public sealed class PopSteps
    {
        [BeforeScenario]
        public void Before()
        {
           ScenarioContext.Current.Set<IWebDriver>(new ChromeDriver());
        }

        [AfterScenario]
        public void After()
        {
            ScenarioContext.Current.Get<IWebDriver>().Quit();
        }
        

        [Given(@"I am loged in as '(.*)' with password '(.*)'")]
        public void GivenIAmLogedInAsWithPassword(string user, string password)
        {
            var browser = ScenarioContext.Current.Get<IWebDriver>();
            var loginPage = new LoginPage(browser);
            var adminPage = loginPage.Login(user, password);

            ScenarioContext.Current.Set(adminPage);
        }

        [When(@"I publish a note")]
        public void WhenIPublishANote()
        {
            var adminPage = ScenarioContext.Current.Get<AdminPage>();
            var newNote = adminPage.OpenNewNotePage();
            var exampleTitle = Faker.Lorem.Sentence();
            var exampleContent = Faker.Lorem.Paragraph();
            var url = newNote.Create(exampleTitle, exampleContent);
            ScenarioContext.Current.Add("NewNoteUrl", url);
            ScenarioContext.Current.Add("ExampleTitle", exampleTitle);
            ScenarioContext.Current.Add("ExampleContent", exampleContent);
        }

        [When(@"logout")]
        public void WhenLogout()
        {
            ScenarioContext.Current.Get<AdminPage>().Logout();
        }

        [When(@"I open new note link")]
        public void WhenIOpenNewNoteLink()
        {
            var browser = ScenarioContext.Current.Get<IWebDriver>();
            var url = ScenarioContext.Current.Get<Uri>("NewNoteUrl");
            ScenarioContext.Current.Set(new NotePage(browser, url));
        }

        [Then(@"I should be able to view the note")]
        public void ThenIShouldBeAbleToViewTheNote()
        {
            var notePage = ScenarioContext.Current.Get<NotePage>();
            var exampleTitle = ScenarioContext.Current.Get<string>("ExampleTitle");
            var exampleContent = ScenarioContext.Current.Get<string>("ExampleContent");

            Assert.Equal(exampleTitle, notePage.Title);
            Assert.Equal(exampleContent, notePage.Content);
        }
    }
}
