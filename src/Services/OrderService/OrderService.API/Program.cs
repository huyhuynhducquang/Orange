var builder = WebApplication.CreateBuilder(args);

builder.AddMicroserviceRegistration();

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddApplicationRegistration();
services.AddInfrastructureRegistration(builder.Configuration);

services.AddEventBusEventHandlerCollection();

var app = builder.Build();

app.Services.AddEventBusSubcribes();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
