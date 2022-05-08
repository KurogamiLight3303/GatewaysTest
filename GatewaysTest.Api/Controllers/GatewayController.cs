using System.Net.Mime;
using GatewaysTest.Domain.Common.Model;
using GatewaysTest.Domain.Core.Features.Gateways.Commands;
using GatewaysTest.Domain.Core.Features.Gateways.Queries;
using GatewaysTest.Domain.Model.Gateways;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GatewaysTest.Api.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[Route("api/[controller]")]
public class GatewayController : ControllerBase
{
    private readonly IMediator _mediator;

    public GatewayController(IMediator mediator) => _mediator = mediator;
    /// <summary>
    /// Search Gateways
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("Search")]
    public Task<PagedResultValue<GatewayResume>> Search(SearchGatewaysQuery model)
        => _mediator.Send(model);
    /// <summary>
    /// Get Gateway Details
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    // ReSharper disable once RouteTemplates.RouteParameterIsNotPassedToMethod
    [HttpGet("{SerialNo}")]
    // ReSharper disable once RouteTemplates.MethodMissingRouteParameters
    public Task<OperationResultValue<GatewayDetailResume>> Get([FromRoute]GetGatewayQuery model)
        => _mediator.Send(model);
    /// <summary>
    /// Create Gateway
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    // ReSharper disable once RouteTemplates.RouteParameterIsNotPassedToMethod
    [HttpPost]
    // ReSharper disable once RouteTemplates.MethodMissingRouteParameters
    public Task<OperationResultValue<GatewayResume>> Post(CreateGatewayCommand model)
        => _mediator.Send(model);
    /// <summary>
    /// Update Gateway
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    // ReSharper disable once RouteTemplates.RouteParameterIsNotPassedToMethod
    [HttpPut("{SerialNo}")]
    // ReSharper disable once RouteTemplates.MethodMissingRouteParameters
    public Task<OperationResultValue<GatewayResume>> Put(UpdateGatewayCommand model)
        => _mediator.Send(model);
    /// <summary>
    /// Remove Gateway
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpDelete("{SerialNo}")]
    // ReSharper disable once RouteTemplates.MethodMissingRouteParameters
    public Task<OperationResultValue> Delete([FromRoute] RemoveGatewayCommand model)
        => _mediator.Send(model);
}