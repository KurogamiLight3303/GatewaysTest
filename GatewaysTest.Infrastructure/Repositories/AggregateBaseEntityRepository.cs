using AutoMapper;
using GatewaysTest.Domain.Common.Model;
using GatewaysTest.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GatewaysTest.Infrastructure.Repositories;

public class AggregateBaseEntityRepository<TDomainEntity, TChildDomainEntity, TKey, TChildKey> 
    : BaseEntityRepository<TDomainEntity, TKey> 
    where TDomainEntity : AggregateDomainEntity<TKey, TChildKey, TChildDomainEntity>
    where TChildDomainEntity : SecondaryDomainEntity<TChildKey, TKey>
{
    protected readonly DbSet<TChildDomainEntity> ChildDataSet;
    protected AggregateBaseEntityRepository(
        DomainContext context, 
        IMapper mapper, 
        IQueryFilterTranslator<TDomainEntity, TKey>? filterTranslator = null) 
        : base(context, mapper, filterTranslator)
    {
        ChildDataSet = context.Set<TChildDomainEntity>();
    }

    public override void Update(TDomainEntity entity)
    {
        TrackChildren(entity);
        base.Update(entity);
    }

    private void TrackChildren(TDomainEntity entity)
    {
        if (entity.Items == null) return;
        foreach (var item in entity.Items)
        {
            item.CreatedDate ??= DateTime.Now;
            if(Context.Entry(item).State == EntityState.Modified || item.UpdatedDate == null)
                item.UpdatedDate = DateTime.Now;
        }
    }
}