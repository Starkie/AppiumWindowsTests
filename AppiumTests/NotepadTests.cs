namespace AppiumTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Appium;
    using OpenQA.Selenium.Appium.Windows;

    /// <summary>
    /// Class for implementing tests on the Notepad windows application.
    /// </summary>
    [TestClass]
    public class NotepadTests
    {
        // Change this path to point to your appium or winappdriver server.
        private const string appiumServer = @"http://127.0.0.1:4723/wd/hub";

        private const string testFolderRelativePath = @"../MyTestFolder/";

        [ClassInitialize]
        public static void TestEnvironmentSetUp(TestContext context)
        {
            CreateTestFolder();
        }

        private static WindowsDriver<WindowsElement> InstanceWebDriver(IDictionary<string, string> additionalCapabilities = null)
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", @"C:\Windows\System32\notepad.exe");
            appiumOptions.AddAdditionalCapability("platformName", "Windows");
            appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");

            foreach (KeyValuePair<string, string> capatibility in additionalCapabilities)
            {
                appiumOptions.AddAdditionalCapability(capatibility.Key, capatibility.Value);
            }

            return new WindowsDriver<WindowsElement>(new Uri(appiumServer), appiumOptions);
        }

        /// <summary>
        /// Test opening an empty text file with notepad and inputing a given text.
        /// </summary>
        [TestMethod]
        public void InputTextInNotepadWindow()
        {
            // Arrange.
            string expectedText = "Say hello world.";

            // Create an empty text file.
            DirectoryInfo currentTestFolder = CreateTestFolder(Path.GetRandomFileName());
            string testFileFullPath = Path.Combine(currentTestFolder.FullName, "myTestFile.txt");
            File.WriteAllText(testFileFullPath, "");

            IDictionary<string, string> additionalCapabilities = new Dictionary<string, string>();
            additionalCapabilities.Add("appWorkingDir", currentTestFolder.FullName);
            additionalCapabilities.Add("appArguments", testFileFullPath);

            using (WindowsDriver<WindowsElement> driver = InstanceWebDriver(additionalCapabilities))
            {
                // Act.
                WindowsElement editTextBox = driver.FindElementByClassName("Edit");
                editTextBox.SendKeys(expectedText);

                editTextBox.Click();

                // TODO: Fix SAVE Key combination.
                driver.Keyboard.PressKey(Keys.Control + "G" + Keys.Control);

                // Assert.
                Assert.AreEqual(expectedText, editTextBox.Text, $"The expected text was '{expectedText}' but found '{editTextBox.Text}'.");
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            DirectoryInfo testFolder = new DirectoryInfo(testFolderRelativePath);

            if (testFolder?.Exists == true)
            {
                testFolder.Delete(recursive: true);
            }
        }

        private static DirectoryInfo CreateTestFolder(string subfolderRelativePath = null)
        {
            DirectoryInfo testFolder = new DirectoryInfo(Path.Combine(testFolderRelativePath, subfolderRelativePath ?? ""));

            if (!testFolder.Exists)
            {
                testFolder.Create();
            }

            return testFolder;
        }
    }
}