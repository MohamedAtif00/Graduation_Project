using Ardalis.Result;
using MediatR;

namespace Graduation_Project.Application.Abstraction
{
    public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
        where TQuery : IQuery<TResult>
        where TResult : notnull
    {
    }
}
