﻿using GatewaysTest.Domain.Common.Model;
using MediatR;

namespace GatewaysTest.Domain.Core.Common.Commands;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, OperationResultValue>
    where TCommand : ICustomCommandBase<OperationResultValue>
{
    
}

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, OperationResultValue<TResult>>
    where TCommand : ICommandBase<TResult>
{
    
}

public interface ICustomCommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult> 
    where TCommand : ICustomCommandBase<TResult>
{
    
}