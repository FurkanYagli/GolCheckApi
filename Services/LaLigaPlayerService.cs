using GolCheckApi.Models.DbSettings;
using GolCheckApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GolCheckApi.Services
{
    public class LaLigaPlayerService
    {
        private readonly IMongoCollection<LaLigaPalyer> _PlayerCollection;

        public LaLigaPlayerService(
            IOptions<LaLigaPlayerDatabaseSettings> LaPlayerDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                LaPlayerDatabaseSettings.Value.LaPlConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                LaPlayerDatabaseSettings.Value.LaPlDatabaseName);

            _PlayerCollection = mongoDatabase.GetCollection<LaLigaPalyer>(
                LaPlayerDatabaseSettings.Value.LaPlCollectionName);
        }

        public async Task<List<LaLigaPalyer>> GetAsync() =>
            await _PlayerCollection.Find(_ => true).ToListAsync();

        public async Task<LaLigaPalyer?> GetAsync(string id) =>
            await _PlayerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(LaLigaPalyer newPlayer) =>
            await _PlayerCollection.InsertOneAsync(newPlayer);

        public async Task UpdateAsync(string id, LaLigaPalyer updatedPlayer) =>
            await _PlayerCollection.ReplaceOneAsync(x => x.Id == id, updatedPlayer);

        public async Task RemoveAsync(string id) =>
            await _PlayerCollection.DeleteOneAsync(x => x.Id == id);
    }
}

