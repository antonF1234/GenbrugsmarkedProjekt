namespace GenbrugsmarkedProjekt.Models;

public class Brugere
{
    [BsonId] public ObjectId Id { get; set; }
    public int BrugerId { get; set; }
    public string Navn { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";      // Vi Bruger BCrypt
}