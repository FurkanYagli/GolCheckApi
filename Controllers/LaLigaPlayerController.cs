using GolCheckApi.App.FromApi;
using GolCheckApi.Models;
using GolCheckApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GolCheckApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LaLigaPlayerController : ControllerBase
    {
        private readonly LaligaPlayerService _LaService;



        public LaLigaPlayerController(LaligaPlayerService laService)
        {
            _LaService = laService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LaLigaOyuncuParametre>>> LaTakimTumunuGetir()
        {
            var teams = await _LaService.GetAsync();

            if (teams == null || teams.Count == 0)
            {
                return NotFound();
            }
            else
            {
                var ligParametreler = teams.Select(team => new LaLigaOyuncuParametre
                {
                    OyuncuId = team.OyuncuId,
                    Oyuncu = team.Player,
                    Ulke = team.Country,
                    Takim = team.Team,
                    Pozisyon = team.Position,
                    OynamaZamanı = team.TimePlayed,
                    DeplasmanGolleri = team.AwayGoals,
            }).ToList();

                return Ok(ligParametreler);
            }
        }


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<LaligaOyuncular>> SeciliGetir(string id)
        {
            var Team = await _LaService.GetAsync(id);

            if (Team is null)
            {
                return NotFound();
            }
            else
            {
                LaLigaOyuncuParametre ligParametre = new LaLigaOyuncuParametre();
                ligParametre.OyuncuId = Team.OyuncuId;
                ligParametre.Oyuncu = Team.Player;
                ligParametre.Ulke = Team.Country;
                ligParametre.Takim = Team.Team;
                ligParametre.Pozisyon = Team.Position;
                ligParametre.OynamaZamanı = Team.TimePlayed;
                ligParametre.DeplasmanGolleri = Team.AwayGoals;
                return Ok(ligParametre);
            }
        }
        [HttpPost]
        public async Task<IActionResult> TakimEkle(LaligaOyuncular newPlayer)
        {
            await _LaService.CreateAsync(newPlayer);

            return CreatedAtAction(nameof(LaTakimTumunuGetir), new { id = newPlayer.Id }, newPlayer);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> TakimGuncelle(string id, LaligaOyuncular updatedPlayer)
        {
            var takim = await _LaService.GetAsync(id);

            if (takim is null)
            {
                return NotFound();
            }

            updatedPlayer.Id = takim.Id;

            await _LaService.UpdateAsync(id, updatedPlayer);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var frLigue = await _LaService.GetAsync(id);

            if (frLigue is null)
            {
                return NotFound();
            }

            await _LaService.RemoveAsync(id);

            return NoContent();
        }
    }
}
