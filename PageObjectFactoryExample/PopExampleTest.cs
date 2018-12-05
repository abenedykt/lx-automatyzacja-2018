using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using Xunit;

namespace PageObjectFactoryExample
{
    public class PopExampleTest : IDisposable
    {
        private readonly IWebDriver _browser;

        public PopExampleTest()
        {
            _browser = new ChromeDriver();
        }

        [Fact]
        public void Can_publish_new_note()
        {
            
            string ExampleTitle = Faker.Lorem.Sentence();
            string ExampleContent = Faker.Lorem.Paragraph();

            var loginPage = new LoginPage(_browser);
            var adminPage = loginPage.Login("automatyzacja", "jesien2018");
            var newNote =  adminPage.OpenNewNotePage();
            var url = newNote.Create(ExampleTitle, ExampleContent);
            adminPage.Logout();

            var notePage = new NotePage(_browser, url);

            Assert.Equal(ExampleTitle, notePage.Title);
            Assert.Equal(ExampleContent, notePage.Content);
        }

        public void Dispose()
        {
            _browser.Quit();
        }
    }
}
