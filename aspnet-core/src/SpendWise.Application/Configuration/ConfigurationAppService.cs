using Abp.Authorization;
using Abp.Runtime.Session;
using SpendWise.Configuration.Dto;
using System.Threading.Tasks;

namespace SpendWise.Configuration;

[AbpAuthorize]
public class ConfigurationAppService : SpendWiseAppServiceBase, IConfigurationAppService
{
    public async Task ChangeUiTheme(ChangeUiThemeInput input)
    {
        await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}
