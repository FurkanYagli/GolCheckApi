using GolCheckApi.App.FromApi;
using GolCheckApi.Models;
using GolCheckApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GolCheckApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class IspanyaLigController : ControllerBase
    {
        private readonly IspanyaService _LaligaService;



        public IspanyaLigController(IspanyaService IspanyaService)
        {
            _LaligaService = IspanyaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LigParametre>>> FrTakimTumunuGetir()
        {
            var teams = await _LaligaService.GetAsync();

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
            var Team = await _LaligaService.GetAsync(id);

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
        public async Task<IActionResult> TakimEkle(IspanyaLaliga newTeam)
        {
            await _LaligaService.CreateAsync(newTeam);

            return CreatedAtAction(nameof(FrTakimTumunuGetir), new { id = newTeam.Id }, newTeam);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> TakimGuncelle(string id, IspanyaLaliga updatedTeam)
        {
            var takim = await _LaligaService.GetAsync(id);

            if (takim is null)
            {
                return NotFound();
            }

            updatedTeam.Id = takim.Id;

            await _LaligaService.UpdateAsync(id, updatedTeam);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var frLigue = await _LaligaService.GetAsync(id);

            if (frLigue is null)
            {
                return NotFound();
            }

            await _LaligaService.RemoveAsync(id);

            return NoContent();
        }
    }
}

