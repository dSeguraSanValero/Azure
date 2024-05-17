using Microsoft.AspNetCore.Mvc;
using FisioSolution.Data;
using FisioSolution.Models;

namespace FisioSolution.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PhysioController : ControllerBase
{
    private readonly MigrationDbContext _context;


    public PhysioController(MigrationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{PhysioId}")]
    public IActionResult GetPhysio(int PhysioId)
    {
        var physio = _context.Physios.FirstOrDefault(p => p.PhysioId == PhysioId);

        if (physio == null)
        {
            return NotFound();
        }

        HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
        return Ok(physio);
    }


    [HttpGet]
    public IActionResult GetPhysioByRegistrationNumber([FromQuery] int registrationNumber)
    {
        var physio = _context.Physios.FirstOrDefault(p => p.RegistrationNumber == registrationNumber);

        if (physio == null)
        {
            return NotFound();
        }

        HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
        return Ok(physio);
    }


    [HttpGet("GetAllPhysios")]
    public IActionResult GetAllPhysios()
    {
        var physios = _context.Physios.ToList();

        if (physios == null || physios.Count == 0)
        {
            return NotFound();
        }

        HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
        return Ok(physios);
    }


    [HttpPost]
    public IActionResult CreatePhysio(Physio physio)
    {
        try
        {
            _context.Physios.Add(physio);
            _context.SaveChanges();
            
            return CreatedAtAction(nameof(GetPhysio), new { registrationNumber = physio.RegistrationNumber }, physio);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }


    [HttpDelete("{PhysioId}")]
    public IActionResult DeletePhysio(int PhysioId)
    {
        var physio = _context.Physios.FirstOrDefault(p => p.PhysioId == PhysioId);

        if (physio == null)
        {
            return NotFound();
        }

        try
        {
            _context.Physios.Remove(physio);
            _context.SaveChanges();

            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpOptions]
    public IActionResult Options()
    {
        HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
        HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "POST");
        HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");

        return Ok();
    }
}