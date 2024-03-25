using MongoDB.Driver;
using Microsoft.Extensions.Options;
using XPChallenge.Models.Settings;

namespace XPChallenge.Services {
    public sealed class MongoService {

        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly MongoSettings _config;

        public MongoService(IOptions<MongoSettings> options) {
            _config = options.Value;
            _client = new MongoClient(_config.ConnectionString);
            _database = _client.GetDatabase(_config.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name) {
            return _database.GetCollection<T>(name);
        }
    }
}
