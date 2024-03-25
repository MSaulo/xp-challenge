using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace XPChallenge.Models {
    public class FinancialProduct {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price {  get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
