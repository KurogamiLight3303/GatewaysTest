using System.Net.Mime;
using GatewaysTest.Domain.Common.Model;
using GatewaysTest.Domain.Core.Features.Peripherals.Commands;
using GatewaysTest.Domain.Core.Features.Peripherals.Queries;
using GatewaysTest.Domain.Model.Gateways;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GatewaysTest.Api.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
// ReSharper disable once RouteTemplates.ControllerRouteParameterIsNotPassedToMethods
[Route("api/Gateway/{SerialNo}/[controller]")]
public class PeripheralController : ControllerBase
{
    private readonly IMediator _mediator;

    public PeripheralController(IMediator mediator)
        => _mediator = mediator;

    /// <summary>
    /// List Peripherals by Gateway Serial Number
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    // ReSharper disable once RouteTemplates.MethodMissingRouteParameters
    public Task<QueryResult<PeripheralResume>> List([FromRoute] ListPeripheralsQuery query)
        => _mediator.Send(query);

    /// <summary>
    /// Add Peripheral to Gateway
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    // ReSharper disable once RouteTemplates.MethodMissingRouteParameters
    public Task<OperationResultValue<PeripheralResume>> Post(AddPeripheralCommand command)
        => _mediator.Send(command);
    /// <summary>
    /// Update Peripheral in Gateway
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    // ReSharper disable once RouteTemplates.RouteParameterIsNotPassedToMethod
    [HttpPut("{Uid}")]
    // ReSharper disable once RouteTemplates.MethodMissingRouteParameters
    public Task<OperationResultValue<PeripheralResume>> Put(UpdatePeripheralCommand command)
        => _mediator.Send(command);
    /// <summary>
    /// Remove Peripheral from Gateway
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    // ReSharper disable once RouteTemplates.RouteParameterIsNotPassedToMethod
    [HttpDelete("{Uid}")]
    // ReSharper disable once RouteTemplates.MethodMissingRouteParameters
    public Task<OperationResultValue> Delete([FromRoute] RemovePeripheralCommand command)
        => _mediator.Send(command);
}