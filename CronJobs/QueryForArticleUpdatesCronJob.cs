﻿using DroneNews.CommandHandlers.Articles;
using Quartz;

namespace DroneNews.CronJobs;

[DisallowConcurrentExecution]
public class QueryForArticleUpdatesCronJob(ArticlesCommandHandler _handler) : IJob
{
    readonly ArticlesCommandHandler handler = _handler;
    public async Task Execute(IJobExecutionContext context)
    {
        await handler.Handle(new QueryForArticleUpdates());

    }
}
