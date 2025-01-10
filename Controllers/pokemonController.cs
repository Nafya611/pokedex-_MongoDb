using Microsoft.AspNetCore.Mvc;
// using PokedexAPI.Models;


[ApiController]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly PokemonService _pokemonService;

    public PokemonController(PokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Pokemon>>> GetAll()
    {
        var pokemons = await _pokemonService.GetAllAsync();
        return Ok(pokemons);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Pokemon>> GetById(string id)
    {
        var pokemon = await _pokemonService.GetByIdAsync(id);
        if (pokemon == null)
        {
            return NotFound();
        }

        return Ok(pokemon);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Pokemon newPokemon)
    {
        await _pokemonService.CreateAsync(newPokemon);
        return CreatedAtAction(nameof(GetById), new { id = newPokemon.Id }, newPokemon);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Pokemon updatedPokemon)
    {
        var existingPokemon = await _pokemonService.GetByIdAsync(id);
        if (existingPokemon == null)
        {
            return NotFound();
        }

        updatedPokemon.Id = id;
        await _pokemonService.UpdateAsync(id, updatedPokemon);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var pokemon = await _pokemonService.GetByIdAsync(id);
        if (pokemon == null)
        {
            return NotFound();
        }

        await _pokemonService.DeleteAsync(id);
        return NoContent();
    }
}
