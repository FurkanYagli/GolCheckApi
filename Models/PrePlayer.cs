using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GolCheckApi.Models
{
    public class PrePlayer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Rank")]
        public int Rank { get; set; }

        [BsonElement("PLAYER")]
        public string Player { get; set; }

        [BsonElement("TEAM")]
        public string Team { get; set; }

        [BsonElement("GP")]
        public int Gp { get; set; }

        [BsonElement("MIN")]
        public int Min { get; set; }

        [BsonElement("G")]
        public int Goal { get; set; }

        [BsonElement("ASST")]
        public int Asist { get; set; }

        [BsonElement("SHOTS")]
        public int Shots { get; set; }
    }
}
