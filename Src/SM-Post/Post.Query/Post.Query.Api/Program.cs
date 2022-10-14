using CQRS.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Post.Query.Infrastructure.DataAccess;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

#region MS SQL Database 

Action<DbContextOptionsBuilder> configureDbContext = (o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddDbContext<DatabaseQueryContext>(configureDbContext);
builder.Services.AddSingleton<DatabaseQueryContextFactory<BaseEntity>>(new DatabaseQueryContextFactory<BaseEntity>(configureDbContext));

// Create database and tables from code (Code First)<>
var databaseContext = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseQueryContext>();
databaseContext.Database.EnsureCreated();

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