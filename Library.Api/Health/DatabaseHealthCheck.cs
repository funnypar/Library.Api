using DataAccess.DataContext;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Library.Api.Health
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly DataContext _context;
        private readonly ILogger<DatabaseHealthCheck> _logger;

        public DatabaseHealthCheck(ILogger<DatabaseHealthCheck> logger, DataContext dataContext)
        {
            _context = dataContext;
            _logger = logger;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                _ = await _context.Database.CanConnectAsync(cancellationToken).ConfigureAwait(false);
                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                _logger.LogError("Database is unhealty", ex); 
                return HealthCheckResult.Unhealthy("Database is unhealty", ex);
            }
        }
    }
}
