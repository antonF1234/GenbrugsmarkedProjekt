using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GenbrugsmarkedProjekt.Models;

public class indkoeb
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    public string KøberId { get; set; }
    public string annonceId { get; set; }
    
    public DateTime KøbsDato { get; set; } = DateTime.UtcNow;
}