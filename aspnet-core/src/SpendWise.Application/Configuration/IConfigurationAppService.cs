using SpendWise.Configuration.Dto;
using System.Threading.Tasks;

namespace SpendWise.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
