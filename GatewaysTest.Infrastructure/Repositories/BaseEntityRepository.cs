using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using GatewaysTest.Domain.Common.Model;
using GatewaysTest.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GatewaysTest.Infrastructure.Repositories;

public abstract class BaseEntityRepository<TDomainEntity, TKey> where TDomainEntity : PrimaryDomainEntity<TKey>
{
    private readonly IMapper _mapper;
    protected readonly DbSet<TDomainEntity> DataSet;
    protected readonly DomainContext Context;

    protected BaseEntityRepository(
        DomainContext context, 
        IMapper mapper 
        )
    {
        _mapper = mapper;
        DataSet = context.Set<TDomainEntity>();
        Context = context;
    }

    #region "CRUD"
    public virtual async Task AddAsync(TDomainEntity entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedDate = entity.CreatedDate = DateTime.Now;
        await DataSet.AddAsync(entity, cancellationToken);
    }
    public virtual void Update(TDomainEntity entity)
    {
        entity.UpdatedDate = DateTime.Now;
        DataSet.Update(entity);
    }
    public virtual bool Update(TDomainEntity entity, Dictionary<Expression<Func<TDomainEntity, object>>, object> changes)
    {
        var result = false;
        foreach (var (key, value) in changes)
        {
            if (value is IComparable c && c.Equals(key.Compile().Invoke(entity))) continue;
            var memberExpression = key.Body as MemberExpression;
            var property = memberExpression?.Member as PropertyInfo;
            property?.SetValue(entity, value);
            result = true;
        }
        if(result)
            Update(entity);
        return result;
    }
    public void Remove(TDomainEntity entity)
    {
        DataSet.Remove(entity);
    }
    #endregion
    
    #region "Search"
    public async Task<PagedResultValue<TDomainEntity>> SearchAsync(PagedRequestValue parameters,
        CancellationToken cancellationToken = default)
    {
        var sourceQuery = await BaseSearch(parameters, cancellationToken);
        var items = await sourceQuery
            .Skip(parameters.PageSize * parameters.PageNo)
            .Take(parameters.PageSize)
            .ToArrayAsync(cancellationToken);
        var count = await sourceQuery.LongCountAsync(cancellationToken);
        return new(items, parameters, count);
    }
    public async Task<PagedResultValue<TProjectedValue>> SearchAsync<TProjectedValue>(PagedRequestValue parameters,
        CancellationToken cancellationToken = default) where TProjectedValue : DomainResumeObject<TDomainEntity, TKey>
    {
        var sourceQuery = await BaseSearch(parameters, cancellationToken);
        var items = await _mapper.ProjectTo<TProjectedValue>(sourceQuery
            .Skip(parameters.PageSize * parameters.PageNo)
            .Take(parameters.PageSize)
        ).ToArrayAsync(cancellationToken);
        var count = await sourceQuery.LongCountAsync(cancellationToken);
        return new(items, parameters, count);
    }
    private async Task<IQueryable<TDomainEntity>> BaseSearch(PagedRequestValue parameters,
        CancellationToken cancellationToken = default)
    {
        var query = DataSet
                .AsNoTracking()
            ;

        // if(parameters.Filters != null && FilterTranslator != null)
        //     foreach (var filter in parameters.Filters)
        //     {
        //         var expression = FilterTranslator.GetFilter(filter.Alias, filter.Value, filter.Type);
        //         if (expression != null)
        //             query = query.Where(expression);
        //     }
        
        return query;
    }
    #endregion

    #region "Find"
    public async Task<TDomainEntity?> FindAsync(Expression<Func<TDomainEntity, bool>> condition, 
        CancellationToken cancellationToken = default)
    {
        return await DataSet.FirstOrDefaultAsync(condition, cancellationToken);
    }
    public async Task<TDomainEntity?> FindAsync(Expression<Func<TDomainEntity, bool>> condition, 
        IEnumerable<Expression<Func<TDomainEntity, object>>> includes,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TDomainEntity> source = DataSet;
        foreach (var i in includes)
            source = source.Include(i);
        return await source.FirstOrDefaultAsync(condition, cancellationToken);
    }
    public async Task<TDomainEntity?> FindAsync<TProperty>(Expression<Func<TDomainEntity, bool>> condition, 
        Expression<Func<TDomainEntity, TProperty>> orderBy, CancellationToken cancellationToken = default)
    {
        return await DataSet.OrderBy(orderBy).FirstOrDefaultAsync(condition, cancellationToken);
    }
    public async Task<TDomainEntity?> FindLastAsync(Expression<Func<TDomainEntity, bool>> condition, 
        CancellationToken cancellationToken = default)
    {
        return await DataSet.LastOrDefaultAsync(condition, cancellationToken);
    }
    public async Task<TDomainEntity?> FindLastAsync<TProperty>(Expression<Func<TDomainEntity, bool>> condition, 
        Expression<Func<TDomainEntity, TProperty>> orderBy, CancellationToken cancellationToken = default)
    {
        return await DataSet.OrderBy(orderBy).LastOrDefaultAsync(condition, cancellationToken);
    }
    #endregion

    public async Task<bool> AnyAsync(Expression<Func<TDomainEntity, bool>> condition,
        CancellationToken cancellationToken = default)
    {
        return await DataSet.AnyAsync(condition, cancellationToken);
    }

    public async Task<long> CountAsync(Expression<Func<TDomainEntity, bool>> condition,
        CancellationToken cancellationToken = default)
    {
        return await DataSet.LongCountAsync(condition, cancellationToken);
    }
}