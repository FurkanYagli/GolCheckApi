using MongoDB.Bson.Serialization.Attributes;

namespace GolCheckApi.App.FromApi
{
    public class LigParametre
    {
        public int Id { get; set; }

        public string Ad { get; set; }

        public string Kisaltma { get; set; }

        public string Stadyum { get; set; }
    }
}
