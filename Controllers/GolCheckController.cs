using GolCheckApi.App.FromApi;
using GolCheckApi.Models;
using GolCheckApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GolCheckApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GolCheckController : ControllerBase
    {
        private readonly FrLigueService _FrService;



        public GolCheckController( FrLigueService frService)
        {
            _FrService = frService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LigParametre>>> FrTakimTumunuGetir()
        {
            var teams = await _FrService.GetAsync();

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
        public async Task<ActionResult<FrLigue>> SeciliGetir(string id)
        {
            var Team = await _FrService.GetAsync(id);

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
        [HttpPost]
        public async Task<IActionResult> TakimEkle(FrLigue newTeam)
        {
            await _FrService.CreateAsync(newTeam);

            return CreatedAtAction(nameof(FrTakimTumunuGetir), new { id = newTeam.Id }, newTeam);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> TakimGuncelle(string id, FrLigue updatedTeam)
        {
            var takim = await _FrService.GetAsync(id);

            if (takim is null)
            {
                return NotFound();
            }

            updatedTeam.Id = takim.Id;

            await _FrService.UpdateAsync(id, updatedTeam);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var frLigue = await _FrService.GetAsync(id);

            if (frLigue is null)
            {
                return NotFound();
            }

            await _FrService.RemoveAsync(id);

            return NoContent();
        }
    }
}
