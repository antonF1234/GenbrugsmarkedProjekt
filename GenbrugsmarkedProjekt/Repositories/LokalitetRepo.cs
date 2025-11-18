using GenbrugsmarkedProjekt.Models;
using MongoDB.Driver;

namespace GenbrugsmarkedProjekt.Repositories;

public class LokalitetRepo
{
    private readonly IMongoCollection<Lokaliteter> _lokaliteter;

    public LokalitetRepo()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("Genbrugsmarked");
        _lokaliteter = database.GetCollection<Lokaliteter>("lokaliteter");
    }

    // Hent alle lokaliteter
    public List<Lokaliteter> GetAll()
    {
        return _lokaliteter.Find(l => true).ToList();
    }

    // Hent en enkelt lokalitet
    public Lokaliteter GetById(string id)
    {
        return _lokaliteter.Find(l => l.Id == id).FirstOrDefault();
    }

    // Opret lokalitet
    public Lokaliteter Create(Lokaliteter lokalitet)
    {
        _lokaliteter.InsertOne(lokalitet);
        return lokalitet;
    }

    // Opdater lokalitet
    public Lokaliteter Update(string id, Lokaliteter opdateretLokalitet)
    {
        _lokaliteter.ReplaceOne(l => l.Id == id, opdateretLokalitet);
        return opdateretLokalitet;
    }

    // Slet lokalitet
    public void Delete(string id)
    {
        _lokaliteter.DeleteOne(l => l.Id == id);
    }
}