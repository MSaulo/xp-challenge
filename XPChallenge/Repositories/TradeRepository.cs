using MongoDB.Driver;
using System.Collections.Generic;
using XPChallenge.Contracts;
using XPChallenge.Models;
using XPChallenge.Services;

namespace XPChallenge.Repositories {
    public class TradeRepository : IBaseRepository<Trade> {
        private readonly string _scope = "customers";
        private readonly IMongoCollection<Trade> _collection;

        public TradeRepository(MongoService mongo) {
            _collection = mongo.GetCollection<Trade>(_scope);
        }

        public async Task<Trade?> GetByCustomerAndFinancialProductAsync(string product, string customer) {
            var filter = Builders<Trade>.Filter.Eq(d => d.CustomerId, customer)
                & Builders<Trade>.Filter.Eq(d => d.ProductId, product);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Trade?> GetByCustomerAsync(string customer) {
            var filter = Builders<Trade>.Filter.Eq(d => d.CustomerId, customer);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Trade> CreateAsync(Trade newDocument) {
            await _collection.InsertOneAsync(newDocument);
            return newDocument;
        }

        public async Task<Trade> UpdateAsync(string id, Trade document) {
            var filter = Builders<Trade>.Filter.Eq(d => d.Id, id);
            document.Id = id;
            await _collection.FindOneAndReplaceAsync(filter, document);
            return document;
        }

        public Task<IEnumerable<Trade>> GetAllAsync() {
            throw new NotImplementedException();
        }

        public Task<Trade?> GetByIdAsync(string id) {
            throw new NotImplementedException();
        }

        public Task<string?> DeleteAsync(string id) {
            throw new NotImplementedException();
        }
    }
}