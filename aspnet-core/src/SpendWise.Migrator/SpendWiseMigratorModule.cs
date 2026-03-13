using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SpendWise.Configuration;
using SpendWise.EntityFrameworkCore;
using SpendWise.Migrator.DependencyInjection;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;

namespace SpendWise.Migrator;

[DependsOn(typeof(SpendWiseEntityFrameworkModule))]
public class SpendWiseMigratorModule : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration;

    public SpendWiseMigratorModule(SpendWiseEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

        _appConfiguration = AppConfigurations.Get(
            typeof(SpendWiseMigratorModule).GetAssembly().GetDirectoryPathOrNull()
        );
    }

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            SpendWiseConsts.ConnectionStringName
        );

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        Configuration.ReplaceService(
            typeof(IEventBus),
            () => IocManager.IocContainer.Register(
                Component.For<IEventBus>().Instance(NullEventBus.Instance)
            )
        );
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(SpendWiseMigratorModule).GetAssembly());
        ServiceCollectionRegistrar.Register(IocManager);
    }
}
