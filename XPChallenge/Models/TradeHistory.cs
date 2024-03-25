using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace XPChallenge.Models {
    public class TradeHistory {
        public string Type { get; set; }
        public uint Quantity { get; set; }
        public uint Balance { get; set; }
        public DateTime Date { get; set; }
    }
}
