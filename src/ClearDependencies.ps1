# This script will make sure that the Unity project (the renderer server) has the latest dlls from dependency projects
# Execute this from this folder in a Powershell window

$Path = (Get-Item -Path ".\").FullName

$UnityProjectPath = Join-Path $Path 'render-srv\src\RenderingServer'
$UnityProjectAssetsPath = Join-Path $UnityProjectPath 'Assets'
$UnityProjectLibPath = Join-Path $UnityProjectAssetsPath 'Libraries'

# Removing
Write-Host "Removing dlls from $UnityProjectLibPath..." -ForegroundColor Yellow
Remove-Item -Path "$UnityProjectLibPath\*.dll"

if ($LASTEXITCODE -eq 0)
{
    Write-Host "Done" -ForegroundColor Green
}
