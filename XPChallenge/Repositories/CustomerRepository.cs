using MongoDB.Driver;
using XPChallenge.Contracts;
using XPChallenge.Models;
using XPChallenge.Services;

namespace XPChallenge.Repositories {
    public class CustomerRepository : IBaseRepository<Customer> {
        private readonly string _scope = "customers";
        private readonly IMongoCollection<Customer> _collection;

        public CustomerRepository(MongoService mongo) {
            _collection = mongo.GetCollection<Customer>(_scope);
        }

        public async Task<Customer?> GetByIdAsync(string id) {
            var filter = Builders<Customer>.Filter.Eq(d => d.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync() {
            var filter = Builders<Customer>.Filter.Empty;
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<Customer> CreateAsync(Customer newDocument) {
            await _collection.InsertOneAsync(newDocument);
            return newDocument;
        }

        public async Task<Customer> UpdateAsync(string id, Customer document) {
            var filter = Builders<Customer>.Filter.Eq(d => d.Id, id);
            document.Id = id;
            await _collection.FindOneAndReplaceAsync(filter, document);
            return document;
        }

        public async Task<string?> DeleteAsync(string id) {
            var filter = Builders<Customer>.Filter.Eq(d => d.Id, id);
            var document = await _collection.FindOneAndDeleteAsync(filter);
            return document != null ? document.Id : null;
        }
    }
}