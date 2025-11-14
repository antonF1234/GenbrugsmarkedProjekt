using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace GenbrugsmarkedProjekt.Models;

public class Lokalitet
{
    [BsonId] public ObjectId Id { get; set; };
    
    public string Lokalitet { get; set; } = "EAAA";     
    public string? Lokale { get; set; }
}