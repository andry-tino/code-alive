# This script will make sure that the Unity project (the renderer server) has the latest dlls from dependency projects
# Execute this from this folder in a Powershell window

$Path = (Get-Item -Path ".\").FullName

$UnityProjectPath = Join-Path $Path 'render-srv\src\RenderingServer'
$UnityProjectAssetsPath = Join-Path $UnityProjectPath 'Assets'
$UnityProjectLibPath = Join-Path $UnityProjectAssetsPath 'Libraries'

$CommunicationEndpointProjectBinPath = Join-Path $Path 'render-srv\src\CommunicationEndpoint\bin\Debug'

Write-Host "Copying dlls from $CommunicationEndpointProjectBinPath to $UnityProjectLibPath..." -ForegroundColor Yellow

Copy-Item -Path "$CommunicationEndpointProjectBinPath\*.dll" -Destination $UnityProjectLibPath

if ($LASTEXITCODE -eq 0)
{
    Write-Host "Done" -ForegroundColor Green
}
