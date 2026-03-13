using Abp.Zero.EntityFrameworkCore;
using SpendWise.Authorization.Roles;
using SpendWise.Authorization.Users;
using SpendWise.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using SpendWise.Expenses;

namespace SpendWise.EntityFrameworkCore;

public class SpendWiseDbContext : AbpZeroDbContext<Tenant, Role, User, SpendWiseDbContext>
{
    /* Define a DbSet for each entity of the application */

    public DbSet<Expense> Expenses { get; set; }

    public SpendWiseDbContext(DbContextOptions<SpendWiseDbContext> options)
        : base(options)
    {
    }
}
