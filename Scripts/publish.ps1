
# Definition for build
$Module = 'ISEPresenter'
$Source = "C:\Projects\$Module"

# Extract module version
$ModuleVersion = (Invoke-Expression -Command (Get-Content -Path "$Source\bin\Release\$Module.psd1" -Raw)).ModuleVersion

# Push appveyor artifacts
Compress-Archive -Path "$Source\bin\Release" -DestinationPath "$Source\$Module-$ModuleVersion-$env:APPVEYOR_BUILD_VERSION.zip"
Push-AppveyorArtifact -Path "$Source\$Module-$ModuleVersion-$env:APPVEYOR_BUILD_VERSION.zip" -DeploymentName $Module
