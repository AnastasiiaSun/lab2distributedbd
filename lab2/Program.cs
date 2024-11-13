using lab2.DAL;
using lab2.DAL.Settings;
using lab2.Services;
using Lab2.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Додайте DbContext iз налаштуваннями рядка пiдключення
builder.Services.AddDbContext<Lab2DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHostedService<DatabaseSeeder>();

builder.Services.Configure<MongoDBSettings>(
builder.Configuration.GetSection("MongoDBSettings"));

builder.Services.AddSingleton<StudentService>();

// Реєстрацiя MongoDB-клiєнта
builder.Services.AddSingleton<IMongoClient>(s =>
{
    var settings = s.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<IMongoService, MongoService>();
builder.Services.AddHostedService<UpdateService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

