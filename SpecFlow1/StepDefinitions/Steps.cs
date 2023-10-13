using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlowTutorial.Pages;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTutorial.StepDefinitions
{
    [Binding]

    public sealed class Steps
    {
        Page Page;

        [Given(@"I open bstackdemo homepage")]

        public void GivenIOpenBstackdemoHomepage()
        {
            IWebDriver webDriver = new ChromeDriver();
            webDriver.Navigate().GoToUrl("https://bstackdemo.com/");
            Page = new Page(webDriver);
        }

        [Given(@"I click signin link")]
        public void GivenIClickSignInLink()
        {
            Page.ClickSignIn();
        }

        [Given(@"I enter the login details")]
        public void GivenIEnterTheLoginDetails()
        {
            Page.UsernameSpace.Click();
            Page.UsernameOption.Click();
            Page.PasswordSpace.Click();
            Page.PasswordOption.Click();

        }

        [Given(@"I click login button")]
        public void GivenIClickLoginButton()
        {
            Page.ClickLoginButton();
        }

        [Then(@"Profile name should appear")]
        public void ThenProfileNameShouldAppear()
        {
            Assert.That(Page.isProfileNameExist(), Is.True);
        }

    }

}