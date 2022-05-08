using GatewaysTest.Domain.Core.Common;
using GatewaysTest.Domain.Core.Common.CustomBinder;
using GatewaysTest.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
    .Services
    .AddControllers(options =>
    {
        options.Filters.Add<HttpResponseExceptionFilter>();
        IHttpRequestStreamReaderFactory readerFactory = 
            builder.Services.BuildServiceProvider().GetRequiredService<IHttpRequestStreamReaderFactory>();
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

var app = builder.Build();

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