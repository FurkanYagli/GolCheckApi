using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GolCheckApi.Models
{
    public class LaLigaPalyer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("id")]
        public int PlayerId { get; set; }

        [BsonElement("name")]
        public string Player { get; set; }

        [BsonElement("country")]
        public string Country { get; set; }

        [BsonElement("team")]
        public string Team { get; set; }

        [BsonElement("position")]
        public string Position { get; set; }
    }
}
