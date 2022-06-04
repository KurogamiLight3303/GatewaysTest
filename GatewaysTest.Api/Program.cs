using GatewaysTest.Domain.Core.Common;
using GatewaysTest.Domain.Core.Common.CustomBinder;
using GatewaysTest.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
// ReSharper disable AccessToModifiedClosure

var builder = WebApplication.CreateBuilder(args);

WebApplication? app = null;
// Add services to the container.

builder
    .Services
    .AddControllers(options =>
    {
        options.Filters.Add<HttpResponseExceptionFilter>();
        IHttpRequestStreamReaderFactory? readerFactory;
        if (app == null || (readerFactory = app.Services.GetService<IHttpRequestStreamReaderFactory>()) == null) 
            throw new($"Unable to Bind {nameof(CustomModelBinderProvider)}");
        options.ModelBinderProviders.Insert(0, new CustomModelBinderProvider(options.InputFormatters, readerFactory));
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = GatewaysTest.Domain.Core.Common.Configuration.InvalidModelHandling;
    });

builder.Services
    .ConfigureMediator()
    .ConfigureAutoMapper()
    .ConfigureRepositories()
    .ConfigurePersistence(builder.Configuration)
    ;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}/GatewaysTest.Api.xml");
});

// Add AWS Lambda support.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(options =>
        options
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(_ => true) // allow any origin
            .AllowCredentials()
    );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();