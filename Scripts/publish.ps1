
# Definition for build
$Module = 'ISEPresenter'
$Source = "C:\Projects\$Module"

# Extract module version
$ModuleVersion = (Invoke-Expression -Command (Get-Content -Path "$Source\bin\Release\$Module.psd1" -Raw)).ModuleVersion

# Rename the release folder
Rename-Item -Path "$Source\bin\Release" -NewName $Module

# Push appveyor artifacts
Compress-Archive -Path "$Source\bin\$Module" -DestinationPath "$Source\$Module-$ModuleVersion-$env:APPVEYOR_BUILD_VERSION.zip"
Push-AppveyorArtifact -Path "$Source\$Module-$ModuleVersion-$env:APPVEYOR_BUILD_VERSION.zip" -DeploymentName $Module
