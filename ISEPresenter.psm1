
$Name = 'Presenter'

if ($Host.Name -ne 'Windows PowerShell ISE Host')
{
    Write-Warning 'ISEPresenter must be run inside PowerShell ISE host environment.'
    return
}

if ($PSVersionTable.PSVersion.Major -lt 3)
{
    Write-Warning 'ISEPresenter requires PowerShell 3.0 or above.'
    return
}

if ($null -eq ($psISE.CurrentPowerShellTab.VerticalAddOnTools | Where-Object { $_.Name -eq $Name }))
{
    $psISE.CurrentPowerShellTab.VerticalAddOnTools.Add($Name, [ISEPresenter.Views.MainView], $true)
}
