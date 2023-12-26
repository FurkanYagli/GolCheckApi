using GolCheckApi.Models.DbSettings;
using GolCheckApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GolCheckApi.Services
{
    public class PreLigService
    {
        private readonly IMongoCollection<PreLig> _PlayerCollection;

        public PreLigService(
            IOptions<PremierLigDatabaseSettings> PreLigDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                PreLigDatabaseSettings.Value.PreLigConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                PreLigDatabaseSettings.Value.PreLigDatabaseName);

            _PlayerCollection = mongoDatabase.GetCollection<PreLig>(
                PreLigDatabaseSettings.Value.PreLigCollectionName);
        }

        public async Task<List<PreLig>> GetAsync() =>
            await _PlayerCollection.Find(_ => true).ToListAsync();

        public async Task<PreLig?> GetAsync(string id) =>
            await _PlayerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(PreLig newPlayer) =>
            await _PlayerCollection.InsertOneAsync(newPlayer);

        public async Task UpdateAsync(string id, PreLig updatedPlayer) =>
            await _PlayerCollection.ReplaceOneAsync(x => x.Id == id, updatedPlayer);

        public async Task RemoveAsync(string id) =>
            await _PlayerCollection.DeleteOneAsync(x => x.Id == id);
    }
}
