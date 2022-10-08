using CQRS.Core.Domain;
using Post.Cmd.Infrastructure.Config;
using Post.Cmd.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Configs

builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));

#endregion

#region Scoped

builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();

#endregion

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
