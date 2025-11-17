using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GenbrugsmarkedProjekt.Models;

public class Lokalitet
{
    [BsonId]
    public ObjectId Id { get; set; }
    
    public string LokalitetNavn { get; set; } = "EAAA";     
    public string? Lokale { get; set; }
}