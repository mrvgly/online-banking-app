using System;
using GetirCase.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GetirCase.Api.Common
{
    public class SeedData
    {

        public static void Seed(IServiceProvider serviceProvider)
        {
            EnsureSeedData(serviceProvider.GetRequiredService<ILogger<SeedData>>(),
                           serviceProvider.GetRequiredService<IConfiguration>(),
                           serviceProvider.GetRequiredService<GetirCaseDbContext>());
        }


        private static void EnsureSeedData(ILogger logger,
                                           IConfiguration config,
                                           GetirCaseDbContext getirCaseDbContext)
        {
            try
            {
                logger.LogInformation("Database migration is starting...");

                getirCaseDbContext.Database.Migrate();

                logger.LogInformation("Database migrations is done...");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}