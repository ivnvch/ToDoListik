using ToDoList.Persistence.DependencyInjection;
using ToDoList.Application.DependencyInjection;
using ToDoList.Domain.Settings;
using ToDoList.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.DefaultSection));

builder.Services.AddControllers();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddAuthenticationAndAuthrization(builder);
builder.Services.AddSwagger();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "PracticeProject v 1.0");
            c.SwaggerEndpoint("/swagger/v2/swagger.json", "PracticeProject v 2.0");
            //c.RoutePrefix = string.Empty;
        });
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
