using System.ComponentModel.DataAnnotations.Schema;
using GenbrugsmarkedProjekt.Models;
using MongoDB.Driver;

namespace GenbrugsmarkedProjekt.Repositories;

public class AnnonceRepo
{
    private readonly IMongoCollection<Annonce> _annonce;
    public AnnonceRepo()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Genbrugsmarked");
        _annonce = database.GetCollection<Annonce>("annonce");
    }

    // GET, vores annoncer
    public List<Annonce> GetAll()
    {
        return _annonce.Find (a => true).ToList();
    }
    
    // en annonce
    public Annonce GetById(string id)
    {
        return _annonce.Find(a => a.Id == id).FirstOrDefault();
    }
    
    //opret
    public Annonce Opret(Annonce annonce)
    {
        _annonce.InsertOne(annonce);
        return annonce;
    }
    //opdatere
    public void opdater(string id, Annonce Opdaterannonce)
    {
        _annonce.ReplaceOne(a => a.Id == id, Opdaterannonce);
    }
    
    //slet 
    public void Slet(string id)
    {
        _annonce.DeleteOne(a => a.Id == id);
    }
}