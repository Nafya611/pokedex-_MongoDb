using MongoDB.Driver;
using Microsoft.Extensions.Options;

public class PokemonService
{
    private readonly IMongoCollection<Pokemon> _pokemonCollection;

    public PokemonService(IOptions<MongoDbSettings> MongoDbSettings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(MongoDbSettings.Value.DatabaseName);
        _pokemonCollection = database.GetCollection<Pokemon>(MongoDbSettings.Value.PokemonCollectionName);
    }

    // Create
    public async Task CreateAsync(Pokemon pokemon) =>
        await _pokemonCollection.InsertOneAsync(pokemon);

    // Read All
    public async Task<List<Pokemon>> GetAllAsync() =>
        await _pokemonCollection.Find(Builders<Pokemon>.Filter.Empty).ToListAsync();

    // Read by ID
    public async Task<Pokemon?> GetByIdAsync(string id) =>
        await _pokemonCollection.Find(p => p.Id == id).FirstOrDefaultAsync();

    // Update
    public async Task UpdateAsync(string id, Pokemon updatedPokemon) =>
        await _pokemonCollection.ReplaceOneAsync(p => p.Id == id, updatedPokemon);

    // Delete
    public async Task DeleteAsync(string id) =>
        await _pokemonCollection.DeleteOneAsync(p => p.Id == id);
}
