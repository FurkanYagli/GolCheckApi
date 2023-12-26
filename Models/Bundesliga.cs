using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GolCheckApi.Models
{
    public class Bundesliga
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("team_id")]
        public int TeamId { get; set; }

        [BsonElement("team_name")]
        public string Name { get; set; }

        [BsonElement("team_abbreviation")]
        public string Abbreviation { get; set; }

        [BsonElement("team_stadium")]
        public string Stadium { get; set; }
    }
}
