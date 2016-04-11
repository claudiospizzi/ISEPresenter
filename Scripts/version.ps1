
# Definition for build
$Module = 'ISEPresenter'
$Source = "C:\Projects\$Module"

# Extract module version
$ModuleVersion = '1.2.3' #(Invoke-Expression -Command (Get-Content -Path "$Source\bin\Release\$Module.psd1" -Raw)).ModuleVersion

# Target assembly version strings
$AssemblyVersion     = "AssemblyVersion(`"$ModuleVersion`")"
$AssemblyFileVersion = "AssemblyFileVersion(`"$ModuleVersion`")"

# Regex serarch pattern to replace assembly version
$AssemblyVersionPattern     = 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
$AssemblyFileVersionPattern = 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'

# Patch the assembly version
(Get-Content -Path "$Source\Properties\AssemblyInfo.cs") | ForEach-Object {
    $_ = $_ -replace $AssemblyVersionPattern, $AssemblyVersion
    $_ = $_ -replace $AssemblyFileVersionPattern, $AssemblyFileVersion
    $_
} | Out-File -FilePath "$Source\Properties\AssemblyInfo.cs" -Encoding UTF8 -Force
