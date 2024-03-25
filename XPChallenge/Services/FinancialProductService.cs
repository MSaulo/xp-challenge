using System.Text;
using XPChallenge.Contracts;
using XPChallenge.Models;
using XPChallenge.Repositories;

namespace XPChallenge.Services {
    public class FinancialProductService {
        private readonly FinancialProductRepository _repository;

        public FinancialProductService(IBaseRepository<FinancialProduct> repository) {
            _repository = (FinancialProductRepository)repository;
        }

        public async Task<IEnumerable<FinancialProduct>> GetWillExpiresFinancialProducts() {
            return await _repository.GetAllWillExpiresAsync();
        }

        public async void NotifyExpirationSoon() {
            var expiringProducts = await GetWillExpiresFinancialProducts();
            if (expiringProducts.Any()) {
                var emailBody = GenerateEmailBody(expiringProducts);
                Console.WriteLine(emailBody);
                // await _emailSender.SendEmail("xxx@xxx.xx", "Subject", emailBody);
            }
        }
        private static string GenerateEmailBody(IEnumerable<FinancialProduct> products) {
            var sb = new StringBuilder();

            sb.AppendLine("The following financial products are expiring soon:");
            foreach (var product in products) {
                sb.AppendLine($"- {product.Name} (Expires in: {product.ExpiryDate:dd/MM/yyyy})");
            }

            return sb.ToString();
        }
    }
}
