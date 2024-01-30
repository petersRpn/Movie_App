using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Movies.API.HealthChecks;

public class MovieAppHealthCheck : IHealthCheck
{
    private readonly IConfiguration _config;

    public MovieAppHealthCheck(IConfiguration config)
    {
        _config = config;
    }
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            //TODO 
            //var ping = new Ping();
            //PingReply pingReply = ping.Send(_config["URLConfig:BaseUrl"]);

            //if(pingReply.Status == IPStatus.Success)
            //{
            //    return Task.FromResult(HealthCheckResult.Healthy("Healthy result from BiometricVericationHealthCheck"));
            //}

            //return Task.FromResult(HealthCheckResult.Unhealthy("Unhealthy result from BiometricVericationHealthCheck. Ping Status: " + pingReply.Status));
            return Task.FromResult(HealthCheckResult.Healthy("Healthy result from MovieAppHealthCheck"));

        }
        catch (Exception ex)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy("Unhealthy result from MovieAppHealthCheck", ex));
        }

    }
}