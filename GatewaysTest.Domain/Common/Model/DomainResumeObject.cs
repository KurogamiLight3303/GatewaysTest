namespace GatewaysTest.Domain.Common.Model;

public abstract class DomainResumeObject<TDomainSource, TKey> where TDomainSource : DomainEntity<TKey>
{
    
}

public abstract class DomainResumeObject<TDomainSource> : DomainResumeObject<TDomainSource, Guid> 
    where TDomainSource : DomainEntity<Guid>
{
    
}