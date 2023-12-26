using GolCheckApi.Models.DbSettings;
using GolCheckApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GolCheckApi.Services
{
    public class BundesligaService
    {
        private readonly IMongoCollection<Bundesliga> _Collection;

        public BundesligaService(
            IOptions<BundesligaDatabaseSettings> BundesligaDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                BundesligaDatabaseSettings.Value.BundesligaConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                BundesligaDatabaseSettings.Value.BundesligaDatabaseName);

            _Collection = mongoDatabase.GetCollection<Bundesliga>(
                BundesligaDatabaseSettings.Value.BundesligaCollectionName);
        }

        public async Task<List<Bundesliga>> GetAsync() =>
            await _Collection.Find(_ => true).ToListAsync();

        public async Task<Bundesliga?> GetAsync(string id) =>
            await _Collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Bundesliga newTeam) =>
            await _Collection.InsertOneAsync(newTeam);

        public async Task UpdateAsync(string id, Bundesliga updatedTeam) =>
            await _Collection.ReplaceOneAsync(x => x.Id == id, updatedTeam);

        public async Task RemoveAsync(string id) =>
            await _Collection.DeleteOneAsync(x => x.Id == id);
    }
}
