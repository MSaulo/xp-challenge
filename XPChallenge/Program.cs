using Hangfire;
using Hangfire.Mongo;
using MongoDB.Driver;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using XPChallenge.Models;
using XPChallenge.Models.Settings;
using XPChallenge.Repositories;
using XPChallenge.Services;
using XPChallenge.Contracts;

var builder = WebApplication.CreateBuilder(args);

IConfiguration config = builder.Configuration;
builder.Services.Configure<MongoSettings>(config.GetSection("Mongo"));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<MongoService>();

builder.Services.AddScoped<IBaseRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<IBaseRepository<FinancialProduct>, FinancialProductRepository>();
builder.Services.AddScoped<IBaseRepository<Trade>, TradeRepository>();

builder.Services.AddScoped<TradeService>();
builder.Services.AddScoped<FinancialProductService>();

var mongoUrlBuilder = new MongoUrlBuilder("mongodb://localhost:27017/jobs");
var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());

builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseMongoStorage(mongoClient, mongoUrlBuilder.DatabaseName, new MongoStorageOptions {
        MigrationOptions = new MongoMigrationOptions {
            MigrationStrategy = new MigrateMongoMigrationStrategy(),
            BackupStrategy = new CollectionMongoBackupStrategy()
        },
        Prefix = "hangfire.mongo",
        CheckConnection = true,
        CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection
    })
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard();
app.UseHangfireServer();

RecurringJob.AddOrUpdate<FinancialProductService>(x => x.NotifyExpirationSoon(), Cron.Daily(10));

app.MapControllers();

app.Run();
