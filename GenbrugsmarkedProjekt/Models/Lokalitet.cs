using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GenbrugsmarkedProjekt.Models;

public class Lokaliteter
{
    [BsonId]
    public required string Id { get; set; }
    
    public string LokalitetNavn { get; set; } = "EAAA";     
    public string? Lokale { get; set; }
    // Hardcoded liste over lokaler
    public static List<Lokaliteter> StandardLokaler => new()
    {
        new Lokaliteter { Id = ObjectId.GenerateNewId().ToString(), LokalitetNavn = "EAAA", Lokale = "Lokale A.12" },
        new Lokaliteter { Id = ObjectId.GenerateNewId().ToString(), LokalitetNavn = "EAAA", Lokale = "Lokale B.24" },
        new Lokaliteter { Id = ObjectId.GenerateNewId().ToString(), LokalitetNavn = "EAAA", Lokale = "Lokale C.03" },
        new Lokaliteter { Id = ObjectId.GenerateNewId().ToString(), LokalitetNavn = "EAAA", Lokale = "Kantinen" }
    };
}