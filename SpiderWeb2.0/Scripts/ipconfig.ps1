Param(
[string] $ComputerName
)

Function Get-IP {
    
    [Cmdletbinding()]
    Param (
        [alias('dnsHostName')]
        [Parameter(ValueFromPipelineByPropertyName=$true,ValueFromPipeline=$true)]
        [string]$ComputerName
    )
    Process {
        $NICs = Get-WmiObject Win32_NetworkAdapterConfiguration -Filter "IPEnabled='$True'" -ComputerName $ComputerName
        Foreach ($Nic in $NICs) {
            $myobj = @{
                Name          = $Nic.Description
                MacAddress    = $Nic.MACAddress
                IP4           = $Nic.IPAddress | where{$_ -match "\d+\.\d+\.\d+\.\d+"}
                IP6           = $Nic.IPAddress | where{$_ -match "\:\:"}
                IP4Subnet     = $Nic.IPSubnet  | where{$_ -match "\d+\.\d+\.\d+\.\d+"}
                DefaultGWY    = $Nic.DefaultIPGateway | Select -First 1
                DNSServer     = $Nic.DNSServerSearchOrder
            }
            $obj = New-Object PSObject -Property $myobj
            $obj.PSTypeNames.Clear()
            $obj
        }
    }
}

$linebreak = "======================================================"

$first = Get-IP -ComputerName $ComputerName | Format-Table Name,IP4 -AutoSize
$second = Get-IP -ComputerName $ComputerName | Format-Table IP4Subnet,DefaultGWY -AutoSize
$third = Get-IP -ComputerName $ComputerName | Format-Table MacAddress,DNSServer -AutoSize

return $first, $linebreak, $second, $linebreak, $third