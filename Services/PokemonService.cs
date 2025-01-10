using MongoDB.Driver;
using Microsoft.Extensions.Options;

public class PokemonService
{
    private readonly IMongoCollection<Pokemon> _pokemonCollection;

    public PokemonService(IOptions<MongoDbSettings> mongoDbSettings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _pokemonCollection = database.GetCollection<Pokemon>(mongoDbSettings.Value.PokemonCollectionName);
    }

    public async Task CreateAsync(Pokemon pokemon) =>
        await _pokemonCollection.InsertOneAsync(pokemon);

    public async Task<List<Pokemon>> GetAllAsync() =>
        await _pokemonCollection.Find(Builders<Pokemon>.Filter.Empty).ToListAsync();

    public async Task<Pokemon?> GetByIdAsync(int id) =>
        await _pokemonCollection.Find(p => p.Id == id).FirstOrDefaultAsync();

    public async Task UpdateAsync(int id, Pokemon updatedPokemon) =>
        await _pokemonCollection.ReplaceOneAsync(p => p.Id == id, updatedPokemon);

    public async Task DeleteAsync(int id) =>
        await _pokemonCollection.DeleteOneAsync(p => p.Id == id);
}
