using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Simpl;

namespace DroneNews.CronJobs;

public static class CronJobHelpers
{
    public static IServiceCollection AddCronJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            //q.UseMicrosoftDependencyInjectionJobFactory();

            var populateJobKey = new JobKey(nameof(PopulateArticlesCronJob));
            q.AddJob<PopulateArticlesCronJob>(populateJobKey)
            .AddTrigger(t => t
                .WithIdentity(populateJobKey.Name + "_trigger")
                .ForJob(populateJobKey)
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever())
                );


            var queryForUpdatesJobKey = new JobKey(nameof(QueryForArticleUpdatesCronJob));
            q
            .AddJob<QueryForArticleUpdatesCronJob>(queryForUpdatesJobKey)
            .AddTrigger(t => t
                .WithIdentity(queryForUpdatesJobKey.Name + "_trigger")
                .ForJob(queryForUpdatesJobKey)
                .StartAt(DateTime.UtcNow.AddMinutes(1))
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(90).RepeatForever())
                );

        });

        services.AddQuartzHostedService(opts => { opts.WaitForJobsToComplete = true; });

        return services;
    }
}
