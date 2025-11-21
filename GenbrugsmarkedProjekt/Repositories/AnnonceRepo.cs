using GenbrugsmarkedProjekt.Models;
using MongoDB.Driver;

namespace GenbrugsmarkedProjekt.Repositories;

public class AnnonceRepo
{
    private readonly IMongoCollection<Annonce> _annonce; //forbindelse til mongoDB

    public AnnonceRepo()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Genbrugsmarked");
        _annonce = database.GetCollection<Annonce>("annonce");
    }
    
    
    public List<Annonce> GetActive() //marked side, henter kun aktive annoncer
    {
        return _annonce.Find(a => a.Status == "Aktiv").ToList();
    }

    public List<Annonce> GetAll() //henter alle annoncer uanset status, under mine annoncer
    {
        return _annonce.Find(a => true).ToList();
    }
    
    public Annonce? GetById(string id) //henter annonce ud fra mongodb id
    {
        return _annonce.Find(a => a.Id == id).FirstOrDefault();
    }

    public Annonce Create(Annonce annonce) //sætter ny annonce i databasen
    {
        _annonce.InsertOne(annonce);
        return annonce;
    }

    public void Update(string id, Annonce updated) // Erstatter en eksisterende annonce med nye værdier
    {
        _annonce.ReplaceOne(a => a.Id == id, updated);
    }

    public void Delete(string id) // slet
    {
        _annonce.DeleteOne(a => a.Id == id);
    }

    // Filtrering efter pris og stand 
    public List<Annonce> Filter(decimal? minPris, decimal? maxPris, string? stand, string? str)
    {
        var filter = Builders<Annonce>.Filter.Empty;

        if (minPris.HasValue)
            filter &= Builders<Annonce>.Filter.Gte(a => a.Pris, minPris.Value);

        if (maxPris.HasValue)
            filter &= Builders<Annonce>.Filter.Lte(a => a.Pris, maxPris.Value);

        if (!string.IsNullOrEmpty(stand))
            filter &= Builders<Annonce>.Filter.Eq(a => a.Stand, stand);

        if (!string.IsNullOrEmpty(str))
            filter &= Builders<Annonce>.Filter.Eq(a => a.Str, str);

        return _annonce.Find(filter).ToList();
    }

    public List<Annonce> Search(string text) // Tekstsøgning i titel og beskrivelse
    {
        text = text.ToLower();

        return _annonce.Find(a =>
            a.Titel.ToLower().Contains(text) ||
            a.Beskrivelse.ToLower().Contains(text)
        ).ToList();
    }

    public void RequestPurchase(string annonceId, string køberId) //"Anmod om køb"
    {
        var annonce = GetById(annonceId);
        if (annonce == null) return;

        annonce.Status = "Reserveret";
        annonce.KoeberId = køberId;

        Update(annonceId, annonce);
    }
}
