using GolCheckApi.Models.DbSettings;
using GolCheckApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GolCheckApi.Services
{
    public class SerieAService
    {
        private readonly IMongoCollection<SerieA> _Collection;

        public SerieAService(
            IOptions<SerieADatabaseSettings> SerieADatabaseSettings)
        {
            var mongoClient = new MongoClient(
                SerieADatabaseSettings.Value.SerieAConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                SerieADatabaseSettings.Value.SerieADatabaseName);

            _Collection = mongoDatabase.GetCollection<SerieA>(
                SerieADatabaseSettings.Value.SerieACollectionName);
        }

        public async Task<List<SerieA>> GetAsync() =>
            await _Collection.Find(_ => true).ToListAsync();

        public async Task<SerieA?> GetAsync(string id) =>
            await _Collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(SerieA newTeam) =>
            await _Collection.InsertOneAsync(newTeam);

        public async Task UpdateAsync(string id, SerieA updatedTeam) =>
            await _Collection.ReplaceOneAsync(x => x.Id == id, updatedTeam);

        public async Task RemoveAsync(string id) =>
            await _Collection.DeleteOneAsync(x => x.Id == id);
    }
}
