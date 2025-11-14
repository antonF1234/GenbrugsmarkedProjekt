using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Driver;

namespace GenbrugsmarkedProjekt.Repositories;

public class AnnonceRepo
{
    private readonly IMongoCollection<Annonce> _annonce;>
    public AnnonceRepo()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Genbrugsmarked");
        _annonce = database.GetCollection<Annonce>("annonce");
    }
}