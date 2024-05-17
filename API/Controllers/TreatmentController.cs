using Microsoft.AspNetCore.Mvc;
using FisioSolution.Data;
using FisioSolution.Models;


namespace FisioSolution.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TreatmentController : ControllerBase
{
    private readonly MigrationDbContext _context;

    public TreatmentController(MigrationDbContext context)
    {
        _context = context;
    }

    public class TreatmentFilterDto
    {
        public int? PatientId { get; set; }
        public int? PhysioId { get; set; }
    }

    [HttpGet("{TreatmentId}")]
    public IActionResult GetTreatment(int TreatmentId)
    {
        var treatment = _context.Treatments.FirstOrDefault(p => p.TreatmentId == TreatmentId);

        if (treatment == null)
        {
            return NotFound();
        }

        HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
        return Ok(treatment);
    }



    [HttpGet]
    public IActionResult GetTreatmentsByFilter([FromQuery] TreatmentFilterDto filter)
    {
        if (filter.PatientId.HasValue)
        {
            var treatments = _context.Treatments.Where(t => t.PatientId == filter.PatientId).ToList();

            if (treatments.Count == 0)
            {
                return NotFound();
            }

            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
            return Ok(treatments);
        }
        else if (filter.PhysioId.HasValue)
        {
            var treatments = _context.Treatments.Where(t => t.PhysioId == filter.PhysioId).ToList();

            if (treatments.Count == 0)
            {
                return NotFound();
            }

            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
            return Ok(treatments);
        }
        else
        {
            return BadRequest("Debe especificar al menos un ID de paciente o ID de fisioterapeuta.");
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

    [HttpPost]
    public IActionResult CreateTreatment(ModelTreatment treatmentInput)
    {
        try
        {
            if (treatmentInput.PhysioId == 0 || treatmentInput.PatientId == 0)
            {
                return BadRequest("Los campos 'PhysioId' y 'PatientId' son obligatorios.");
            }

            var treatment = new Treatment
            {
                PhysioId = treatmentInput.PhysioId,
                PatientId = treatmentInput.PatientId,
                TreatmentCause = treatmentInput.TreatmentCause,
                TreatmentDate = treatmentInput.TreatmentDate,
                MoreSessionsNeeded = treatmentInput.MoreSessionsNeeded
            };

            _context.Treatments.Add(treatment);
            _context.SaveChanges();
            
            return CreatedAtAction(nameof(GetTreatment), new { physioId = treatment.PhysioId }, treatment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpDelete("{TreatmentId}")]
    public IActionResult DeleteTreatment(int TreatmentId)
    {
        try
        {
            var treatment = _context.Treatments.FirstOrDefault(p => p.TreatmentId == TreatmentId);
            if (treatment == null)
            {
                return NotFound();
            }

            _context.Treatments.Remove(treatment);
            _context.SaveChanges();
            
            return NoContent(); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }


}

    public class ModelTreatment
    {
        public int PhysioId { get; set; }
        public int PatientId { get; set; }
        public string TreatmentCause { get; set; }
        public DateTime TreatmentDate { get; set; }
        public bool MoreSessionsNeeded { get; set; }
    }