using DevProject.StepDefinitions;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using System.IO;
using System;
using System.Threading;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using DevProject.Configurations;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Security.Permissions;


namespace DevProject
{
    [Binding]
    public class BasePage
    {


        public RemoteWebDriver driver;
        private BaseTest _baseTest;
        private IJavaScriptExecutor javaScriptExecutor;
        public BasePage(BaseTest baseTest)
        {

            _baseTest = baseTest;
        }


        [Given(@"Click (.*) Element By (.*)")]
        public void clickElement(string key, string type)
        {
            IWebElement textAreaElement = null;



            if ("id".Equals(type))
            {
                textAreaElement = _baseTest.driver.FindElementById(key);
            }
            else if ("name".Equals(type))
            {
                textAreaElement = _baseTest.driver.FindElementByName(key);
            }
            else if ("xpath".Equals(type))
            {
                textAreaElement = _baseTest.driver.FindElementByXPath(key);
            }
            else
            {
                Console.WriteLine("The type value of element is not valid");
            }

            textAreaElement.Click();





            ///
        





        }



        [Given(@"Driver Navigate to (.*)")]
        public void navigate(string key)
        {
            _baseTest.driver.Navigate().GoToUrl(key);
        }

        [Given(@"Send (.*) key to (.*) type (.*) element")]
        public void sendKeys( string text, string type, string key)
        {


            IWebElement textAreaElement=null;


        if ("id".Equals(type))
            {
                textAreaElement = _baseTest.driver.FindElementById(key);
            }
            else if ("name".Equals(type))
            {
                textAreaElement = _baseTest.driver.FindElementByName(key);
            }
            else if ("xpath".Equals(type))
            {
                textAreaElement = _baseTest.driver.FindElementByXPath(key);
            }
            else
            {
                Console.WriteLine("The type value of element is not valid");
            }

            textAreaElement.SendKeys(text);

        }


        [Given(@"Check (.*) type (.*) element is equasls (.*)")]
        public void GivenCheckİf(string key , string type , string assertKey)
        {

            IWebElement myErorrMessage = null;


            if ("id".Equals(type))
            {
                myErorrMessage = _baseTest.driver.FindElementById(key);
            }
            else if ("name".Equals(type))
            {
                myErorrMessage = _baseTest.driver.FindElementByName(key);
            }
            else if ("xpath".Equals(type))
            {
                myErorrMessage = _baseTest.driver.FindElementByXPath(key);
            }
            else
            {
                Console.WriteLine("The type value of element is not valid");
            }


            Assert.AreEqual(assertKey, myErorrMessage.Text.Trim());



        }









        //==================================================================NewSteps=====================================================================
        public void goToUrl(string uri)
        {
            _baseTest.driver.Navigate().GoToUrl(uri);

        }
        public void clickBP(string key)
        {
            Thread.Sleep(1000);
            findElement(key).Click();
        }
        public void clickStale(string key)
        {
            Thread.Sleep(1000);
            findElementStale(key).Click();
        }
        public void clickDD(string key, string key2)
        {
            findElement(key).Click();
            findElement(key2).Click();
        }

        public void fillTextBox(string key, string text)
        {
            findElement(key).SendKeys(text);
        }

        public void fillPasswordBox(string key, string passId)
        {
            Console.WriteLine(_baseTest.keyValuePairs[passId].Value);
           
        }





        [Given(@"secret Send (.*) key to (.*) element")]
        public void fillPasswordBoxSentence(string text, string key)
        {
            fillPasswordBox(key, text);
        }

        public void fileUpload()
        {
            var allowsDetection = _baseTest.driver as IAllowsFileDetection;
            if (allowsDetection != null)
            {
                allowsDetection.FileDetector = new LocalFileDetector();
            }
        }

        public void checkElementIsDisplayed(string key)
        {
            Assert.IsTrue(findElement(key).Displayed, key + "' not found");
        }
        public void checkElement(string key)
        {
            Assert.AreNotEqual(findElement(key), null + "not found");
        }

        public void browserMaximize()
        {
            _baseTest.driver.Manage().Window.Maximize();
        }
        public void goBack()
        {
            _baseTest.driver.Navigate().Back();
        }

        public void forward()
        {
            _baseTest.driver.Navigate().Forward();
        }
        public void leftPress(string key, string text)
        {
            findElement(key).SendKeys(Keys.Home);
            fillTextBox(key, text);

        }

        public void refreshPage()
        {
            _baseTest.driver.Navigate().Refresh();
        }
        public void switchToLastWindow()

        {
            _baseTest.driver.SwitchTo().Window(_baseTest.driver.WindowHandles.Last());
        }

        public void pageContainsText(string text)

        {
            Assert.IsTrue(_baseTest.driver.PageSource.Contains(text), "page contains " + text + " value");
        }

        public void elementContainsText(string key, string text)

        {
            Assert.AreEqual(findElement(key).Text.ToString(), text, "text not match..");
        }


        public By generateElementBy(string by, string value)
        {
            Console.WriteLine("generateElementBy by: " + by + " value: " + value);
            By byElement = null;
            if (by.Equals(ElementType.id))
            {
                byElement = By.Id(value);
            }
            else if (by.Equals(ElementType.name))
            {
                byElement = By.Name(value);
            }
            else if (by.Equals(ElementType.className))
            {
                byElement = By.ClassName(value);
            }
            else if (by.Equals(ElementType.cssSelector))
            {
                byElement = By.CssSelector(value);
            }
            else if (by.Equals(ElementType.xpath))
            {
                byElement = By.XPath(value);
            }
            else if (by.Equals(ElementType.linkText))
            {
                byElement = By.LinkText(value);
            }
            else
            {
                Assert.Fail("No such selector.");
            }
            return byElement;
        }

        public IWebElement findElement(string key)
        {
            By by = generateElementBy(_baseTest.keyValuePairs[key].Key, _baseTest.keyValuePairs[key].Value);
            WebDriverWait wait = new WebDriverWait(_baseTest.driver, TimeSpan.FromSeconds(AppConficReader.GetElementLoadTimeout()));
            IWebElement webElement = (IWebElement)wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));//
            return webElement;
        }
        public IList<IWebElement> findElements(string key)
        {
            By by = generateElementBy(_baseTest.keyValuePairs[key].Key, _baseTest.keyValuePairs[key].Value);
            WebDriverWait wait = new WebDriverWait(_baseTest.driver, TimeSpan.FromSeconds(AppConficReader.GetElementLoadTimeout()));
            IList<IWebElement> webElements = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
            return webElements;
        }

        public IWebElement findElementStale(string key)
        {
            By by = generateElementBy(_baseTest.keyValuePairs[key].Key, _baseTest.keyValuePairs[key].Value);

            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(_baseTest.driver);
            wait.Timeout = TimeSpan.FromSeconds(AppConficReader.GetElementLoadTimeout());
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            wait.Message = "StaleElementReferenceException";
            IWebElement webElement = wait.Until(ExpectedConditions.ElementToBeClickable(by));
            return webElement;
        }


        public void scrollElement(IWebElement element)
        {
            javaScriptExecutor = (IJavaScriptExecutor)_baseTest.driver;
            javaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView({ behaviour: 'smooth', block: 'center', inline: 'center'});", element);
        }

        public void clickJS(string key)
        {
            By by = generateElementBy(_baseTest.keyValuePairs[key].Key, _baseTest.keyValuePairs[key].Value);
            WebDriverWait wait = new WebDriverWait(_baseTest.driver, TimeSpan.FromSeconds(AppConficReader.GetElementLoadTimeout()));
            IWebElement webElement = wait.Until(ExpectedConditions.ElementExists(by));
            scrollElement(webElement);
            javaScriptExecutor = (IJavaScriptExecutor)_baseTest.driver;
            javaScriptExecutor.ExecuteScript("arguments[0].click();", webElement);

        }

        public void clearElement(string key)
        {
            findElement(key).Clear();
        }
        ////sentences
      

        [Given(@"Click (.*) element")]
        public void clickElementSentence(string key)
        {
            clickBP(key);
        }

        [Given(@"Stale click (.*) element")]
        public void clickElementStaleSentence(string key)
        {
            clickStale(key);
        }

        [Given(@"Click via JS (.*) element")]
        public void clickElementJS(string key)
        {
            clickJS(key);
        }

        [Given(@"Click (.*),(.*) elements with order")]
        public void clickElementSentence(string key, string key2)
        {
            clickDD(key, key2);
        }

        [Given(@"Check (.*) element is displayed")]
        public void checkElementIsDisplayedSentence(string key)
        {
            checkElementIsDisplayed(key);
        }

        [Given(@"Check (.*) element is not null")]
        public void checkElementIsNull(string key)
        {
            checkElement(key);
        }

        [Given(@"Send (.*) key to (.*) element")]
        public void fillTextBoxSentence(string text, string key)
        {
            fillTextBox(key, text);
        }
        
       
        [Given(@"Go to (.*) Url")]
        public void goToUrlSentence(string url)
        {
            goToUrl(url);
            Console.WriteLine(url + " is opened");
        }


        [Given(@"Wait (.*) seconds")]
        public void waitBySecond(int second)
        {
            Thread.Sleep(second * 1000);
        }

        [Given(@"Go back")]
        public void goBackSentence()
        {
            goBack();
        }

        [Given(@"Go forward")]
        public void forwardSentence()
        {
            forward();
        }

        [Given(@"Reflesh page")]
        public void refreshPageSentence()
        {
            refreshPage();
        }

        [Given(@"Switch to last tab")]
        public void switchToLastWindowSentence()
        {
            switchToLastWindow();
        }

        [Given(@"File Upload")]
        public void fileUploadSentence()
        {
            fileUpload();
        }

        [Given(@"Page contains (.*) text")]
        public void pageContainsTextSentence(string text)
        {
            pageContainsText(text);
        }

        [Given(@"(.*) element contains (.*) text")]
        public void elementContainsTextSentence(string key, string text)
        {
            elementContainsText(key, text);
        }


        [Given(@"Driver Quit")]
        public void driverClose()
        {
            _baseTest.driver.Quit();
            Console.WriteLine("Driver Quited");
        }


 
        [Given(@"Clear (.*) element")]
        public void clearElementSentence(string key)
        {
            clearElement(key);
            Console.WriteLine(key + "element is cleared");
        }





    }
}