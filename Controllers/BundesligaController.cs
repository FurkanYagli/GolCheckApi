using GolCheckApi.App.FromApi;
using GolCheckApi.Models;
using GolCheckApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GolCheckApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BundesligaController : ControllerBase
    {
        private readonly BundesligaService _BundesligaService;



        public BundesligaController(BundesligaService bundesligaService)
        {
            _BundesligaService = bundesligaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LigParametre>>> TakimTumunuGetir()
        {
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
