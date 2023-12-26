using MongoDB.Bson.Serialization.Attributes;

namespace GolCheckApi.App.FromApi
{
    public class OyuncuParametre
    {
        public int Rank { get; set; }

        public string Oyuncu { get; set; }

        public string Takim { get; set; }

        public int Gp { get; set; }

        public int Min { get; set; }

        public int G { get; set; }

        public int Asist { get; set; }

        public int Sut { get; set; }



    }
}
