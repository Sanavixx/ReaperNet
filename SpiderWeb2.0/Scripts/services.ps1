Param(
[string] $ComputerName
)

Get-WmiObject Win32_Service -ComputerName $ComputerName |Select-Object *,@{Name="Owner";Expression={$_.StartName}} | Out-GridView