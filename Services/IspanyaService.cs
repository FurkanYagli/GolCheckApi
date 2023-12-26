using GolCheckApi.Models.DbSettings;
using GolCheckApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GolCheckApi.Services
{
    public class IspanyaService
    {
        private readonly IMongoCollection<IspanyaLaliga> _Collection;

        public IspanyaService(
            IOptions<IspanyaDatabaseSettings> IspanyaDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                IspanyaDatabaseSettings.Value.IspanyaConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                IspanyaDatabaseSettings.Value.IspanyaDatabaseName);

            _Collection = mongoDatabase.GetCollection<IspanyaLaliga>(
                IspanyaDatabaseSettings.Value.IspanyaCollectionName);
        }

        public async Task<List<IspanyaLaliga>> GetAsync() =>
            await _Collection.Find(_ => true).ToListAsync();

        public async Task<IspanyaLaliga?> GetAsync(string id) =>
            await _Collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(IspanyaLaliga newTeam) =>
            await _Collection.InsertOneAsync(newTeam);

        public async Task UpdateAsync(string id, IspanyaLaliga updatedTeam) =>
            await _Collection.ReplaceOneAsync(x => x.Id == id, updatedTeam);

        public async Task RemoveAsync(string id) =>
            await _Collection.DeleteOneAsync(x => x.Id == id);
    }
}
