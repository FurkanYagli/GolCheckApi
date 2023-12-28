namespace GolCheckApi.App.FromApi
{
    public class LaLigaOyuncuParametre
    {
        public string Oyuncu { get; set; }

        public string Takim { get; set; }

        public string Pozisyon { get; set; }

        public string Ulke { get; set; }

        public int OyuncuId { get; set; }

        public int OynamaZamanı { get; set; }
        
        public int DeplasmanGolleri { get; set; }
    }
}
