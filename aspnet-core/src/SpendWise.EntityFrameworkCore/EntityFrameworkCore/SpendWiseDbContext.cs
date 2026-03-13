using Abp.Zero.EntityFrameworkCore;
using SpendWise.Authorization.Roles;
using SpendWise.Authorization.Users;
using SpendWise.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace SpendWise.EntityFrameworkCore;

public class SpendWiseDbContext : AbpZeroDbContext<Tenant, Role, User, SpendWiseDbContext>
{
    /* Define a DbSet for each entity of the application */

    public SpendWiseDbContext(DbContextOptions<SpendWiseDbContext> options)
        : base(options)
    {
    }
}
