using GatewaysTest.Domain.Common.Model;

namespace GatewaysTest.Domain.Core.Common.Queries;

public abstract class BasePagedQuery<TResult> : PagedRequestValue, IPagedQuery<TResult>
{
}