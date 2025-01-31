using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Application.Extensions;
using Serilog;
using Restaurants.Domain.Entities;
using Restaurants.API.Middlewares;
using Restaurants.API.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.AddPresentaion();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();






var app = builder.Build();


var scope = app.Services.CreateScope();

var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

await seeder.Seed();
app.UseMiddleware<ErrorHandlingMiddle>();
app.UseMiddleware<RequestLogInformation>();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
    app.UseSwagger();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.MapGroup("/api/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
