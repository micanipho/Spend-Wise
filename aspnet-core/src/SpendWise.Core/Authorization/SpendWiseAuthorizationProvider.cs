using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace SpendWise.Authorization;

public class SpendWiseAuthorizationProvider : AuthorizationProvider
{
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
        context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
        context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
        context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
        context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

        var expensesPermission = context.CreatePermission(PermissionNames.Pages_Expenses, L("Expenses"));
        expensesPermission.CreateChildPermission(PermissionNames.Pages_Expenses_Create, L("CreateExpense"));
        expensesPermission.CreateChildPermission(PermissionNames.Pages_Expenses_Edit, L("EditExpense"));
        expensesPermission.CreateChildPermission(PermissionNames.Pages_Expenses_Delete, L("DeleteExpense"));
    }

    private static ILocalizableString L(string name)
    {
        return new LocalizableString(name, SpendWiseConsts.LocalizationSourceName);
    }
}
