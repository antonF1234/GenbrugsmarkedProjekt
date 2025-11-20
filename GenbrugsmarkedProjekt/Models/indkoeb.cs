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
    
    public string status { get; set; } = "Afventer"; // som standard er anmodningen afventer
    public DateTime KøbsDato { get; set; } = DateTime.UtcNow;
}