using MongoDB.Driver;
using XPChallenge.Models;
using XPChallenge.Contracts;
using XPChallenge.Repositories;

namespace XPChallenge.Services {
    public class TradeService {
        private readonly TradeRepository _repository;

        public TradeService(IBaseRepository<Trade> repository) {
            _repository = (TradeRepository)repository;
        }

        public async Task<Trade?> GetAllTransactions(string customer) {
            return await _repository.GetByCustomerAsync(customer);
        }

        public async Task<Trade?> GetProductHistory(string product, string customer) {
            return await _repository.GetByCustomerAndFinancialProductAsync(product, customer);
        }

        public async Task<Trade?> Buy(string product, string customer, uint quantity) {
            if (quantity <= 0) {
                return null;
            }

            var trade = await _repository.GetByCustomerAndFinancialProductAsync(product, customer);
            var newTradeHistory = CreateTradeHistory(quantity, "BUY");

            if (trade == null) {
                List<TradeHistory> listTradeHistory = [];
                newTradeHistory.Balance = quantity;
                listTradeHistory.Add(newTradeHistory);

                Trade newTrade = new() {
                    ProductId = product,
                    CustomerId = customer,
                    Quantity = quantity,
                    History = listTradeHistory
                };

                return await _repository.CreateAsync(newTrade);
            } else {
                newTradeHistory.Balance = trade.Quantity += quantity;
                trade.History.Add(newTradeHistory);

                return await _repository.UpdateAsync(trade.Id, trade);
            }
        }

        public async Task<Trade?> Sell(string product, string customer, uint quantity) {
            var trade = await _repository.GetByCustomerAndFinancialProductAsync(product, customer);
            if (trade != null) {
                if (quantity > trade.Quantity) {
                    return null;
                }

                var newTradeHistory = CreateTradeHistory(quantity, "SELL");

                newTradeHistory.Balance = trade.Quantity -= quantity;
                trade.History.Add(newTradeHistory);

                return await _repository.UpdateAsync(trade.Id, trade);
            } else {
                return null;
            }
        }

        private static TradeHistory CreateTradeHistory(uint quantity, string type) => new() {
            Quantity = quantity,
            Date = DateTime.UtcNow,
            Type = type
        };
    }
}
