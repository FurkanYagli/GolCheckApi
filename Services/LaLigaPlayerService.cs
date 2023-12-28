using GolCheckApi.Models.DbSettings;
using GolCheckApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GolCheckApi.Services
{
    public class LaligaPlayerService
    {
        private readonly IMongoCollection<LaligaOyuncular> _Collection;

        public LaligaPlayerService(
            IOptions<LaligaPlDatabaseSettings> LaPlDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                LaPlDatabaseSettings.Value.LaPlConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                LaPlDatabaseSettings.Value.LaPlDatabaseName);

            _Collection = mongoDatabase.GetCollection<LaligaOyuncular>(
                LaPlDatabaseSettings.Value.LaPlCollectionName);
        }

        public async Task<List<LaligaOyuncular>> GetAsync() =>
            await _Collection.Find(_ => true).ToListAsync();

        public async Task<LaligaOyuncular?> GetAsync(string id) =>
            await _Collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(LaligaOyuncular newTeam) =>
            await _Collection.InsertOneAsync(newTeam);

        public async Task UpdateAsync(string id, LaligaOyuncular updatedTeam) =>
            await _Collection.ReplaceOneAsync(x => x.Id == id, updatedTeam);

        public async Task RemoveAsync(string id) =>
            await _Collection.DeleteOneAsync(x => x.Id == id);
    }
}

