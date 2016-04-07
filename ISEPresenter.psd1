@{
    RootModule         = 'ISEPresenter.psm1'
    RequiredAssemblies = 'ISEPresenter.dll'
    ModuleVersion      = '1.0.0'
    GUID               = 'CFF2DF3D-5649-4BC1-B560-8F2F24252490'
    Author             = 'Claudio Spizzi'
    Copyright          = 'Copyright (c) 2016 by Claudio Spizzi. Licensed under MIT license.'
    Description        = 'PowerShell ISE Add-On for presenting scripts and demos with a remote control.'
    PowerShellVersion  = '5.0'
    ScriptsToProcess   = @()
    TypesToProcess     = @()
    FormatsToProcess   = @()
    FunctionsToExport  = @()
    CmdletsToExport    = @()
    VariablesToExport  = @()
    AliasesToExport    = @()
    PrivateData        = @{
        PSData             = @{
            Tags               = @('PSModule', 'ISE', 'Add-On', 'Presenter', 'Demo', 'Logitech')
            LicenseUri         = 'https://raw.githubusercontent.com/claudiospizzi/ISEPresenter/master/LICENSE'
            ProjectUri         = 'https://github.com/claudiospizzi/ISEPresenter'
        }
    }
}
