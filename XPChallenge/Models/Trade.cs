using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace XPChallenge.Models {
    public class Trade {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public uint Quantity { get; set; }
        [JsonIgnore]
        public List<TradeHistory>? History { get; set; }
    }
}
