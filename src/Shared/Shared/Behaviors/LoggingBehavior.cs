
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Shared.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next
        , CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] ==> Handling {RequestType} -- Response={Response} -- RequestData={RequestData}"
            , typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timeStarted = new Stopwatch();

        timeStarted.Start();

        var response = next();

        timeStarted.Stop();

        var timeTaken = timeStarted.Elapsed;

        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning("[PERFORMANCE] ==> The Request {Request} tookk {TimeTaken} seconds to be handled"
                , typeof(TRequest).Name, timeTaken.TotalSeconds
               );
        } 

        logger.LogInformation("[END] ==> Handled {Request} with {Wesponse}"
            , typeof(TRequest).Name, typeof(TResponse).Name);

        return response;
    }
}
