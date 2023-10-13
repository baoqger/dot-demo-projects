using OpenQA.Selenium;

namespace SpecFlowTutorial.Pages
{
    public class Page
    {
        public IWebDriver webDriver { get; }
        public Page(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }
        public IWebElement SignInLink => WebDriver.FindElement(By.LinkText("Sign In"));
        public IWebElement UsernameSpace => WebDriver.FindElement(By.Id("username"));
        public IWebElement UsernameOption => WebDriver.FindElement(By.XPath("//*[@id='username']/div[2]"));
        public IWebElement PasswordSpace => WebDriver.FindElement(By.Id("password"));
        public IWebElement PasswordOption => WebDriver.FindElement(By.XPath("//*[@id='password']/div[2]"));
        public IWebElement LoginButton => WebDriver.FindElement(By.Id("login-btn"));
        public IWebElement ProfileName => WebDriver.FindElement(By.LinkText("demouser"));

        public IWebDriver WebDriver { get; }

        public void ClickSignIn() => SignInLink.Click();

        public void ClickLoginButton() => LoginButton.Submit();
        public bool isProfileNameExist() => ProfileName.Displayed;

    }
}