﻿using GatewaysTest.Domain.Common.Model;

namespace GatewaysTest.Domain.Core.Common.Commands;

public abstract class CommandBase : CustomCommandBase<OperationResultValue>
{
    
}

public abstract class CommandBase<TResult> : CustomCommandBase<OperationResultValue<TResult>>, ICommandBase<TResult>
{
}

public abstract class CustomCommandBase<TResult> : ICustomCommandBase<TResult>
{
    public readonly Guid CommandId;

    protected CustomCommandBase()
    {
        CommandId = Guid.NewGuid();
    }
}