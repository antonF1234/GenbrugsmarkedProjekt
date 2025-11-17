using GenbrugsmarkedProjekt.Models;            
using GenbrugsmarkedProjekt.Repositories;     
using Microsoft.AspNetCore.Mvc;

namespace GenbrugsmarkedprojektAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrugereController : ControllerBase
{
    private readonly BrugereRepo _repo;

    public BrugereController()
    {
        _repo = new BrugereRepo();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var brugere = _repo.GetAll();
        return Ok(brugere);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var bruger = _repo.GetById(id);
        if (bruger == null) return NotFound();
        
        return Ok(bruger);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Brugere bruger)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = _repo.Create(bruger);
        return Ok(created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(string id, [FromBody] Brugere bruger)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existing = _repo.GetById(id);
        if (existing == null) return NotFound();

        _repo.Update(id, bruger);
        return Ok(bruger);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var bruger = _repo.GetById(id);
        if (bruger == null) return NotFound();

        _repo.Delete(id);
        return NoContent();
    }
}