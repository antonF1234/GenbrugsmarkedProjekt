using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace GenbrugsmarkedProjekt.Models;

public class Annonce
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    
    public string Titel { get; set; } = "";
    public string Beskrivelse { get; set; } = "";
    public decimal Pris { get; set; }
    public string Stand { get; set; } = "God";
    public string Str { get; set; } = "";
    public string FotoUrl { get; set; } = "";

    public string Status { get; set; } = "Aktiv";
    public DateTime Dato { get; set; } = DateTime.Now;

    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("koeberId")]
    public string? KoeberId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("brugerId")]
    public string BrugerId { get; set; } = "";

    public string Lokalitet { get; set; } = "EAAA";
    public string? Lokale { get; set; }
}