using System;
using System.Reflection;

using JobBoardApp_DataAcces.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobBoardApp_DataAcces
{
    public static class RepositoryExtension
    {
        public static void SetDefaultModelProperty<T>(string field, string defaultValue, ref ModelBuilder model) where T : class
        {
            model.Entity<T>()
                .Property(field)
                .HasDefaultValueSql(defaultValue);
        }

        public static void SetIdentityModelProperty<T>(string field, ref ModelBuilder model) where T : class
        {
            model.Entity<T>()
               .Property(field)
               .UseSqlServerIdentityColumn()
               .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(JobBoardAppContext).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<JobBoardAppContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(migrationsAssembly);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });
                options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            });
        }
    }
}
