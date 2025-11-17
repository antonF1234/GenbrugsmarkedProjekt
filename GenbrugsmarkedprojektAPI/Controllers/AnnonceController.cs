using GenbrugsmarkedProjekt.Models;            
using GenbrugsmarkedProjekt.Repositories;     
using Microsoft.AspNetCore.Mvc;
namespace GenbrugsmarkedprojektAPI.Controllers;

public class AnnonceController : Controller
{
    
    private  readonly AnnonceRepo _repo;

    public AnnonceController()
    {
        _repo = new AnnonceRepo();
    }

    public IActionResult Index()
    {
        var annonce = _repo.GetAll();
        return View(annonce);
    }

    public IActionResult Detaljer(string id)
    {
        var annonce = _repo.GetById(id);
        if (annonce == null) return NotFound();
        
        return View(annonce);
    }

    public IActionResult Opret()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Annonce annonce)
    {
        if (!ModelState.IsValid)
            return View(annonce);

        _repo.Create(annonce);
        return RedirectToAction("Index");
    }
    
    public IActionResult Edit(string id)
    {
        var annonce = _repo.GetById(id);
        if (annonce == null) return NotFound();

        return View(annonce);
    }
    [HttpPost]
    public IActionResult Edit(string id, Annonce annonce)
    {
        if (!ModelState.IsValid)
            return View(annonce);

        _repo.Update(id, annonce);
        return RedirectToAction("Index");
    }
    public IActionResult Delete(string id)
    {
        var annonce = _repo.GetById(id);
        if (annonce == null) return NotFound();

        return View(annonce);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(string id)
    {
        _repo.Delete(id);
        return RedirectToAction("Index");
    }
} 