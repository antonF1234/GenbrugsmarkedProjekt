using GenbrugsmarkedProjekt.Models;            
using GenbrugsmarkedProjekt.Repositories;     
using Microsoft.AspNetCore.Mvc;


namespace GenbrugsmarkedprojektAPI.Controllers;

[ApiController]
[Route("api/[controller]")] // route bliver til - api/controller
public class AnnonceController : ControllerBase
{
    private readonly AnnonceRepo _repo = new(); // repository pattern, henter og gemmer ting basicly

    [HttpGet("active")] // henter aktive annoncer - bruges på forside/marked
    public IActionResult GetActive()
    {
        return Ok(_repo.GetActive());
    }
    
    [HttpGet] // henter alle annoncer
    public IActionResult GetAll()
    {
        var annonce = _repo.GetAll();
        return Ok(annonce);
    }

    [HttpGet("{id}")] // henter en specifik annonce udfra mongoDB
    public IActionResult GetById(string id)
    {
        var annonce = _repo.GetById(id);
        if (annonce == null) return NotFound();
        
        return Ok(annonce);
    }

    [HttpPost] // Opretter ny annonce – modtager JSON fra Blazor
    public IActionResult Create([FromBody] Annonce annonce)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = _repo.Create(annonce);
        return Ok(created);
    }

    [HttpPut("{id}")] // Opdaterer en eksisterende annonce, bruges ved redigering)
    public IActionResult Update(string id, [FromBody] Annonce annonce)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existing = _repo.GetById(id);
        if (existing == null) return NotFound();

        _repo.Update(id, annonce);
        return Ok(annonce);
    }

    [HttpDelete("{id}")] // sletter annonce permanent
    public IActionResult Delete(string id)
    {
        var annonce = _repo.GetById(id);
        if (annonce == null) return NotFound();

        _repo.Delete(id);
        return NoContent();
    }
    //simpel filtrering 
    [HttpGet("filter")]
    public IActionResult Filter([FromQuery] decimal? minPris, [FromQuery] decimal? maxPris, [FromQuery] string? stand, [FromQuery] string? str)
    {
        return Ok(_repo.Filter(minPris, maxPris, stand, str));
    }
    [HttpPost("{id}/request")] // købsanmodning oprettes her
    public IActionResult RequestPurchase(string id, [FromQuery] string køberId)
    {
        var annonce = _repo.GetById(id);
        if (annonce == null) return NotFound();

        _repo.RequestPurchase(id, køberId); //Her sættes KoeberId på annoncen eller oprettes en Indkoeb
        return Ok();
    }

    

}