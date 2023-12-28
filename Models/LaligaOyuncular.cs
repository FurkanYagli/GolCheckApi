using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GolCheckApi.Models
{
    public class LaligaOyuncular
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("id")]
        public int OyuncuId { get; set; }

        [BsonElement("name")]
        public string Player { get; set; }

        [BsonElement("country")]
        public string Country { get; set; }

        [BsonElement("team")]
        public string Team { get; set; }

        [BsonElement("position")]
        public string Position { get; set; }

        [BsonElement("time_played")]
        public int TimePlayed { get; set; }
        
        [BsonElement("away_goals")]
        public int AwayGoals { get; set; }
    }
}
