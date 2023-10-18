using AdvertAPI.Services;
using Microsoft.Extensions.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdvertAPI.HealthChecks
{
    public class StorageHealthChecks : IHealthCheck
    {
        private readonly IAdvertStorageService advertStorageService;

        public StorageHealthChecks(IAdvertStorageService advertStorageService)
        {
            this.advertStorageService = advertStorageService;
        }
        public async ValueTask<IHealthCheckResult> CheckAsync(CancellationToken cancellationToken = default)
        {
            bool isStorageOk = await advertStorageService.CheckHealthAsync();
            return HealthCheckResult.FromStatus(isStorageOk ? CheckStatus.Healthy : CheckStatus.Unhealthy,string.Empty);
        }
    }
}
