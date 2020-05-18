Param(
[string] $ComputerName
)

Get-WmiObject win32_process -ComputerName $ComputerName |% {$owners[$_.handle] = $_.getowner().user}
$ProcessALL = get-process -ComputerName $ComputerName| Select @{l="Owner";e={$owners[$_.id.tostring()]}},* | Out-GridView

return $ProcessALL