using GolCheckApi.Models;
using GolCheckApi.Models.DbSettings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GolCheckApi.Services
{
    public class FrLigueService
    {
        private readonly IMongoCollection<FrLigue> _Collection;

        public FrLigueService(
            IOptions<FrLigueDatabaseSettings> FrLigueDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                FrLigueDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                FrLigueDatabaseSettings.Value.DatabaseName);

            _Collection = mongoDatabase.GetCollection<FrLigue>(
                FrLigueDatabaseSettings.Value.CollectionName);
        }

        public async Task<List<FrLigue>> GetAsync() =>
            await _Collection.Find(_ => true).ToListAsync();

        public async Task<FrLigue?> GetAsync(string id) =>
            await _Collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(FrLigue newTeam) =>
            await _Collection.InsertOneAsync(newTeam);

        public async Task UpdateAsync(string id, FrLigue updatedTeam) =>
            await _Collection.ReplaceOneAsync(x => x.Id == id, updatedTeam);

        public async Task RemoveAsync(string id) =>
            await _Collection.DeleteOneAsync(x => x.Id == id);
    }
}
