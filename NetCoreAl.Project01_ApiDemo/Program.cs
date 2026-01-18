using NetCoreAl.Project01_ApiDemo.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApiContext>();

builder.Services.AddControllers();

// 🔴 Swagger servisleri
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// (İstersen kalabilir ama şart değil)
builder.Services.AddOpenApi();

var app = builder.Build();

// 🔴 Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapOpenApi(); // opsiyonel
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
