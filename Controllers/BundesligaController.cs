using GolCheckApi.App.FromApi;
using GolCheckApi.Models;
using GolCheckApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using ZstdSharp.Unsafe;

namespace GolCheckApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BundesligaController : ControllerBase
    {
        private readonly BundesligaService _BundesligaService;
        //Repository Tasarım Deseni BundesligaService veritabanı
        //gibi bir veri kaynağıyla etkileşimde bulunmak için tasarlanmıştır. Bu, Repository Tasarım Deseni ile uyumludur


        //Dependency Injection - DI
        //bağımlılıkların (dependencies) sınıfların dışarıdan sağlanmasını ve
        //bu bağımlılıkların sınıf içine enjekte edilmesini sağlar. 
        //BundesligaController sınıfının constructor'ında BundesligaService bağımlılığı enjekte ediliyor.
        //Bu, ASP.NET Core'daki bir tasarım deseni olan bağımlılık enjeksiyonunu kullanmanın bir örneğidir.
        public BundesligaController(BundesligaService bundesligaService)
        {
            _BundesligaService = bundesligaService;
        }



        //DTO (Data Transfer Object) Tasarım Deseni:
        //LigParametre sınıfı, API tarafından döndürülen veriyi şekillendirmek için bir Veri Transfer Objesi olarak kullanılır.
        //Bu, API tarafından açığa çıkarılan verinin şeklini kontrol etmek için yaygın olarak kullanılan bir uygulamadır.       
        [HttpGet]
        public async Task<ActionResult<List<LigParametre>>> TakimTumunuGetir()
        {
            /*Null Object Tasarım Deseni:
            TakimTumunuGetir(Tüm Takımları Getir) metodu içinde, null veya boş liste kontrolü yapılır ve
            uygun durum koduyla yanıt verilir.
            Bu, takım bulunamadığında NotFound() yanıtının döndürülmesine yardımcı olur.*/
            var teams = await _BundesligaService.GetAsync();

            if (teams == null || teams.Count == 0)
            {
                return NotFound();
            }
            else
            {
                var ligParametreler = teams.Select(team => new LigParametre
                {
                    Ad = team.Name,
                    Stadyum = team.Stadium,
                    Kisaltma = team.Abbreviation,
                    Id = team.TeamId
                }).ToList();

                return Ok(ligParametreler);
            }
        }


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Bundesliga>> SeciliGetir(string id)
        {
            var Team = await _BundesligaService.GetAsync(id);

            if (Team is null)
            {
                return NotFound();
            }
            else
            {
                LigParametre ligParametre = new LigParametre();
                ligParametre.Ad = Team.Name;
                ligParametre.Stadyum = Team.Stadium;
                ligParametre.Kisaltma = Team.Abbreviation;
                ligParametre.Id = Team.TeamId;
                return Ok(ligParametre);
            }
        }


        //Post-Redirect-Get (PRG) Tasarım Deseni:
        //TakimEkle(Takım Ekle) metodu, CreatedAtAction kullanarak 201 Oluşturuldu durumunu
        //ve yeni oluşturulan kaynağın URL'sini döndürerek çalışır.
        //Bu, form gönderimlerinin tekrarlanmasını önlemeye yardımcı olan Post-Redirect-Get tasarım deseni ile uyumludur.
        //Bu desen, kullanıcıların tarayıcıda "Yeniden Yükle" yapmalarından kaynaklanan
        //tekrarlanan form gönderimleri ve ilgili sorunları önlemeye yardımcı olur.
        [HttpPost]
        public async Task<IActionResult> TakimEkle(Bundesliga newTeam)
        {
            await _BundesligaService.CreateAsync(newTeam);

            return CreatedAtAction(nameof(TakimTumunuGetir), new { id = newTeam.Id }, newTeam);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> TakimGuncelle(string id, Bundesliga updatedTeam)
        {
            var takim = await _BundesligaService.GetAsync(id);

            if (takim is null)
            {
                return NotFound();
                //ActionResult Tasarım Deseni:
                //MVC mimarisinde, bir kullanıcının bir web sayfasına veya bir servise
                //yaptığı istekler sonucunda uygulama tarafından döndürülen sonuçları ifade eder.
                //Kontrolcü metotları, HTTP yanıtlarını esnek ve tutarlı bir şekilde işlemek için ActionResult türlerini döndürür.
                //Örneğin, NotFound() 404 durum kodunu döndürmek için kullanılır.
            }

            updatedTeam.Id = takim.Id;

            await _BundesligaService.UpdateAsync(id, updatedTeam);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var frLigue = await _BundesligaService.GetAsync(id);

            if (frLigue is null)
            {
                return NotFound();
            }

            await _BundesligaService.RemoveAsync(id);

            return NoContent();
        }
    }
}
