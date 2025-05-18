using BlockchainNet;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI
builder.Services.AddCustomService();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
// }
    app.UseSwagger();
    app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.UseAuthorization();

// Aktifkan CORS
app.UseCors("AllowAnyOrigin");

app.MapControllers();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello, world!");
    });
    endpoints.MapGet("/cicd", async context =>
    {
        await context.Response.WriteAsync("test ci cd!");
    });
});

app.Run();