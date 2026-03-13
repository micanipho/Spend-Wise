using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SpendWise.Authorization;

namespace SpendWise;

[DependsOn(
    typeof(SpendWiseCoreModule),
    typeof(AbpAutoMapperModule))]
public class SpendWiseApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Authorization.Providers.Add<SpendWiseAuthorizationProvider>();
    }

    public override void Initialize()
    {
        var thisAssembly = typeof(SpendWiseApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            // Scan the assembly for classes which inherit from AutoMapper.Profile
            cfg => cfg.AddMaps(thisAssembly)
        );
    }
}
