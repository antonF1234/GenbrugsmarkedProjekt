using GenbrugsmarkedProjekt.Models;            
using GenbrugsmarkedProjekt.Repositories;     
using Microsoft.AspNetCore.Mvc;

namespace GenbrugsmarkedprojektAPI.Controllers;

public class BrugereController : Controller
{
    
    private  readonly BrugereRepo _repo;

    public BrugereController()
    {
        _repo = new BrugereRepo();
    }

    public IActionResult Index()
    {
        var brugere = _repo.GetAll();
        return View(brugere);
    }

    public IActionResult Detaljer(string id)
    {
        var brugere = _repo.GetById(id);
        if (brugere == null) return NotFound();
        
        return View(brugere);
    }

    public IActionResult Opret()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Brugere bruger)
    {
        if (!ModelState.IsValid)
            return View(bruger);

        _repo.Create(bruger);
        return RedirectToAction("Index");
    }
    
    public IActionResult Edit(string id)
    {
        var bruger = _repo.GetById(id);
        if (bruger == null) return NotFound();

        return View(bruger);
    }
    [HttpPost]
    public IActionResult Edit(string id, Brugere bruger)
    {
        if (!ModelState.IsValid)
            return View(bruger);

        _repo.Update(id, bruger);
        return RedirectToAction("Index");
    }
    public IActionResult Delete(string id)
    {
        var bruger = _repo.GetById(id);
        if (bruger == null) return NotFound();

        return View(bruger);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(string id)
    {
        _repo.Delete(id);
        return RedirectToAction("Index");
    }
}    