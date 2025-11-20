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
    public IActionResult Get()
    {
        var indkoeb = _repo.GetAll();
        return Ok(indkoeb);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var indkoeb =  _repo.GetById(id);
        if (indkoeb == null) return NotFound();
        
        return Ok(indkoeb);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Indkoeb indkoeb)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        var created = _repo.Create(indkoeb);
        return Ok(created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(string id, [FromBody] Indkoeb indkoeb)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var existing = _repo.GetById(id);
        if (existing == null) return NotFound();
        
        var updated = _repo.Update(id, indkoeb);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var indkoeb = _repo.GetById(id);
        if (indkoeb == null) return NotFound();

        _repo.Delete(id);
        return NoContent();
    }
    
    [HttpGet("mine")]
    public IActionResult GetMine([FromQuery] string KoeberId)
    {
        return Ok(_repo.GetByBuyer(KoeberId));
    }
    
    [HttpPatch("{id}/status")]
    public IActionResult UpdateStatus(string id, [FromBody] StatusUpdateModel model)
    {
        var result = _repo.UpdateStatus(id, model.status);

        if (!result)
            return NotFound();

        return Ok();
    }

    public class StatusUpdateModel
    {
        public string status { get; set; }
    }

}