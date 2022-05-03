using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GatewaysTest.Domain.Common.Model;
using GatewaysTest.Domain.Core.Common;
using GatewaysTest.Domain.Core.Features.Gateways.Commands;
using GatewaysTest.Domain.Core.Features.Gateways.Queries;
using GatewaysTest.Domain.Core.Features.Peripherals.Commands;
using GatewaysTest.Domain.Model.Gateways;
using GatewaysTest.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace GatewaysTest.UnitTests;

public class Tests
{
    private ServiceProvider _serviceProvider = null!;
    protected IMediator Mediator => _serviceProvider.CreateScope().ServiceProvider.GetService<IMediator>()!;
    [SetUp]
    public void Setup()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services
            .ConfigureMediator()
            .ConfigureAutoMapper()
            .ConfigureRepositories()
            .ConfigurePersistence(builder.Configuration)
            ;
        _serviceProvider = builder.Services.BuildServiceProvider(); 
    }

    protected Task<OperationResultValue<GatewayResume>> AddGateway(string serialNo, string name, string ipAddress)
    {
        return Mediator.Send(new CreateGatewayCommand()
        {
            SerialNo = serialNo,
            Name = name,
            IpAddress = ipAddress
        });
    }
    
    [TestCase("Gateway1", "192.168.10.1")]
    [TestCase("Gateway2", "192.168.10.255")]
    public async Task AddGateway(string name, string ipAddress)
    {
        var serialNo = Guid.NewGuid().ToString();
        var cResult = await AddGateway(serialNo, name, ipAddress);
        if(cResult.Data?.SerialNo == serialNo && cResult.Data.Name == name && ipAddress == cResult.Data.IpAddress)
            Assert.Pass();
        Assert.Fail("Invalid Response");
    }
    
    [TestCase("Gateway1", "192.168.10.256")]
    [TestCase("Gateway2", "192.168.10.255.255")]
    [TestCase("Gateway3", "192.168.10.FF")]
    public async Task InvalidGatewayIpAddress(string name, string ipAddress)
    {
        var serialNo = Guid.NewGuid().ToString();
        
        try
        {
            var cResult = await Mediator.Send(new CreateGatewayCommand()
            {
                SerialNo = serialNo,
                Name = name,
                IpAddress = ipAddress
            });
        }
        catch (ValidationException exc)
        {
            if(exc.Errors.Any(p => p.PropertyName == nameof(CreateGatewayCommand.IpAddress)))
                Assert.Pass();
        }
            
        Assert.Fail("Invalid Response");
    }

    [TestCase("Gateway1", "192.168.10.1", "Gateway1_v2", "192.168.10.200")]
    public async Task UpdateGateway(string name, string ipAddress, string name2, string ipAddress2)
    {
        var serialNo = Guid.NewGuid().ToString();
        var cResult = await AddGateway(serialNo, name, ipAddress);
        if (cResult.Success && cResult.Data?.SerialNo == serialNo)
        {
            var cResult2 = await Mediator.Send(new UpdateGatewayCommand()
            {
                SerialNo = serialNo,
                Name = name2,
                IpAddress = ipAddress2
            });
            if(cResult2.Data?.SerialNo == serialNo 
               && cResult2.Data.Name == name2 
               && ipAddress2 == cResult2.Data.IpAddress)
                Assert.Pass();
        }
        
        Assert.Fail("Invalid Response");
    }

    [TestCase("Gateway1", "192.168.10.1", "Vendor", "2021-02-14")]
    public async Task AddPeripheral(string name, string ipAddress, string vendor, string date)
    {
        var serialNo = Guid.NewGuid().ToString();
        var cResult = await AddGateway(serialNo, name, ipAddress);
        var uid = new Random().Next(0, int.MaxValue);
        if (cResult.Success)
        {
            var cResult2 = await Mediator.Send(new AddPeripheralCommand()
            {
                SerialNo = serialNo,
                Uid = uid,
                Status = true,
                Vendor = vendor,
                FabricationDate = date
            });
            if (cResult2.Success)
            {
                var rResult1 = await Mediator.Send(new GetGatewayQuery()
                {
                    SerialNo = serialNo
                });
                if(rResult1.Data!.SerialNo == serialNo && rResult1.Data.Peripherals!.Any(p
                       => p.Uid == uid && p.Vendor == vendor && p.FabricationDate != null
                                       && DateTime.Parse(p.FabricationDate) == DateTime.Parse(date)))
                    Assert.Pass();
                
            }
        }
        Assert.Fail("Invalid Response");
    }
}