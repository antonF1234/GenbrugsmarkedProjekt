using GenbrugsmarkedProjekt.Models;
using GenbrugsmarkedProjekt.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GenbrugsmarkedprojektAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class indkoebController  : ControllerBase
{
    private readonly IndkoebRepo _repo;
    
    public indkoebController()
    {
        _repo = new IndkoebRepo();
    }

    [HttpGet]
    public IActionResult Get()  // alle anmodninger/indkøb
    {
        var indkoeb = _repo.GetAll(); // hent liste af alle indkøb som er anmodninger
        return Ok(indkoeb); // response 200 hvis det er ok
    }

    [HttpGet("{id}")]   // hent specifikt anmodninger/indkøb vha. id
    public IActionResult GetById(string id)
    {
        var indkoeb =  _repo.GetById(id);
        if (indkoeb == null) return NotFound(); // status 404 hvis det ikke findes
        
        return Ok(indkoeb);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Indkoeb indkoeb)   // henter data fra body
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        var created = _repo.Create(indkoeb);
        return Ok(created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(string id, [FromBody] Indkoeb indkoeb)  // opdaterer data fra body
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var existing = _repo.GetById(id);
        if (existing == null) return NotFound();
        
        var updated = _repo.Update(id, indkoeb);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)  // brug id til at slette
    {
        var indkoeb = _repo.GetById(id);
        if (indkoeb == null) return NotFound();

        _repo.Delete(id);
        return NoContent();
    }
    
    [HttpGet("mine")]
    public IActionResult GetMine([FromQuery] string KoeberId) // hent mine anmodninger
    {
        return Ok(_repo.GetByBuyer(KoeberId));
    }
    
    [HttpPatch("{id}/status")]
    public IActionResult UpdateStatus(string id, [FromBody] StatusUpdateModel model)    // opdater status
    {
        var result = _repo.UpdateStatus(id, model.status);

        if (!result)    // hvis intet resultat så returner 404 not found
            return NotFound();

        return Ok();
    }

    public class StatusUpdateModel  // lille model til at opdatere status
    {
        public string status { get; set; }  // status string
    }
    
    

}