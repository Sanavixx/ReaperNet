Param(
[string] $ComputerName
)

Get-HotFix -ComputerName $ComputerName | Sort-Object InstalledOn| Format-Table Description, HotFixID, InstalledOn -AutoSize