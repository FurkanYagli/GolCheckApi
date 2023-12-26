using GolCheckApi.App.FromApi;
using GolCheckApi.Models;
using GolCheckApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GolCheckApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrePlayerController : ControllerBase
    {
        public PrePlayerController(PrePlayerService prePlayerService)
        {
            _PrPlService = prePlayerService;
        }

        private readonly PrePlayerService _PrPlService;

        [HttpGet]
        public async Task<ActionResult<List<OyuncuParametre>>> EnOyuncuTumunuGetir()
        {
            var player = await _PrPlService.GetAsync();

            if (player == null || player.Count == 0)
            {
                return NotFound();
            }
            else
            {
                var OyuncuParametre = player.Select(oyuncu => new OyuncuParametre
                {
                    Rank = oyuncu.Rank,
                    Takim = oyuncu.Team,
                    Gp = oyuncu.Gp,
                    G = oyuncu.Goal,
                    Asist = oyuncu.Asist,
                    Sut = oyuncu.Shots,
                    Min = oyuncu.Min,
                    Oyuncu = oyuncu.Player,
                }).ToList();

                return Ok(OyuncuParametre);
            }
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<PrePlayer>> SeciliGetir(string id)
        {
            var Oyuncu = await _PrPlService.GetAsync(id);

            if (Oyuncu is null)
            {
                return NotFound();
            }
            else
            {
                OyuncuParametre oyuncuParametre = new OyuncuParametre();
                oyuncuParametre.Rank = Oyuncu.Rank;
                oyuncuParametre.Takim = Oyuncu.Team;
                oyuncuParametre.Gp = Oyuncu.Gp;
                oyuncuParametre.G = Oyuncu.Goal;
                oyuncuParametre.Asist = Oyuncu.Asist;
                oyuncuParametre.Sut = Oyuncu.Shots;
                oyuncuParametre.Min = Oyuncu.Min;
                oyuncuParametre.Oyuncu = Oyuncu.Player;
                return Ok(oyuncuParametre);
            }
        }

        [HttpPost]
        public async Task<IActionResult> OyuncuEkle(PrePlayer newPlayer)
        {
            await _PrPlService.CreateAsync(newPlayer);

            return CreatedAtAction(nameof(EnOyuncuTumunuGetir), new { id = newPlayer.Id }, newPlayer);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> OyunncuGuncelle(string id, PrePlayer updatedPlayer)
        {
            var player = await _PrPlService.GetAsync(id);

            if (player is null)
            {
                return NotFound();
            }

            updatedPlayer.Id = player.Id;

            await _PrPlService.UpdateAsync(id, updatedPlayer);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var prePlayer = await _PrPlService.GetAsync(id);

            if (prePlayer is null)
            {
                return NotFound();
            }

            await _PrPlService.RemoveAsync(id);

            return NoContent();
        }
    }
        
    }
