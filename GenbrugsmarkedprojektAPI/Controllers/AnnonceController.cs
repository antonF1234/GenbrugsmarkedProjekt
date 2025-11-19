using GenbrugsmarkedProjekt.Models;            
using GenbrugsmarkedProjekt.Repositories;     
using Microsoft.AspNetCore.Mvc;


namespace GenbrugsmarkedprojektAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnnonceController : ControllerBase
{
    private readonly AnnonceRepo _repo = new();

    [HttpGet("active")]
    public IActionResult GetActive()
    {
        return Ok(_repo.GetActive());
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var annonce = _repo.GetAll();
        return Ok(annonce);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var annonce = _repo.GetById(id);
        if (annonce == null) return NotFound();
        
        return Ok(annonce);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Annonce annonce)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = _repo.Create(annonce);
        return Ok(created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(string id, [FromBody] Annonce annonce)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existing = _repo.GetById(id);
        if (existing == null) return NotFound();

        _repo.Update(id, annonce);
        return Ok(annonce);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var annonce = _repo.GetById(id);
        if (annonce == null) return NotFound();

        _repo.Delete(id);
        return NoContent();
    }
    //filtrer
    [HttpGet("filter")]
    public IActionResult Filter([FromQuery] decimal? minPris, [FromQuery] decimal? maxPris, [FromQuery] string? stand, [FromQuery] string? str)
    {
        return Ok(_repo.Filter(minPris, maxPris, stand, str));
    }
    [HttpPost("{id}/request")]
    public IActionResult RequestPurchase(string id, [FromQuery] string køberId)
    {
        var annonce = _repo.GetById(id);
        if (annonce == null) return NotFound();

        _repo.RequestPurchase(id, køberId);
        return Ok();
    }

    

}