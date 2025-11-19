using GenbrugsmarkedProjekt.Models;
using GenbrugsmarkedProjekt.Repositories;
using GenbrugsmarkedprojektAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GenbrugsmarkedprojektAPI.Controllers;

[ApiController]
[Route("api/autentificering")]
public class AutentificeringController : ControllerBase
{
    private readonly BrugereRepo _brugereRepo;

    public AutentificeringController()
    {
        _brugereRepo = new BrugereRepo();
    }

    [HttpPost("opretbruger")]
    public IActionResult OpretBruger([FromBody] OpretBrugerRequest request)
    {
        if (request is null ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password) ||
            string.IsNullOrWhiteSpace(request.Navn))
        {
            return BadRequest(new { message = "Ugyldige input." });
        }

        var existing = _brugereRepo.GetByEmail(request.Email);
        if (existing != null)
        {
            return Conflict(new { message = "Email er allerede i brug." });
        }

        var user = new Brugere
        {
            Email = request.Email.Trim(),
            Navn = request.Navn.Trim(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            BrugerId = _brugereRepo.GetNextBrugerId(),
            Id = null
        };

        _brugereRepo.Create(user);

        return Ok(new { user.Id, user.Navn, user.Email });
    }

    [HttpPost("logind")]
    public IActionResult LogInd([FromBody] LogIndRequest request)
    {
        if (request is null ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "Ugyldige input." });
        }

        var user = _brugereRepo.GetByEmail(request.Email.Trim());
        if (user == null)
        {
            return Unauthorized(new { message = "Forkert email eller password." });
        }

        var ok = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash ?? "");
        if (!ok)
        {
            return Unauthorized(new { message = "Forkert email eller password." });
        }

        return Ok(new { user.Id, user.Navn, user.Email });
    }
}
