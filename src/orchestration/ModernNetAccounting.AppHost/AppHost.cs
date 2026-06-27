using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// اضافه کردن پروژه API با نام درست
var apiService = builder.AddProject<Projects.Accounting_Endpoints_Api>("api-service");

// اگر می‌خواهید مطمئن شوید اسپایر از تنظیمات لانچ استفاده می‌کند، این متد استاندارد است:
apiService.WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development");

builder.Build().Run();