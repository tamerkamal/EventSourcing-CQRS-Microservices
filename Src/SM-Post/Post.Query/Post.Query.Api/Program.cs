using CQRS.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Post.Common.DbContexts;
using Post.Common.Entities;
using Post.Query.Api.Queries;
using Post.Query.Api.Queries.Handler;
using Post.Query.Domain.Repositories;
using Post.Query.Infrastructure.Dispatchers;
using Post.Query.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container 
// Note: Order is important since some lower added services depend on upper ones.

#region Configs

#region MS SQL Database 

Action<DbContextOptionsBuilder> configureDbContext = (o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
//builder.Services.AddDbContext<DatabaseQueryContext>(configureDbContext);
builder.Services.AddDbContext<DatabaseContext>(configureDbContext);
//builder.Services.AddSingleton<DatabaseQueryContextFactory<BaseEntity>>(new DatabaseQueryContextFactory<BaseEntity>(configureDbContext));
builder.Services.AddSingleton<DatabaseContextFactory>(new DatabaseContextFactory(configureDbContext));

// Create database and tables from code (Code First)<>
//var databaseContext = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseQueryContext>();
var databaseContext = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
databaseContext.Database.EnsureCreated();

#endregion

#endregion

#region Scoped

//builder.Services.AddScoped(typeof(IBaseCmdRepository<>), typeof(BaseCmdRepository<>));
builder.Services.AddScoped<IPostQueryRepository, PostQueryRepository>();
builder.Services.AddScoped<ICommentQueryRepository, CommentQueryRepository>();
builder.Services.AddScoped<IQueryHandler, QueryHandler>();

#endregion

#region Query Handler methods

var queryHandler = builder.Services.BuildServiceProvider().GetRequiredService<IQueryHandler>();

QueryDispatcher queryDispatcher = new();

queryDispatcher.RegisterHandler<GetAllPostsQuery>(queryHandler.HandleAsync);
queryDispatcher.RegisterHandler<GetPostsByAuthorQuery>(queryHandler.HandleAsync);
queryDispatcher.RegisterHandler<GetPostsHavingCommentsQuery>(queryHandler.HandleAsync);
queryDispatcher.RegisterHandler<GetPostsHavingLikesQuery>(queryHandler.HandleAsync);
queryDispatcher.RegisterHandler<GetPostByIdQuery>(queryHandler.HandleAsync);

builder.Services.AddSingleton<IQueryDispatcher<PostEntity>>(_ => queryDispatcher);
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