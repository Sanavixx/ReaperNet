Param(
[string] $ComputerName
)

Function Get-InstalledSoftware {

    [Cmdletbinding()]
    Param(
        [alias('dnsHostName')]
        [Parameter(ValueFromPipelineByPropertyName=$true,ValueFromPipeline=$true)]
        [string]$ComputerName
    )

    Invoke-Command -ComputerName $ComputerName -ScriptBlock {Get-ItemProperty HKLM:\Software\Microsoft\Windows\CurrentVersion\Uninstall\* | Select DisplayName, Publisher, InstallDate, Version, UninstallString } | Select -Property * -ExcludeProperty PSComputerName, RunspaceId, PSShowComputerName | Sort-Object DisplayName | Out-GridView
}

Get-InstalledSoftware -ComputerName $ComputerName | Out-GridView