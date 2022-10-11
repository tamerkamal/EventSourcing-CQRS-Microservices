using Confluent.Kafka;
using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using Post.Cmd.Api.Commands;
using Post.Cmd.Api.Commands.Handler;
using Post.Cmd.Domain.Aggregates;
using Post.Cmd.Infrastructure.Config;
using Post.Cmd.Infrastructure.Dispatchers;
using Post.Cmd.Infrastructure.Handlers;
using Post.Cmd.Infrastructure.Producers;
using Post.Cmd.Infrastructure.Repositories;
using Post.Cmd.Infrastructure.Stores;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container 
// Note: Order is important since some lower added services depend on upper ones.

#region Configs

builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));

#endregion

#region Scoped

builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<PostAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<ICommandHandler, CommandHandler>();

#endregion

#region Register command handlers

var commandHandler = builder.Services.BuildServiceProvider().GetRequiredService<ICommandHandler>();

var commandDispatcher = new CommandDispatcher();
// ToDo: use and loop over =>: var commandTypes = Reflections.FindAllDerivedTypes<BaseCommand>();

commandDispatcher.RegiserHandler<AddPostCommand>(commandHandler.HandleAsync);
commandDispatcher.RegiserHandler<AddCommentCommand>(commandHandler.HandleAsync);
commandDispatcher.RegiserHandler<EditPostTextCommand>(commandHandler.HandleAsync);
commandDispatcher.RegiserHandler<EditCommentCommand>(commandHandler.HandleAsync);
commandDispatcher.RegiserHandler<RemovePostCommand>(commandHandler.HandleAsync);
commandDispatcher.RegiserHandler<RemoveCommentCommand>(commandHandler.HandleAsync);
commandDispatcher.RegiserHandler<LikePostCommand>(commandHandler.HandleAsync);

// Todo: Try => builder.Services.AddSingleton<ICommandDispatcher,CommandDispatcher>();
builder.Services.AddSingleton<ICommandDispatcher>(_ => commandDispatcher);

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

var app = builder.Build();

#region Configure the HTTP request pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion