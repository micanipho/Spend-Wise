using Abp.Authorization;
using SpendWise.Authorization.Roles;
using SpendWise.Authorization.Users;

namespace SpendWise.Authorization;

public class PermissionChecker : PermissionChecker<Role, User>
{
    public PermissionChecker(UserManager userManager)
        : base(userManager)
    {
    }
}
