using AutoMapper;
using GatewaysTest.Domain.Common.Language;
using GatewaysTest.Domain.Common.Model;
using GatewaysTest.Domain.Common.Repositories;
using GatewaysTest.Domain.Core.Common.Commands;
using GatewaysTest.Domain.Core.Features.Gateways.Commands;
using GatewaysTest.Domain.Model.Gateways;
using GatewaysTest.Domain.Repositories;

namespace GatewaysTest.Domain.Core.Features.Gateways.Handlers;

public class CommandHandler :
    ICommandHandler<CreateGatewayCommand, GatewayResume>,
    ICommandHandler<UpdateGatewayCommand, GatewayResume>,
    ICommandHandler<RemoveGatewayCommand>
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

    public async Task<OperationResultValue<GatewayResume>> Handle(CreateGatewayCommand request, CancellationToken cancellationToken)
    {
        GatewayObject gateway= new()
        {
            Name = request.Name!,
            SerialNo = request.SerialNo,
            IpAddress = new(request.IpAddress!)
        };
        await _repository.AddAsync(gateway, cancellationToken);
        return await _unitOfWork.CommitAsync(cancellationToken)
            ? new(true, _mapper.Map<GatewayResume>(gateway))
            : new(false, I18n.UnknowError);
    }

    public async Task<OperationResultValue<GatewayResume>> Handle(UpdateGatewayCommand request, CancellationToken cancellationToken)
    {
        GatewayObject? gateway;
        if ((gateway = await _repository.FindAsync(p => p.SerialNo == request.SerialNo,
                cancellationToken)) == null) return new(true, "Not found");
        if(!string.IsNullOrEmpty(request.Name))
            gateway.Name = request.Name;
        if(!string.IsNullOrEmpty(request.IpAddress))
            gateway.IpAddress = new(request.IpAddress);
        _repository.Update(gateway);
        return await _unitOfWork.CommitAsync(cancellationToken)
            ? new(true, _mapper.Map<GatewayResume>(gateway))
            : new(false, I18n.UnknowError);
    }

    public async Task<OperationResultValue> Handle(RemoveGatewayCommand request, CancellationToken cancellationToken)
    {
        GatewayObject? gateway;
        if ((gateway = await _repository.FindAsync(p => p.SerialNo == request.SerialNo,
                cancellationToken)) == null) return new(true, "Not found");
        _repository.Remove(gateway);
        return await _unitOfWork.CommitAsync(cancellationToken)
            ? new(true, I18n.GatewayRemoved)
            : new(false, I18n.UnknowError);
    }
}