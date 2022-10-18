using Confluent.Kafka;
using CQRS.Core.Consumers;
using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using Microsoft.EntityFrameworkCore;
using Post.Cmd.Api.Commands;
using Post.Cmd.Api.Commands.Handler;
using Post.Cmd.Domain.Aggregates;
using Post.Cmd.Domain.Handlers;
using Post.Cmd.Domain.Repositories;
using Post.Cmd.Infrastructure.Config;
using Post.Cmd.Infrastructure.Consumers;
using Post.Cmd.Infrastructure.Dispatchers;
using Post.Cmd.Infrastructure.Handlers;
using Post.Cmd.Infrastructure.Handlers.EventHandlers;
using Post.Cmd.Infrastructure.Producers;
using Post.Cmd.Infrastructure.Repositories;
using Post.Cmd.Infrastructure.Repositories.EventStore;
using Post.Cmd.Infrastructure.Stores;
using Post.Common.DbContexts;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container 
// Note: Configs should come on top, for others Order is useful to clearly show the dependencies.

#region Configs

builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));
builder.Services.Configure<ConsumerConfig>(builder.Configuration.GetSection(nameof(ConsumerConfig)));

#region MS SQL Database 

Action<DbContextOptionsBuilder> configureDbContext = (o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
//builder.Services.AddDbContext<DatabaseCmdContext>(configureDbContext);
builder.Services.AddDbContext<DatabaseContext>(configureDbContext);
//builder.Services.AddSingleton<DatabaseCmdContextFactory<BaseEntity>>(new DatabaseCmdContextFactory<BaseEntity>(configureDbContext));
builder.Services.AddSingleton<DatabaseContextFactory>(new DatabaseContextFactory(configureDbContext));

// Create database and tables from code (Code First)<>
//var databaseContext = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseCmdContext>();
var databaseContext = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
databaseContext.Database.EnsureCreated();

#endregion

#endregion

#region Scoped

#region Repositories

builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
//builder.Services.AddScoped(typeof(IBaseCmdRepository<>), typeof(BaseCmdRepository<>));
builder.Services.AddScoped<IPostCmdRepository, PostCmdRepository>();
builder.Services.AddScoped<ICommentCmdRepository, CommentCmdRepository>();

#endregion

#region Event/EventSourcing Handlers

builder.Services.AddScoped<IPostEventHandler, PostEventHandler>();
builder.Services.AddScoped<ICommentEventHandler, CommentEventHandler>();
builder.Services.AddScoped<IEventSourcingHandler<PostAggregate>, EventSourcingHandler>();

#endregion

#region Event...

builder.Services.AddScoped<IEventConsumer, EventConsumer>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();

#endregion

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