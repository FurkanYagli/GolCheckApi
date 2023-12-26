using GolCheckApi.Models;
using GolCheckApi.Models.DbSettings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GolCheckApi.Services
{
    public class PrePlayerService
    {
        private readonly IMongoCollection<PrePlayer> _PlayerCollection;

    public PrePlayerService(
        IOptions<PrePlayerDatabaseSettings> prePlayerDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            prePlayerDatabaseSettings.Value.PreConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            prePlayerDatabaseSettings.Value.PreDatabaseName);

        _PlayerCollection = mongoDatabase.GetCollection<PrePlayer>(
            prePlayerDatabaseSettings.Value.PreCollectionName);
    }

    public async Task<List<PrePlayer>> GetAsync() =>
        await _PlayerCollection.Find(_ => true).ToListAsync();

    public async Task<PrePlayer?> GetAsync(string id) =>
        await _PlayerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(PrePlayer newPlayer) =>
        await _PlayerCollection.InsertOneAsync(newPlayer);

    public async Task UpdateAsync(string id, PrePlayer updatedPlayer) =>
        await _PlayerCollection.ReplaceOneAsync(x => x.Id == id, updatedPlayer);

    public async Task RemoveAsync(string id) =>
        await _PlayerCollection.DeleteOneAsync(x => x.Id == id);
    }
}
