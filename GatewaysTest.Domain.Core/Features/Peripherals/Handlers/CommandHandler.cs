using System.Linq.Expressions;
using AutoMapper;
using GatewaysTest.Domain.Common.Language;
using GatewaysTest.Domain.Common.Model;
using GatewaysTest.Domain.Common.Repositories;
using GatewaysTest.Domain.Core.Common.Commands;
using GatewaysTest.Domain.Core.Features.Peripherals.Commands;
using GatewaysTest.Domain.Model.Gateways;
using GatewaysTest.Domain.Repositories;

namespace GatewaysTest.Domain.Core.Features.Peripherals.Handlers;

public class CommandHandler :
    ICommandHandler<AddPeripheralCommand, PeripheralResume>,
    ICommandHandler<RemovePeripheralCommand>
{
    private readonly IGatewayCommandRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CommandHandler(IGatewayCommandRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OperationResultValue<PeripheralResume>> Handle(AddPeripheralCommand request, CancellationToken cancellationToken)
    {
        GatewayObject? gateway;
        if ((gateway = await _repository.FindAsync(p => p.SerialNo == request.SerialNo,
                cancellationToken)) == null) return new(false, "Gateway Not found");
        PeripheralObject peripheral = new()
        {
            UID = request.Uid,
            Status = request.Status,
            FabricationDate = DateTime.Parse(request.FabricationDate!),
            Vendor = request.Vendor
        };
        gateway.Items!.Add(peripheral);
        _repository.Update(gateway);
        return await _unitOfWork.CommitAsync(cancellationToken)
            ? new(true, _mapper.Map<PeripheralResume>(peripheral))
            : new(false, I18n.UnknowError);
    }

    public async Task<OperationResultValue<PeripheralResume>> Handle(UpdatePeripheralCommand request, CancellationToken cancellationToken)
    {
        GatewayObject? gateway;
        PeripheralObject? peripheral;
        if ((gateway = await _repository.FindAsync(p => p.SerialNo == request.SerialNo,
                new Expression<Func<GatewayObject, object>>[]{ p => p.Items! },
                cancellationToken)) == null) return new(true, I18n.GatewayNotFound);
        if((peripheral = gateway.Items!.FirstOrDefault(p => p.UID == request.Uid)) == null)
            return new(false, I18n.PeripheralNotFound);
        if (!string.IsNullOrEmpty(request.Vendor))
            peripheral.Vendor = request.Vendor;
        if (!string.IsNullOrEmpty(request.FabricationDate))
            peripheral.FabricationDate = DateTime.Parse(request.FabricationDate);
        peripheral.Status = request.Status;
        _repository.Update(gateway);
        return await _unitOfWork.CommitAsync(cancellationToken)
            ? new(true, _mapper.Map<PeripheralResume>(peripheral))
            : new(false, I18n.UnknowError);
    }
    public async Task<OperationResultValue> Handle(RemovePeripheralCommand request, CancellationToken cancellationToken)
    {
        GatewayObject? gateway;
        PeripheralObject? peripheral;
        if ((gateway = await _repository.FindAsync(p => p.SerialNo == request.SerialNo,
                new Expression<Func<GatewayObject, object>>[]{ p => p.Items! },
                cancellationToken)) == null) return new(true, I18n.GatewayNotFound);
        if((peripheral = gateway.Items!.FirstOrDefault(p => p.UID == request.Uid)) == null)
            return new(false, I18n.PeripheralNotFound);
        gateway.Items!.Remove(peripheral);
        _repository.Update(gateway);
        return await _unitOfWork.CommitAsync(cancellationToken)
            ? new(true, I18n.PeripheralRemoved)
            : new(false, I18n.UnknowError);
    }
}