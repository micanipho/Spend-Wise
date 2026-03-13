using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace SpendWise.EntityFrameworkCore;

public static class SpendWiseDbContextConfigurer
{
    public static void Configure(DbContextOptionsBuilder<SpendWiseDbContext> builder, string connectionString)
    {
        builder.UseSqlServer(connectionString);
    }

    public static void Configure(DbContextOptionsBuilder<SpendWiseDbContext> builder, DbConnection connection)
    {
        builder.UseSqlServer(connection);
    }
}
