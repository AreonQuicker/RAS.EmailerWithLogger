using E.S.Data.Query.Context.DI;
using E.S.Data.Query.DataAccess.Interfaces;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using RAS.Emailer;
using RAS.EmailerWithLogger.Extensions;
using RAS.EmailerWithLogger.HealthChecks;
using RAS.EmailerWithLogger.Interfaces;
using RAS.EmailerWithLogger.Services;

namespace RAS.EmailerWithLogger
{
    public static class Init
    {
        public static void AddEmailerWithLogger(this IServiceCollection services, EmailConfig config)
        {
            services.AddEmailer(config);
            services.AddDataQueryContext();
            services.AddTransient<IEmailerService, EmailerService>();
            services.AddTransient<IEmailLogService, EmailLogService>();
            services.AddTransient<IEmailerHealthCheckService, EmailerHealthCheckService>();
            services.AddTransient<ISendUnHealthyEmailFluentService, SendUnHealthyEmailFluentService>();
            
            services.AddHealthChecks()
                .AddCheck<EmailTodayHealthCheck>(
                    "Emails Today Health Check",
                    tags: new[] {"Email"});
            
            // services.AddHealthChecks()
            //     .AddCheck<EmailTodayLastHourHealthCheck>(
            //         "Emails Last Hour Health Check",
            //         tags: new[] {"Email"});
        }

        public static void AddEmailerWithLogger(this IApplicationBuilder app, IDataAccessQuery dataAccessQuery)
        {
            dataAccessQuery.CreateSchemaAndTableAsync()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            
            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapHealthChecks("/health", new HealthCheckOptions
                    {
                        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    });
                    endpoints.MapHealthChecks("/health/email", new HealthCheckOptions
                    {
                        Predicate = r => r.Tags.Contains("email"),
                        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    });
                });
        }
    }
}