using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
    
namespace GenbrugsmarkedProjekt.Models;

public class Brugere
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    
    public required string Id { get; set; }
    
    public int BrugerId { get; set; }
    public string Navn { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";      // Vi Bruger BCrypt
}