$base = "src/services/accounting"
New-Item -Path "$base/1.core" -ItemType Directory -Force | Out-Null
New-Item -Path "$base/2.infra" -ItemType Directory -Force | Out-Null
New-Item -Path "$base/3.endpoints" -ItemType Directory -Force | Out-Null

dotnet new classlib -n "Accounting.Core.Domain"          -o "$base/1.core/domain"          --framework net10.0
dotnet new classlib -n "Accounting.Core.Application"     -o "$base/1.core/application"     --framework net10.0
dotnet new classlib -n "Accounting.Core.Contracts"       -o "$base/1.core/contracts"       --framework net10.0
dotnet new classlib -n "Accounting.Core.RequestResponse" -o "$base/1.core/request-response" --framework net10.0
dotnet new classlib -n "Accounting.Core.Resources"       -o "$base/1.core/resources"       --framework net10.0

dotnet new webapi   -n "Accounting.Endpoints.Api"        -o "$base/3.endpoints/api"        --framework net10.0