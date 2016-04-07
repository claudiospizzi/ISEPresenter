break

# Define and return a variable
$x = 'This is a test!'
$x

# Execute a basic command
Get-CimInstance -ClassName Win32_Bios

# Longer pipeline 
Get-Service | Where-Object { $_.Status -eq 'Running' } |
    Format-Table Name, DisplayName, Status, StartType -AutoSize

# Combination of assignment, subexpressions and pipeline
$Processes = Get-Process -Name powershell_ise |
                 Select-Object @{
                    N = 'ProcessName'
                    E = { $_.ProcessName + '.exe' }
                 }

# Create a new simple funciton
function Test-Ast
{
    param
    (
        [Parameter(Mandatory=$true)]
        [String] $Name
    )

    write-Host "Hello, $Name!"
}

# Invoke the cretaed function
Test-Ast -Name 'Don'


<#

$Code = $psISE.CurrentPowerShellTab.Files.SelectedFile.Editor.Text

$Tokens = $null
$Errors = $null

$AST = [System.Management.Automation.Language.Parser]::ParseInput($Code, [ref]$Tokens, [ref]$Errors)

$Elements = $AST.FindAll({ $args[0] -is [System.Management.Automation.Language.NamedBlockAst] }, $false)

$Elements | Select-Object @{ N = 'Type'; E = { $_.GetType().Name } }, @{ N = 'Element'; E = { $_.ToString() } }

#>



#
#

#

#$test = $ast.FindAll({ $true }, $false)
#$test = $AST.FindAll({ $args[0] -is [System.Management.Automation.Language.NamedBlockAst]}, $false)

#AssignmentStatementAst
#PipelineAst