using Ardalis.Result;
using MediatR;

namespace Graduation_Project.Application.Abstraction
{
    public interface IQuery<T> : IRequest<Result<T>>
    {
    }
}
