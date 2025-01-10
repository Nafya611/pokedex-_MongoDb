using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Pokemon
{
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; } = null!;

    [BsonElement("Type")]
    public string Type { get; set; } = null!;
    [BsonElement("Level")]
    public int Level { get; set; }
    
    [BsonElement("Ability")]

    public string Ability { get; set; } = null!;
    
}
