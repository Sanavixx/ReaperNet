Param(
[string] $ComputerName
)

Function Get-MappedPrinters {

    [Cmdletbinding()]
    Param(
        [alias('dnsHostName')]
        [Parameter(ValueFromPipelineByPropertyName=$true,ValueFromPipeline=$true)]
        [string]$ComputerName
    )

    $id = Get-WmiObject -Class win32_computersystem -ComputerName $ComputerName |
    Select-Object -ExpandProperty Username |
    ForEach-Object { ([System.Security.Principal.NTAccount]$_).Translate([System.Security.Principal.SecurityIdentifier]).Value }
    
    $path = "Registry::\HKEY_USERS\$id\Printers\Connections\"
    
    Invoke-Command -Computername $ComputerName -ScriptBlock {param($path)(Get-Childitem $path | Select PSChildName)} -ArgumentList $path | Select -Property * -ExcludeProperty PSComputerName, RunspaceId, PSShowComputerName
}

$result = Get-MappedPrinters -ComputerName $ComputerName | Format-Table * -AutoSize| Out-String
$result = $result -replace ",,msek-qs-001v,","" -replace "PSChildName", "Mapped Network Printers:"

$linebreak = "======================================================"

$result2 = Get-WmiObject Win32_Printer -ComputerName $ComputerName  | Format-table Name -AutoSize | Out-String
$result2 = $result2 -replace "Name","Other Printers:"
return $result, $linebreak, $result2