using Microsoft.AspNetCore.Mvc;
using YourPetHealth.Data; // ajuste o namespace se necessário

namespace YourPetHealth.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalResponsaveis = _context.Responsaveis.Count();
            ViewBag.TotalVeterinarios = _context.Veterinarios.Count();
            ViewBag.TotalPets = _context.Pets.Count();
            ViewBag.TotalConsultas = _context.Consultas.Count();
            ViewBag.ConsultasAgendadas = _context.Consultas
                .Count(c => c.Status == "AGENDADA");

            return View();
        }
    }
}