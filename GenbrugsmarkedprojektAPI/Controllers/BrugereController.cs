using GenbrugsmarkedProjekt.Models;            
using GenbrugsmarkedProjekt.Repositories;     
using Microsoft.AspNetCore.Mvc;               

namespace GenbrugsmarkedprojektAPI.Controllers;

// Angiver at dette er en API-controller, og at den automatisk håndterer JSON m.m.
[ApiController]

// Definerer base-route for controlleren → api/Brugere
[Route("api/[controller]")]
public class BrugereController : ControllerBase
{
    private readonly BrugereRepo _repo;   

    // Constructor, hvor repository instansieres
    public BrugereController()
    {
        _repo = new BrugereRepo();
    }

    // GET: api/Brugere
    // Henter alle brugere i databasen
    [HttpGet]
    public IActionResult GetAll()
    {
        var brugere = _repo.GetAll();
        return Ok(brugere);   
    }

    // GET: api/Brugere/{id}
    // Henter en enkelt bruger via dens MongoDB-id
    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var bruger = _repo.GetById(id);

        if (bruger == null) 
            return NotFound();   
        
        return Ok(bruger);       
    }

    // GET: api/Brugere/by-email?email=test@test.dk
    // Henter en bruger ud fra email (bruges fx når man logger ind)
    [HttpGet("by-email")]
    public IActionResult GetByEmail([FromQuery] string email)
    {
        // Tjekker om der overhovedet er sendt en email med
        if (string.IsNullOrWhiteSpace(email))
            return BadRequest(new { message = "Email is required." });

        // Søger efter bruger baseret på email
        var bruger = _repo.GetByEmail(email.Trim());

        if (bruger == null) 
            return NotFound();    

        return Ok(bruger);        
    }

    // POST: api/Brugere
    // Opretter en ny bruger i databasen
    [HttpPost]
    public IActionResult Create([FromBody] Brugere bruger)
    {
        // Kontrollerer om data opfylder model-validering
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Gemmer bruger i databasen
        var created = _repo.Create(bruger);

        return Ok(created);  
    }

    // PUT: api/Brugere/{id}
    // Opdaterer en bruger ud fra id
    [HttpPut("{id}")]
    public IActionResult Update(string id, [FromBody] Brugere bruger)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existing = _repo.GetById(id);

        if (existing == null) 
            return NotFound();   

        // Udfører opdatering
        _repo.Update(id, bruger);

        return Ok(bruger);      
    }

    // DELETE: api/Brugere/{id}
    // Sletter en bruger fra databasen
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var bruger = _repo.GetById(id);

        if (bruger == null) 
            return NotFound();   

        _repo.Delete(id);        

        return NoContent();      
    }
}
