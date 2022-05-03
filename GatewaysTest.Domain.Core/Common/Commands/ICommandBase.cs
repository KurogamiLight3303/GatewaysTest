using GatewaysTest.Domain.Common.Model;
using MediatR;

namespace GatewaysTest.Domain.Core.Common.Commands;

public interface ICommandBase<TResult> : ICustomCommandBase<OperationResultValue<TResult>>
{
}

public interface ICustomCommandBase<out TCustom> : IRequest<TCustom>
{
}