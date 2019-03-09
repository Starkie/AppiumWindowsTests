namespace AppiumTests
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium.Appium;
    using OpenQA.Selenium.Appium.Windows;

    /// <summary>
    /// Class for implementing tests on the Notepad windows application.
    /// </summary>
    [TestClass]
    public class NotepadTests
    {
        private const string testFolderRelativePath = @"../MyTestFolder/";
        private const string testFileName = "MyTestFile.txt";

        private WindowsDriver<WindowsElement> windowsDriver;

        [TestInitialize]
        public void SetUp()
        {
            DirectoryInfo testFolder = GetTestDirectory();

            // Create the text file or overwrite it if it exists.
            string testFileFullPath = $"{testFolder.FullName}/{testFileName}";
            File.WriteAllText(testFileFullPath, "");

            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", @"C:\Windows\System32\notepad.exe");
            appiumOptions.AddAdditionalCapability("platformName", "Windows");
            appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");
            appiumOptions.AddAdditionalCapability("appArguments", testFileFullPath);

            this.windowsDriver = new WindowsDriver<WindowsElement>(new Uri(@"http://127.0.0.1:4723/wd/hub"), appiumOptions);
        }

        [TestMethod]
        public void InputTextInNotepadWindow()
        {
            // Arrange.
            string expectedText = "Say hello world.";

            // Act.
            WindowsElement editTextBox = this.windowsDriver.FindElementByClassName("Edit");
            editTextBox.SendKeys(expectedText);

            // Assert.
            Assert.AreEqual(expectedText, editTextBox.Text, $"The expected text was '{expectedText}' but found '{editTextBox.Text}'.");
        }

        private static DirectoryInfo GetTestDirectory()
        {
            DirectoryInfo testFolder = new DirectoryInfo(testFolderRelativePath);

            if (!testFolder.Exists)
            {
                testFolder.Create();
            }

            return testFolder;
        }
    }
}