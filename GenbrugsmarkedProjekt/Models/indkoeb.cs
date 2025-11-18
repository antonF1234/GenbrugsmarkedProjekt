using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GenbrugsmarkedProjekt.Models;

public class Indkoeb
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }
    
    public string KoeberId { get; set; }
    public string annonceId { get; set; }
    
    public DateTime KøbsDato { get; set; } = DateTime.UtcNow;
}