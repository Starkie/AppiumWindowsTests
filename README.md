# AppiumWindowsTests
A personal test project for trying out the [WinAppDriver](https://github.com/Microsoft/WinAppDriver), the official Appium driver to automate testing of UWP, WPF and Windows.Forms applications.

### Requirements:
------------------
The WinAppDriver works only on a Window 10 computer, with the [Developer Mode enabled](https://docs.microsoft.com/en-us/windows/uwp/get-started/enable-your-device-for-development).

### Setting up the environment:
------------------------
This guide is based on an [Automate The Planet article](https://www.automatetheplanet.com/automate-windows-desktop-apps-winappdriver/) and the [Appium documentation](http://appium.io/docs/en/drivers/windows/).

1. Install [Appium](https://github.com/appium/appium-desktop/releases/).
    - In my case, I used the Appium Desktop version.
2. Download the latest release of the WinAppDriver. You can do this either from:
    * [Their oficial repo](https://github.com/Microsoft/WinAppDriver/releases). At the time of writing, its the v1.1. 
    * ... or throught the npm: `$ npm install -g  appium-windows-driver`. (This needs to be in done in an administrator console)

3. Enable the [Windows Developer Mode](https://docs.microsoft.com/en-us/windows/uwp/get-started/enable-your-device-for-development).

Now you should be able to run instances of the WinAppDriver.

### Running the tests:
------------------------
* To run the tests, [an Appium server needs to be started](http://appium.io/docs/en/about-appium/getting-started/index.html#starting-appium). By default, it will run on the local address `http://127.0.0.1:4723/wd/hub`.

1. Clone the repo and restore the nuget packages of the project.
2. Check that the `appiumServer` parameter in the test class points to your server.
3. Run the tests.
