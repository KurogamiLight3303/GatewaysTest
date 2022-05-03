using GatewaysTest.Domain.Core.Common;
using GatewaysTest.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
    .Services
    .AddControllers(options => { options.Filters.Add<HttpResponseExceptionFilter>(); })
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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();