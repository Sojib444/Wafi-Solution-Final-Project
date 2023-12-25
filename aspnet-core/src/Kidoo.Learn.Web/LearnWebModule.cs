using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Kidoo.Learn.EntityFrameworkCore;
using Kidoo.Learn.Localization;
using Kidoo.Learn.MultiTenancy;
using Kidoo.Learn.Web.Menus;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System;
using Volo.Abp.OpenIddict;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using Volo.Abp.Users;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Azure;
using Kidoo.Learn.Files;

namespace Kidoo.Learn.Web;

[DependsOn(
    typeof(LearnHttpApiModule),
    typeof(LearnApplicationModule),
    typeof(LearnEntityFrameworkCoreModule),
    typeof(AbpAutofacModule),
    typeof(AbpIdentityWebModule),
    typeof(AbpSettingManagementWebModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(AbpTenantManagementWebModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpBlobStoringModule),
    typeof(AbpBlobStoringAzureModule)
    )]
public class LearnWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(LearnResource),
                typeof(LearnDomainModule).Assembly,
                typeof(LearnDomainSharedModule).Assembly,
                typeof(LearnApplicationModule).Assembly,
                typeof(LearnApplicationContractsModule).Assembly,
                typeof(LearnWebModule).Assembly
            );
        });

        PreConfigure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
        });

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("Learn");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });

        var hostingEnvironment = context.Services.GetHostingEnvironment();

        if (!hostingEnvironment.IsDevelopment())
        {
            PreConfigure<AbpOpenIddictAspNetCoreOptions>(options =>
            {
                options.AddDevelopmentEncryptionAndSigningCertificate = false;
            });
            PreConfigure<OpenIddictServerBuilder>(builder =>
            {
                // In production, it is recommended to use two RSA certificates, 
                // one for encryption, one for signing.
                builder.AddEncryptionCertificate(
                        GetEncryptionCertificate(hostingEnvironment, context.Services.GetConfiguration()));
                builder.AddSigningCertificate(
                        GetSigningCertificate(hostingEnvironment, context.Services.GetConfiguration()));
            });
        }
        ConfigureRateLimiters(context); //Rate limit
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        ConfigureAuthentication(context);
        ConfigureUrls(configuration);
        ConfigureBundles();
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureNavigationServices();
        ConfigureAutoApiControllers();
        ConfigureSwaggerServices(context.Services);
        ConfigureBlobService(configuration);
    }

    public void ConfigureBlobService(IConfiguration configuration)
    {

        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.Configure<KidooFileContainer>(container =>
            {
                container.UseAzure(azure =>
                {
                    azure.ConnectionString = configuration["Blob:ConnectionString"];
                    azure.ContainerName = configuration["Blob:ContainerName"];
                    azure.CreateContainerIfNotExists = true;

                });
            });
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["Kidoo"].RootUrl = configuration["App:SelfUrl"];
        });
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                LeptonXLiteThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );
        });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<LearnWebModule>();
        });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<LearnDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Kidoo.Learn.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<LearnDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Kidoo.Learn.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<LearnApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Kidoo.Learn.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<LearnApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Kidoo.Learn.Application"));
                options.FileSets.ReplaceEmbeddedByPhysical<LearnWebModule>(hostingEnvironment.ContentRootPath);
            });
        }
    }

    private void ConfigureNavigationServices()
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new LearnMenuContributor());
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(LearnApplicationModule).Assembly);
        });
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Learn API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Learn API");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }

    private void ConfigureRateLimiters(ServiceConfigurationContext context)
    {
        context.Services.AddRateLimiter(limiterOptions =>
        {
            limiterOptions.OnRejected = (context, cancellationToken) =>
            {
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    context.HttpContext.Response.Headers.RetryAfter =
                        ((int)retryAfter.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
                }

                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.HttpContext.RequestServices.GetService<ILoggerFactory>()?
                    .CreateLogger("Microsoft.AspNetCore.RateLimitingMiddleware")
                    .LogWarning("OnRejected: {RequestPath}", context.HttpContext.Request.Path);

                return new ValueTask();
            };

            limiterOptions.AddPolicy("UserBasedRateLimiting", context =>
            {
                var currentUser = context.RequestServices.GetService<ICurrentUser>();

                if (currentUser is not null && currentUser.IsAuthenticated)
                {
                    return RateLimitPartition.GetTokenBucketLimiter(currentUser.UserName, _ => new TokenBucketRateLimiterOptions
                    {
                        TokenLimit = 10,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 3,
                        ReplenishmentPeriod = TimeSpan.FromMinutes(1),
                        TokensPerPeriod = 4,
                        AutoReplenishment = true
                    });
                }

                return RateLimitPartition.GetSlidingWindowLimiter("anonymous-user",
                    _ => new SlidingWindowRateLimiterOptions
                    {
                        PermitLimit = 2,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 1,
                        Window = TimeSpan.FromMinutes(1),
                        SegmentsPerWindow = 2
                    });
            });

            limiterOptions.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
            {
                var currentTenant = context.RequestServices.GetService<ICurrentTenant>();

                if (currentTenant is not null && currentTenant.IsAvailable)
                {
                    return RateLimitPartition.GetConcurrencyLimiter(currentTenant!.Name, _ => new ConcurrencyLimiterOptions
                    {
                        PermitLimit = 5,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 1
                    });
                }

                return RateLimitPartition.GetNoLimiter("host");
            });
        });
    }

    private X509Certificate2 GetSigningCertificate(IWebHostEnvironment hostingEnv,
                          IConfiguration configuration)
    {
        var fileName = $"cert-signing.pfx";
        var file = Path.Combine(hostingEnv.ContentRootPath, fileName);
        if (File.Exists(file))
        {
            var created = File.GetCreationTime(file);
            var days = (DateTime.Now - created).TotalDays;
            if (days > 180)
                File.Delete(file);
            else
                return new X509Certificate2(file, "",
                             X509KeyStorageFlags.MachineKeySet);
        }

        // file doesn't exist or was deleted because it expired
        using var algorithm = RSA.Create(keySizeInBits: 2048);
        var subject = new X500DistinguishedName("CN=Kidoo Signing Certificate");
        var request = new CertificateRequest(subject, algorithm,
                            HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        request.CertificateExtensions.Add(new X509KeyUsageExtension(
                            X509KeyUsageFlags.DigitalSignature, critical: true));
        var certificate = request.CreateSelfSigned(DateTimeOffset.UtcNow,
                            DateTimeOffset.UtcNow.AddYears(2));
        File.WriteAllBytes(file, certificate.Export(X509ContentType.Pfx, string.Empty));
        return new X509Certificate2(file, "",
                            X509KeyStorageFlags.MachineKeySet);
    }

    private X509Certificate2 GetEncryptionCertificate(IWebHostEnvironment hostingEnv,
                                 IConfiguration configuration)
    {
        var fileName = $"cert-encryption.pfx";
        var file = Path.Combine(hostingEnv.ContentRootPath, fileName);
        if (File.Exists(file))
        {
            var created = File.GetCreationTime(file);
            var days = (DateTime.Now - created).TotalDays;
            if (days > 180)
                File.Delete(file);
            else
                return new X509Certificate2(file, "",
                                X509KeyStorageFlags.MachineKeySet);
        }

        // file doesn't exist or was deleted because it expired
        using var algorithm = RSA.Create(keySizeInBits: 2048);
        var subject = new X500DistinguishedName("CN=Kidoo Encryption Certificate");
        var request = new CertificateRequest(subject, algorithm,
                            HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        request.CertificateExtensions.Add(new X509KeyUsageExtension(
                            X509KeyUsageFlags.KeyEncipherment, critical: true));
        var certificate = request.CreateSelfSigned(DateTimeOffset.UtcNow,
                            DateTimeOffset.UtcNow.AddYears(2));
        File.WriteAllBytes(file, certificate.Export(X509ContentType.Pfx, string.Empty));
        return new X509Certificate2(file, "", X509KeyStorageFlags.MachineKeySet);
    }
}
