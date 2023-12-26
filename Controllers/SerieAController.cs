using GolCheckApi.App.FromApi;
using GolCheckApi.Models;
using GolCheckApi.Services;
using Microsoft.AspNetCore.Mvc;



namespace GolCheckApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SerieAController : ControllerBase
    {
        private readonly SerieAService _serieAService;

        public SerieAController(SerieAService serieAService)
        {
            _serieAService = serieAService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LigParametre>>> Get()
        {
            var teams = await _serieAService.GetAsync();

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
        public async Task<ActionResult<SerieA>> SeciliGetir(string id)
        {
            var Team = await _serieAService.GetAsync(id);

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
        public async Task<IActionResult> TakimEkle(SerieA newTeam)
        {
            await _serieAService.CreateAsync(newTeam);

            return CreatedAtAction(nameof(Get), new { id = newTeam.Id }, newTeam);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> TakimGuncelle(string id, SerieA updatedTeam)
        {
            var takim = await _serieAService.GetAsync(id);

            if (takim is null)
            {
                return NotFound();
            }

            updatedTeam.Id = takim.Id;

            await _serieAService.UpdateAsync(id, updatedTeam);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var serieA = await _serieAService.GetAsync(id);

            if (serieA is null)
            {
                return NotFound();
            }

            await _serieAService.RemoveAsync(id);

            return NoContent();
        }
    }
}
