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

# Sleep
Start-Sleep -Seconds 1
