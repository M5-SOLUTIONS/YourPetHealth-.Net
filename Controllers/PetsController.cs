using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YourPetHealth.Data;
using YourPetHealth.Models;

namespace YourPetHealth.Controllers
{
    public class PetsController : Controller
    {
        private readonly AppDbContext _context;
        public PetsController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
            => View(await _context.Pets.Include(p => p.Responsavel).ToListAsync());

        public async Task<IActionResult> Create()
        {
            ViewBag.Responsaveis = new SelectList(await _context.Responsaveis.ToListAsync(), "Id", "Nome");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pet p)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Responsaveis = new SelectList(await _context.Responsaveis.ToListAsync(), "Id", "Nome");
                return View(p);
            }
            p.Sexo = p.Sexo.ToUpper();

            _context.Pets.Add(p);
            _context.SaveChanges();

            TempData["Sucesso"] = "Pet cadastrado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var p = await _context.Pets.FindAsync(id);
            if (p == null) return NotFound();
            ViewBag.Responsaveis = new SelectList(await _context.Responsaveis.ToListAsync(), "Id", "Nome", p.ResponsavelId);
            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Pet atualizado)
        
        {
            _context.Pets.Entry(atualizado).State = EntityState.Modified;
            _context.SaveChanges();

            TempData["Sucesso"] = "Pet atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var p = await _context.Pets.Include(x => x.Responsavel).FirstOrDefaultAsync(x => x.Id == id);
            if (p == null) return NotFound();
            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var p = await _context.Pets.FindAsync(id);
            if (p != null)
            {
                _context.Pets.Remove(p);
                await _context.SaveChangesAsync();
            }

            TempData["Sucesso"] = "Pet removido com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}