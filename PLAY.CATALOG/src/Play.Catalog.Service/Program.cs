using Play.Catalog.Service.Entities;
using Play.Common.MongoDb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//ServiceSettings serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();


builder.Services.AddControllers();

// builder.Services.AddSingleton(serviceProvider =>
// {
//     var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
//     var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
//     return mongoClient.GetDatabase(serviceSettings.ServiceName);
// });

// builder.Services.AddSingleton<IRepository<Item>>(serviceProvider => {
//     var database = serviceProvider.GetService<IMongoDatabase>();
//     return new MongoRepository<Item>(database, "items");
// });

builder.Services.AddMongo()
    .AddMongoRepository<Item>("items");

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
