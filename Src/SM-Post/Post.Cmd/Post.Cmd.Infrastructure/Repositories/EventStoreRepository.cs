namespace Post.Cmd.Infrastructure.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Core.Domain;
using CQRS.Core.Events;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Post.Cmd.Infrastructure.Config;

public class EventStoreRepository : IEventStoreRepository
{
    private readonly IMongoCollection<EventStoreModel> _eventStorCollection;

    public EventStoreRepository(IOptions<MongoDbConfig> mongoDbConfig)
    {
        MongoClient mongoClient = new(mongoDbConfig.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(mongoDbConfig.Value.DatabaseName);

        _eventStorCollection = mongoDb.GetCollection<EventStoreModel>(mongoDbConfig.Value.CollectionName);
    }

    #region Public methods

    public async Task<List<BaseEvent>> FindEventsByAggregateIdAsync(Guid aggregateId)
    {
        return (await FindByAggregateIdAsync(aggregateId))?.Select(x => x.EventData).ToList();
    }

    public async Task SaveAsync(EventStoreModel eventStoreModel)
    {
        await _eventStorCollection.InsertOneAsync(@eventStoreModel).ConfigureAwait(false);
    }

    public async Task SaveAsync(IEnumerable<EventStoreModel> eventStoreModels)
    {
        await _eventStorCollection.InsertManyAsync(@eventStoreModels).ConfigureAwait(false);
    }

    #endregion

    #region Private methods

    private async Task<List<EventStoreModel>> FindByAggregateIdAsync(Guid aggregateId)
    {
        return await _eventStorCollection.Find(x => x.AggregateId == aggregateId).ToListAsync().ConfigureAwait(false);
    }

    #endregion
}
