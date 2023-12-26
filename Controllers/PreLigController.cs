using GolCheckApi.App.FromApi;
using GolCheckApi.Models;
using GolCheckApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GolCheckApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreLigController : ControllerBase
    {
        private readonly PreLigService _PreService;



        public PreLigController(PreLigService preService)
        {
            _PreService = preService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LigParametre>>> FrTakimTumunuGetir()
        {
            var teams = await _PreService.GetAsync();

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
        public async Task<ActionResult<PreLig>> SeciliGetir(string id)
        {
            var Team = await _PreService.GetAsync(id);

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
        public async Task<IActionResult> TakimEkle(PreLig newTeam)
        {
            await _PreService.CreateAsync(newTeam);

            return CreatedAtAction(nameof(FrTakimTumunuGetir), new { id = newTeam.Id }, newTeam);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> TakimGuncelle(string id, PreLig updatedTeam)
        {
            var takim = await _PreService.GetAsync(id);

            if (takim is null)
            {
                return NotFound();
            }

            updatedTeam.Id = takim.Id;

            await _PreService.UpdateAsync(id, updatedTeam);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var preLigue = await _PreService.GetAsync(id);

            if (preLigue is null)
            {
                return NotFound();
            }

            await _PreService.RemoveAsync(id);

            return NoContent();
        }
    }
}
