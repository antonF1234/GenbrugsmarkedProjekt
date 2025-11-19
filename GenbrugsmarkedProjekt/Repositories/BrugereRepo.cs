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
        // Auto-assign missing BrugerId values before returning
        AssignMissingBrugerIds();
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
        if (brugere.BrugerId <= 0)
        {
            brugere.BrugerId = GetNextBrugerId();
        }
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

        // Ensure a user fetched by email has a non-zero BrugerId (updates and returns it)
        public Brugere? EnsureBrugerIdForEmail(string email)
        {
            var bruger = GetByEmail(email);
            if (bruger == null) return null;

            if (bruger.BrugerId <= 0)
            {
                bruger.BrugerId = GetNextBrugerId();
                _brugere.ReplaceOne(b => b.Id == bruger.Id, bruger);
            }

            return bruger;
        }

        // Ensure all users have a non-zero BrugerId (updates missing ones)
        public int AssignMissingBrugerIds()
        {
            var missing = _brugere.Find(b => b.BrugerId <= 0).ToList();
            var updated = 0;
            foreach (var u in missing)
            {
                u.BrugerId = GetNextBrugerId();
                _brugere.ReplaceOne(b => b.Id == u.Id, u);
                updated++;
            }
            return updated;
        }

    // Generate next incremental BrugerId
    public int GetNextBrugerId()
    {
        var last = _brugere
            .Find(b => true)
            .SortByDescending(b => b.BrugerId)
            .Limit(1)
            .FirstOrDefault();

        return (last?.BrugerId ?? 0) + 1;
    }

    //Slet brugere
    public void Delete(string id)
    {
        _brugere.DeleteOne(b => b.Id == id);
    }
}