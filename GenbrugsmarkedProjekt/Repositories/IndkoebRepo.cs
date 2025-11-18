using GenbrugsmarkedProjekt.Models;
using MongoDB.Driver;

namespace GenbrugsmarkedProjekt.Repositories;

public class IndkoebRepo
{
    private readonly IMongoCollection<Indkoeb> _indkoeb;

    public IndkoebRepo()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Genbrugsmarked");
        _indkoeb = database.GetCollection<Indkoeb>("indkoeb");
    }

    // Hent alle indkøb
    public List<Indkoeb> GetAll()
    {
        return _indkoeb.Find(i => true).ToList();
    }

    // Hent et enkelt indkøb
    public Indkoeb GetById(string id)
    {
        return _indkoeb.Find(i => i.Id == id).FirstOrDefault();
    }

    // Opret indkøb
    public Indkoeb Create(Indkoeb indkoeb)
    {
        _indkoeb.InsertOne(indkoeb);
        return indkoeb;
    }

    // Opdater indkøb
    public Indkoeb Update(string id, Indkoeb opdateretIndkoeb)
    {
        _indkoeb.ReplaceOne(i => i.Id == id, opdateretIndkoeb);
        return opdateretIndkoeb;
    }

    // Slet indkøb
    public void Delete(string id)
    {
        _indkoeb.DeleteOne(i => i.Id == id);
    }
}