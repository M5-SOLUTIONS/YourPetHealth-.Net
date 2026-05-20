using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourPetHealth.Data;
using YourPetHealth.Models;

namespace YourPetHealth.Controllers
{
    public class VeterinariosController : Controller
    {
        private readonly AppDbContext _context;
        public VeterinariosController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
            => View(await _context.Veterinarios.ToListAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Veterinario v)
        {
            if (!ModelState.IsValid) return View(v);

            var vet = _context.Veterinarios.Where(x => x.Email == v.Email).ToList();
            if (vet.Count > 0)
            {
                ModelState.AddModelError("Email", "Email já cadastrado.");
                return View(v);
            }

            vet = _context.Veterinarios.Where(x => x.Crmv == v.Crmv).ToList();
            if (vet.Count > 0)
            {
                ModelState.AddModelError("Crmv", "CRMV já cadastrado.");
                return View(v);
            }

            _context.Veterinarios.Add(v);
            _context.SaveChanges();

            TempData["Sucesso"] = "Veterinário cadastrado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var v = await _context.Veterinarios.FindAsync(id);
            if (v == null) return NotFound();
            return View(v);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Veterinario atualizado)
        {
            _context.Veterinarios.Entry(atualizado).State = EntityState.Modified;
            _context.SaveChanges();

            TempData["Sucesso"] = "Veterinário atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var v = await _context.Veterinarios.FindAsync(id);
            if (v == null) return NotFound();
            return View(v);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var v = await _context.Veterinarios.FindAsync(id);
            if (v != null)
            {
                _context.Veterinarios.Remove(v);
                await _context.SaveChangesAsync();
            }
            TempData["Sucesso"] = "Veterinário removido com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}