using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SpendWise.EntityFrameworkCore;
using SpendWise.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace SpendWise.Web.Tests;

[DependsOn(
    typeof(SpendWiseWebMvcModule),
    typeof(AbpAspNetCoreTestBaseModule)
)]
public class SpendWiseWebTestModule : AbpModule
{
    public SpendWiseWebTestModule(SpendWiseEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
    }

    public override void PreInitialize()
    {
        Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(SpendWiseWebTestModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        IocManager.Resolve<ApplicationPartManager>()
            .AddApplicationPartsIfNotAddedBefore(typeof(SpendWiseWebMvcModule).Assembly);
    }
}