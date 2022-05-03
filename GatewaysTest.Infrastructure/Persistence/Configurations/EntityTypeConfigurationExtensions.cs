using System.Linq.Expressions;
using System.Text.Json;
using GatewaysTest.Domain.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GatewaysTest.Infrastructure.Persistence.Configurations;

public static class EntityTypeConfigurationExtensions
{
    internal static void ConfigureAggregateDomainEntity<TDomainEntity, TKey, TChildrenKey, TChildren>(
        this EntityTypeBuilder<TDomainEntity> builder,
        string? tableName = null
    )
        where TChildren : SecondaryDomainEntity<TChildrenKey, TKey>
        where TDomainEntity : AggregateDomainEntity<TKey, TChildrenKey, TChildren>
    {
        builder
            .HasMany(p => p.Items)
            .WithOne()
            .HasForeignKey(p => p.ParentId);
        ConfigureDomainEntity<TDomainEntity, TKey>(builder, tableName);
    }
    internal static void ConfigureDomainEntity<TDomainEntity, TKey>(
        this EntityTypeBuilder<TDomainEntity> builder,
        string? tableName = null
        )
        where TDomainEntity : DomainEntity<TKey>
    {
        if (!string.IsNullOrEmpty(tableName))
            builder.ToTable(tableName);
        builder.HasKey(p => p.Id);
        builder.Property(p => p.CreatedDate).IsRequired();
        builder.Property(p => p.UpdatedDate).IsRequired(false);

    }
    internal static ValueComparer<TResult> GetValueComparer<TResult>()
    {
        JsonSerializerOptions jOptions = new();
        return new(
            (o1, o2) 
                => JsonSerializer.Serialize(o1, jOptions) == JsonSerializer.Serialize(o2, jOptions),
            v 
                => v == null ? 0 : JsonSerializer.Serialize(v, jOptions).GetHashCode(),
            v 
                => JsonSerializer.Deserialize<TResult>(JsonSerializer.Serialize(v, jOptions), jOptions)!);
    }
    internal static ValueComparer<TResult> GetValueComparer<TResult, TProxy>(
        Expression<Func<TProxy, TResult>> fromExpression,
        Expression<Func<TResult, TProxy>> toExpression
        )
    {
        JsonSerializerOptions jOptions = new();
        return new(
            (o1, o2) 
                => JsonSerializer.Serialize(toExpression.Compile().Invoke(o1!), jOptions) 
                   == JsonSerializer.Serialize(toExpression.Compile().Invoke(o2!), jOptions),
            v 
                => v == null 
                    ? 0 
                    : JsonSerializer.Serialize(toExpression.Compile().Invoke(v), jOptions).GetHashCode(),
            v 
                => fromExpression.Compile().Invoke(JsonSerializer.Deserialize<TProxy>(
                    JsonSerializer.Serialize(toExpression.Compile().Invoke(v), jOptions), jOptions)!));
    }

    internal static ValueConverter<TResult, string> GetValueConverter<TResult>()
    {
        JsonSerializerOptions jOptions = new();
        return new(
            p =>
                JsonSerializer.Serialize(p, jOptions),
            p =>
                (!string.IsNullOrEmpty(p) 
                    ? JsonSerializer.Deserialize<TResult>(p, jOptions)
                    : default)!
        );
    }
    internal static ValueConverter<TResult, string> GetValueConverter<TResult, TProxy>(
        Expression<Func<TProxy, TResult>> fromExpression,
        Expression<Func<TResult, TProxy>> toExpression
        )
    {
        JsonSerializerOptions jOptions = new();
        return new(
            p =>
                JsonSerializer.Serialize(toExpression.Compile().Invoke(p), jOptions),
            p =>
                fromExpression.Compile().Invoke((!string.IsNullOrEmpty(p) 
                    ? JsonSerializer.Deserialize<TProxy>(p, jOptions)
                    : default)!)
        );
    }
    
    internal static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder)
    {
        propertyBuilder.HasConversion(GetValueConverter<T>(), GetValueComparer<T>());
        propertyBuilder.HasColumnType("nvarchar(max)");
        return propertyBuilder;
    }
    internal static PropertyBuilder<TResult> HasJsonConversion<TResult, TProxy>(
        this PropertyBuilder<TResult> propertyBuilder, 
        Expression<Func<TProxy, TResult>> fromExpression,
        Expression<Func<TResult, TProxy>> toExpression
        )
    {
        propertyBuilder.HasConversion(
            GetValueConverter(fromExpression, toExpression), 
            GetValueComparer(fromExpression, toExpression));
        propertyBuilder.HasColumnType("nvarchar(max)");
        return propertyBuilder;
    }
}