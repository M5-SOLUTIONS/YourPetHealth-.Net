using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourPetHealth.Data;
using YourPetHealth.Models;

namespace YourPetHealth.Controllers
{
    public class ResponsaveisController : Controller
    {
        private readonly AppDbContext _context;
        public ResponsaveisController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
            => View(await _context.Responsaveis.ToListAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Responsavel r)
        {
            if (!ModelState.IsValid) return View(r);

            var vet = _context.Responsaveis.Where(x => x.Email == r.Email).ToList();
            if (vet.Count > 0)
            {
                ModelState.AddModelError("Email", "Email já cadastrado.");
                return View(r);
            }

            _context.Responsaveis.Add(r);
            _context.SaveChanges();

            TempData["Sucesso"] = "Responsável cadastrado com sucesso!";
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int id)
        {
            var r = await _context.Responsaveis.FindAsync(id);
            if (r == null) return NotFound();
            return View(r);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Responsavel atualizado)
        {
            _context.Responsaveis.Entry(atualizado).State = EntityState.Modified;
            _context.SaveChanges();

            TempData["Sucesso"] = "Responsável atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var r = await _context.Responsaveis.FindAsync(id);
            if (r == null) return NotFound();
            return View(r);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var r = await _context.Responsaveis.FindAsync(id);
            if (r != null)
            {
                _context.Responsaveis.Remove(r);
                await _context.SaveChangesAsync();
            }
            TempData["Sucesso"] = "Responsável removido com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}