#--------------------------------------------
# Get System Type Information
#--------------------------------------------

Param(
[string] $ComputerName
)

Function Get-SystemType {

    [Cmdletbinding()]
    Param (
        [alias('dnsHostName')]
        [Parameter(ValueFromPipelineByPropertyName=$true,ValueFromPipeline=$true)]
        [string]$ComputerName = $Env:COMPUTERNAME
    )
    
    Begin {
    
        Function ConvertTo-ChassisType($Type) {
            Switch ($Type) {
                1    {"Other"}
                2    {"Unknown"}
                3    {"Desktop"}
                4    {"Low Profile Desktop"}
                5    {"Pizza Box"}
                6    {"Mini Tower"}
                7    {"Tower"}
                8    {"Portable"}
                9    {"Laptop"}
                10    {"Notebook"}
                11    {"Hand Held"}
                12    {"Docking Station"}
                13    {"All in One"}
                14    {"Sub Notebook"}
                15    {"Space-Saving"}
                16    {"Lunch Box"}
                17    {"Main System Chassis"}
                18    {"Expansion Chassis"}
                19    {"SubChassis"}
                20    {"Bus Expansion Chassis"}
                21    {"Peripheral Chassis"}
                22    {"Storage Chassis"}
                23    {"Rack Mount Chassis"}
                24    {"Sealed-Case PC"}
            }
        }
        Function ConvertTo-SecurityStatus($Status) {
            Switch ($Status) {
                1    {"Other"}
                2    {"Unknown"}
                3    {"None"}
                4    {"External Interface Locked Out"}
                5    {"External Interface Enabled"}
            }
        }
    
    }

    Process {
        If ($ComputerName -match "(.*)(\$)$") {
            $ComputerName = $ComputerName -replace "(.*)(\$)$",'$1'
        }
        Try {
            $SystemInfo = Get-WmiObject Win32_SystemEnclosure -ComputerName $ComputerName
            $CSInfo = Get-WmiObject -Query "Select Model FROM Win32_ComputerSystem" -ComputerName $ComputerName
            $myobj = @{}
            $myobj.ComputerName = $ComputerName
            $myobj.Manufacturer = $SystemInfo.Manufacturer
            $myobj.Model = $CSInfo.Model
            $myobj.SerialNumber = $SystemInfo.SerialNumber
            $myobj.SecurityStatus = ConvertTo-SecurityStatus $SystemInfo.SecurityStatus
            $myobj.Type = ConvertTo-ChassisType $SystemInfo.ChassisTypes
            $obj = New-Object PSCustomObject -Property $myobj
            $obj.PSTypeNames.Clear()
            $obj
        }
        Catch {
        }
    }
}

Get-SystemType -ComputerName $ComputerName