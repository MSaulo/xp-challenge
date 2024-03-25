using MongoDB.Driver;
using XPChallenge.Contracts;
using XPChallenge.Models;
using XPChallenge.Services;

namespace XPChallenge.Repositories {
    public class FinancialProductRepository: IBaseRepository<FinancialProduct> {
        private readonly string _scope = "financial_products";
        private readonly IMongoCollection<FinancialProduct> _collection;

        public FinancialProductRepository(MongoService mongo) {
            _collection = mongo.GetCollection<FinancialProduct>(_scope);
        }

        public async Task<FinancialProduct?> GetByIdAsync(string id) {
            var filter = Builders<FinancialProduct>.Filter.Eq(d => d.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<FinancialProduct>> GetAllAsync() {
            var filter = Builders<FinancialProduct>.Filter.Empty;
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<FinancialProduct>> GetAllWillExpiresAsync() {
            var filter = Builders<FinancialProduct>.Filter.Lte(d => d.ExpiryDate, DateTime.UtcNow.AddDays(7));
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<FinancialProduct> CreateAsync(FinancialProduct newDocument) {
            await _collection.InsertOneAsync(newDocument);
            return newDocument;
        }

        public async Task<FinancialProduct> UpdateAsync(string id, FinancialProduct document) {
            var filter = Builders<FinancialProduct>.Filter.Eq(d => d.Id, id);
            document.Id = id;
            await _collection.FindOneAndReplaceAsync(filter, document);
            return document;
        }

        public async Task<string?> DeleteAsync(string id) {
            var filter = Builders<FinancialProduct>.Filter.Eq(d => d.Id, id);
            var document = await _collection.FindOneAndDeleteAsync(filter);
            return document != null ? document.Id : null;
        }
    }
}
