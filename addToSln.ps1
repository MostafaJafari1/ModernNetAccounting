# رفتن به پوشه‌ای که اسکریپت توشه
Push-Location $PSScriptRoot

$solutionFile = Get-ChildItem -Filter "*.sln" | Select-Object -First 1
if ($null -eq $solutionFile) {
    Write-Error "فایل sln پیدا نشد"
    Pop-Location
    exit 1
}
Write-Host "Solution found: $($solutionFile.Name)"

$searchPaths = @(
    "src/services/accounting/1.core",
    "src/services/accounting/2.infra",
    "src/services/accounting/3.endpoints"
)

foreach ($searchPath in $searchPaths) {
    if (-not (Test-Path $searchPath)) {
        Write-Host "SKIP: path not found -> $searchPath" -ForegroundColor Yellow
        continue
    }

    $csprojFiles = Get-ChildItem -Path $searchPath -Filter "*.csproj" -Recurse

    foreach ($csproj in $csprojFiles) {
        $relativePath = $csproj.FullName.Substring((Get-Location).Path.Length + 1).Replace("\", "/")
        Write-Host "Adding: $relativePath"
        dotnet sln $solutionFile.Name add $relativePath
        if ($LASTEXITCODE -eq 0) {
            Write-Host "OK: $($csproj.Name) added successfully" -ForegroundColor Green
        } else {
            Write-Host "FAILED: $($csproj.Name)" -ForegroundColor Red
        }
    }
}

Write-Host "Done." -ForegroundColor Cyan
Pop-Location