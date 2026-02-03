clear
$currentPath = (Get-Location).Path
$outputDir = Split-Path -Path $currentPath -Parent

Write-Output "Create a new Solution with a MVC Application in the above folder(workspace, in case of orginal usasge) "
python -m cookiecutter  "$currentPath"  --output-dir  "$outputDir"

for ($i = 1; $i -le 10; $i++) {
    $dots = ".." * $i
    Write-Output $dots
    Start-Sleep -Milliseconds  15}

Write-Output "`nSucsesfull!"
Write-Host "`n`n`nPress any key to close."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")