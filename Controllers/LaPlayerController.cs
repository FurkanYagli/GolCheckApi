using GolCheckApi.App.FromApi;
using GolCheckApi.Models;
using GolCheckApi.Services;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;


namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LaPlayerController : ControllerBase
{
    private readonly LaLigaPlayerService _LaPlService;

    public LaPlayerController(LaLigaPlayerService laPlService) =>
        _LaPlService = laPlService;

    [HttpGet]
    public async Task<ActionResult<List<OyuncuParametre>>> Get()
    {

        var player = await _LaPlService.GetAsync();
        if (player is null)
        {
            return NotFound();
        }
        else
        {
            var OyuncuParametre = player.Select(oyuncu => new LaLigaOyuncuParametre
            {
                OyuncuId = oyuncu.PlayerId,
                Oyuncu = oyuncu.Player,
                Ulke = oyuncu.Country,
                Takim = oyuncu.Team,
                Pozisyon = oyuncu.Position,
            }).ToList();

            return Ok(OyuncuParametre);
        }

    }

   /* [HttpGet]
    public async Task<List<LaLigaPalyer>> Getir() =>
       await _LaPlService.GetAsync();*/


    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<LaLigaPalyer>> Get(string id)
    {
        var player = await _LaPlService.GetAsync(id);

        if (player is null)
        {
            return NotFound();
        }

        return player;
    }

    [HttpPost]
    public async Task<IActionResult> Post(LaLigaPalyer newPlayer)
    {
        await _LaPlService.CreateAsync(newPlayer);

        return CreatedAtAction(nameof(Get), new { id = newPlayer.Id }, newPlayer);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, LaLigaPalyer updatedPlayer)
    {
        var book = await _LaPlService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedPlayer.Id = book.Id;

        await _LaPlService.UpdateAsync(id, updatedPlayer);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _LaPlService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _LaPlService.RemoveAsync(id);

        return NoContent();
    }
}