using LivingDoc.Dtos;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System.Reflection;
using System.Xml.Linq;
using TechTalk.SpecFlow;
using System.IO;
using System.Threading;
using DevProject.Models;
using System;
using System.Threading;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using System.Security;
using System.Security.Permissions;


namespace DevProject.StepDefinitions
{
    [Binding]
    public class BaseTest
    {

        public static string BASE_PATH_CONSTANTS = "";
        public RemoteWebDriver driver = null;
        DesiredCapabilities capabilities;
        public bool isRemoteDriver = false;
        private readonly string chrome = "chrome";

        public Dictionary<string, KeyValuePair<string, string>> keyValuePairs;
        private static string BASE_EXT = "*.json";


        [BeforeScenario]
        public void setUp()
        {


            if (Environment.GetEnvironmentVariable("key") == null)
            {

                Console.WriteLine("Test will be running in local");
                BASE_PATH_CONSTANTS = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../") + "Constants";
            }
            else
            {
                Console.WriteLine("Test will be running in Testinium");


                BASE_PATH_CONSTANTS = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../demospecflow/DevProject/") + "Constants";

            }

        }




        [Given(@"Wake up the driver")]
        public void driverAwake()
        {
            if (Environment.GetEnvironmentVariable("key") == null)
            {
                Console.WriteLine("key null ChromeDriver is waking up");
                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                string driverPath = projectDirectory + "\\Drivers";
                // Bonigare C:\Users\testinium\Documents\ProjectsTestSolutions\SpecflowProject\demospecflow\DevProject\Documents\OzanDemoWillRemove\DevProject\Drivers\chromedriver.exe does not exist
                //C:\Users\testinium\Documents\ProjectsTestSolutions\SpecflowProject\demospecflow\DevProject\Drivers
                ChromeOptions option1 = new ChromeOptions();
                option1.AddArguments("disable-popup-blocking");
                option1.AddArguments("ignore-certificate-errors");
                option1.AddArguments("--ignore-ssl-errors=yes");
                option1.AddArguments("start-maximized");
                driver = new ChromeDriver(driverPath, option1);
                isRemoteDriver = false;
                Console.WriteLine("ChromeDriver woke");

            }
            else
            {
                Console.WriteLine("Remote ChromeDriver is waking up");

                ChromeOptions options = new ChromeOptions();
                options.AddArguments("disable-popup-blocking");
                options.AddArguments("ignore-certificate-errors");
                options.AddArguments("--ignore-ssl-errors=yes");
                Console.WriteLine("options added");

                capabilities = (DesiredCapabilities)options.ToCapabilities();
                capabilities.SetCapability("testinium:browserName", chrome);
                Console.WriteLine(chrome);
                capabilities.SetCapability("testinium:key", Environment.GetEnvironmentVariable("key"));
                Console.WriteLine(TestContext.Parameters.Get("key"));
                Console.WriteLine(Environment.GetEnvironmentVariable("key"));
                //capabilities.SetCapability("platformName", "Windows 10");
                //  capabilities.SetCapability("acceptInsecureCerts", true);
                Console.WriteLine("capability added");

               // driver = new RemoteWebDriver(new Uri("http://10.1.33.193:4444/wd/hub"), capabilities, TimeSpan.FromSeconds(60));
                 //driver = new RemoteWebDriver(new Uri("https://hubdev.testinium.com/wd/hub"), capabilities, TimeSpan.FromSeconds(60));
               driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capabilities, TimeSpan.FromSeconds(60));

                isRemoteDriver = true;
                Console.WriteLine("ChromeDriver woke");

            }

        }


        [AfterScenario]
        public void AfterScenario()
        {


            try
            {
                if (null != driver)
                {
                    driver.Quit();
                    Console.WriteLine("Driver Quited After Scenario");
                }
            }
            catch
            {
                Console.WriteLine("Driver is already closed");
            }
        }






        [Given(@"Get (.*) dictionary folders json")]
        public void createDictionary(string key)
        {
            keyValuePairs = GetDictionary(key);

        }


        [Given(@"Add these two number (.*) ,(.*)")]
        public void createDictionary(string key, string key2)
        {



        }





        //
        [Given(@"Connect to DB via (.*) connectionString")]
        public void connecToOutDB(string connectionString)
        {

            //DB connection functions here
        }




        public Dictionary<string, KeyValuePair<string, string>> GetDictionary(string key)
        {
            Dictionary<string, KeyValuePair<string, string>> dic = new Dictionary<string, KeyValuePair<string, string>>();
            var txtFiles = Directory.EnumerateFiles(BASE_PATH_CONSTANTS + "/" + key, BASE_EXT);
            foreach (string currentFile in txtFiles)
            {
                var json = File.ReadAllText(currentFile);
                Dictionary<string, Element> d = JsonConvert.DeserializeObject<IEnumerable<Element>>(json).
                 Select(p => (Id: p.key, Record: p)).
                 ToDictionary(t => t.Id, t => t.Record);
                //Console.WriteLine("Okunan dosya: " + currentFile + " element sayısı: " + d.Count);
                foreach (var item in d)
                {
                    dic.Add(item.Key.ToString(), new KeyValuePair<string, string>(item.Value.type, item.Value.value));
                    //Console.WriteLine("Sözlüğe eklenen element -> Key:" + item.Key + " type: " + item.Value.type + " value: " + item.Value.value);
                }

            }

            Console.WriteLine("Total element count in the dictionary" + dic.Count);
            return dic;
        }


    }
}
