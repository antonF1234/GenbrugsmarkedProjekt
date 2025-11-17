using GenbrugsmarkedProjekt.Models;
using MongoDB.Driver;

namespace GenbrugsmarkedProjekt.Repositories;

public class BrugereRepo
{
    private readonly IMongoCollection<Brugere> _brugere; // Brugere collection i databasen og Modellen brugere

    public BrugereRepo()
    {
    var client = new MongoClient("mongodb://localhost:27017");
    var database = client.GetDatabase("Genbrugsmarked");
    _brugere = database.GetCollection<Brugere>("brugere");
    }
    
    //Get vores brugere
    public List<Brugere> GetAll()
    {   
        return _brugere.Find(b => true).ToList();
    }
    
    //En bruger
    public Brugere GetById(string id)
    {
        return _brugere.Find(b => b.Id == id).FirstOrDefault();
    }
    
    //Opret brugere
    public Brugere Create(Brugere brugere)
    {
        _brugere.InsertOne(brugere);
        return brugere;
    }
    
    //Opdater brugere
    public Brugere Update(string id, Brugere opdateretbrugere)
    {
        _brugere.ReplaceOne(b =>b.Id == id, opdateretbrugere);
        return opdateretbrugere;
    }
    
    // Find bruger via email
    public Brugere? GetByEmail(string email)
    {
        return _brugere.Find(b => b.Email == email).FirstOrDefault();
    }

    //Slet brugere
    public void Delete(string id)
    {
        _brugere.DeleteOne(b => b.Id == id);
    }
}