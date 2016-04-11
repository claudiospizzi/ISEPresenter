[![AppVeyor - master](https://ci.appveyor.com/api/projects/status/h14qy0k9jih4sp78/branch/master?svg=true)](https://ci.appveyor.com/project/claudiospizzi/isepresenter/branch/master) [![AppVeyor - dev](https://ci.appveyor.com/api/projects/status/h14qy0k9jih4sp78/branch/dev?svg=true)](https://ci.appveyor.com/project/claudiospizzi/isepresenter/branch/dev) [![PowerShell Gallery - ISEPresenter](https://img.shields.io/badge/PowerShell%20Gallery-ISEPresenter-0072C6.svg)](https://www.powershellgallery.com/packages/ISEPresenter)

# ISEPresenter PowerShell Module
PowerShell ISE Add-On for presenting scripts and demos with a remote control.


## Introduction

The ISEPresenter is a PowerShell ISE Add-On, designed to give teachers and speakers an easier way to present PowerShell scripts or demos inside the PowerShell ISE. In addition of just executing the statements step by step, the ISE Presenter module also provides support for remote control devices like the Logitech Presenter R400. Thanks to this support, it's possible to execute the scripts or demos with a remote control.

After you have [installed](https://github.com/claudiospizzi/ISEPresenter#installation) the ISEPresenter module, just start it by importing module inside the PowerShell ISE with the command `Import-Module ISEPresenter`. The Add-On will apear on the right side.

![ISEPresenter screenshot in the ready state.](https://raw.githubusercontent.com/claudiospizzi/ISEPresenter/dev/Assets/Screenshot-Ready.png)

To control the presenter, you can first select the remote device and next use the following buttons to start, pause or stop the presentation:

* ![Play](https://raw.githubusercontent.com/claudiospizzi/ISEPresenter/dev/Assets/Play.png) Play
* ![Play](https://raw.githubusercontent.com/claudiospizzi/ISEPresenter/dev/Assets/Pause.png) Pause
* ![Play](https://raw.githubusercontent.com/claudiospizzi/ISEPresenter/dev/Assets/Stop.png) Stop

After the presenter was started, it will parse the current active opened script file and afterwards highlight the first detected statement. Now you can use the remote device to navigate throught the statements (back / forward), execute the current statement (run) or clear the PowerShell Console (clear). The active key or button for this actions depends on the selected remote control. You can find a short descripton below the dropdown box.

![ISEPresenter screenshot in the ready state.](https://raw.githubusercontent.com/claudiospizzi/ISEPresenter/dev/Assets/Screenshot-Running.png)

If you have completed your presentation, stop it with the according button. After you have stopped, the remote control device will no longer be active.


## Requirenments

The following minimum requirenments are necessary to use the module:

* Windows PowerShell 3.0
* Windows Server 2008 R2 / Windows 7


## Installation

Install the module **automatically** from the [PowerShell Gallery](https://www.powershellgallery.com/packages/ISEPresenter) with PowerShell 5.0:

```powershell
Install-Module ISEPresenter
```

To install the module **manually**, perform the following steps:

1. Download the latest release from [GitHub](https://github.com/claudiospizzi/isepresenter/releases) as a ZIP file
2. Extract the downloaded module into one of your module paths ([TechNet: Installing Modules](https://technet.microsoft.com/en-us/library/dd878350))


## Supported Remote Control Devices

### Generic Keyboard

![Generic Keyboard](https://raw.githubusercontent.com/claudiospizzi/ISEPresenter/dev/Assets/GenericKeyboard-100.png)

To present a script or demo, not only dedicated remote control devices are suppored. You can also use the keyboard. Therefore four keys are bound to special functions during the presentation.

### Logitech Presenter R400

![Logitech Presenter R400](https://raw.githubusercontent.com/claudiospizzi/ISEPresenter/dev/Assets/LogitechPresenterR400-100.png)

The Logitech Presenter R400 is designed to control PowerPoint slideshows. With the ISEPresenter module, this device can be used to control a PowerShell demo.

## Versions

### 1.0.0

* Initial public release
* Support for Generic Keyboard
* Support for Logitech Presenter R400


## Contribute

Please feel free to contribute by opening new issues or providing pull requests.
