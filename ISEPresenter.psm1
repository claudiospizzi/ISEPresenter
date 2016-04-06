
if ($Host.Name -ne 'Windows PowerShell ISE Host')
{
    Write-Warning 'ISEPresenter must be run inside PowerShell ISE host environment.'
    return
}

if ($PSVersionTable.PSVersion.Major -lt 5)
{
    Write-Warning 'ISEPresenter requires PowerShell 5.0 or above.'
    return
}

$psISE.CurrentPowerShellTab.VerticalAddOnTools.Add('Presenter', [ISEPresenter.Views.MainView], $true)
