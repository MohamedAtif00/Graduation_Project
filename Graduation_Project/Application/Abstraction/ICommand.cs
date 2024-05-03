using Ardalis.Result;
using MediatR;

namespace Graduation_Project.Application.Abstraction
{
    public interface ICommand<T> : IRequest<Result<T>>
    {
    }

    public interface ICommand : IRequest<Result>
    { }
}
