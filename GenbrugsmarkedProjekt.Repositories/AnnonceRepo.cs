using GenbrugsmarkedProjekt.Models;
using MongoDB.Driver;

namespace GenbrugsmarkedProjekt.Repositories;

public class AnnonceRepo
{
    private readonly IMongoCollection<Annonce> _annoncer;

    public AnnonceRepo()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Genbrugsmarked");
        _annoncer = database.GetCollection<Annonce>("annoncer");
    }

    public Annonce Create(Annonce annonce)
    {
        _annoncer.InsertOne(annonce);
        return annonce;
    }

    public List<Annonce> GetAll()
    {
        return _annoncer.Find(a => true).ToList();
    }

    public Annonce GetById(string id)
    {
        return _annoncer.Find(a => a.Id == id).FirstOrDefault();
    }

    public Annonce Update(string id, Annonce opdateretAnnonce)
    {
        _annoncer.ReplaceOne(a => a.Id == id, opdateretAnnonce);
        return opdateretAnnonce;
    }

    public void Delete(string id)
    {
        _annoncer.DeleteOne(a => a.Id == id);
    }
}
