using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Kidoo.Learn;

public class LearnWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<LearnWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
