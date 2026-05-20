using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YourPetHealth.Data;
using YourPetHealth.Models;

namespace YourPetHealth.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly AppDbContext _context;
        public ConsultasController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
            => View(await _context.Consultas.Include(c => c.Pet).Include(c => c.Veterinario).ToListAsync());

        public async Task<IActionResult> Create()
        {
            
            ViewBag.PetId = new SelectList(await _context.Pets.ToListAsync(), "Id", "Nome");          
            ViewBag.VeterinarioId = new SelectList(await _context.Veterinarios.ToListAsync(), "Id", "Nome"); 
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(Consulta c)
        {
            ViewBag.PetId = new SelectList(await _context.Pets.ToListAsync(), "Id", "Nome", c.PetId);
            ViewBag.VeterinarioId = new SelectList(await _context.Veterinarios.ToListAsync(), "Id", "Nome", c.VeterinarioId);

            if (c.Status.ToUpper() != "AGENDADA")
            {
                ModelState.AddModelError("Status", "Status deve ser Agendada!");
                return View(c);
            }
            
            c.Status = c.Status.ToUpper();

            if (!ModelState.IsValid)
            {
                ViewBag.PetId = new SelectList(await _context.Pets.ToListAsync(), "Id", "Nome");
                ViewBag.VeterinarioId = new SelectList(await _context.Veterinarios.ToListAsync(), "Id", "Nome");
                return View(c);
            }
            c.Status = c.Status.ToUpper();


            _context.Consultas.Add(c);
            _context.SaveChanges();

            TempData["Sucesso"] = "Consulta cadastrada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var c = await _context.Consultas.FindAsync(id);
            if (c == null) return NotFound();
            ViewBag.Petid = new SelectList(await _context.Pets.ToListAsync(), "Id", "Nome", c.PetId);
            ViewBag.VeterinarioId = new SelectList(await _context.Veterinarios.ToListAsync(), "Id", "Nome", c.VeterinarioId);
            return View(c);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Consulta atualizado)
        {

            ViewBag.PetId = new SelectList(await _context.Pets.ToListAsync(), "Id", "Nome", atualizado.PetId);
            ViewBag.VeterinarioId = new SelectList(await _context.Veterinarios.ToListAsync(), "Id", "Nome", atualizado.VeterinarioId);

            if (atualizado.Status.ToUpper() != "REALIZADA" && atualizado.Status.ToUpper() != "CANCELADA")
            {
                ModelState.AddModelError("Status", "Status deve ser atualizado para Realizada ou Cancelada!");
                return View(atualizado);
            }

            atualizado.Status = atualizado.Status.ToUpper();


            if (!ModelState.IsValid)
            {
                ViewBag.Petid = new SelectList(await _context.Pets.ToListAsync(), "Id", "Nome");
                ViewBag.VeterinarioId = new SelectList(await _context.Veterinarios.ToListAsync(), "Id", "Nome");
                return View(atualizado);
            }
           
            _context.Consultas.Entry(atualizado).State = EntityState.Modified;
            _context.SaveChanges();

            TempData["Sucesso"] = "Consulta atualizada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var c = await _context.Consultas.Include(x => x.Pet).Include(x => x.Veterinario).FirstOrDefaultAsync(x => x.Id == id);
            if (c == null) return NotFound();
            return View(c);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var c = await _context.Consultas.FindAsync(id);
            if (c != null) 
            { 
                
                _context.Consultas.Remove(c); 
                await _context.SaveChangesAsync(); 
            }

            TempData["Sucesso"] = "Consulta removida com sucesso!";
            return RedirectToAction(nameof(Index));

           
        }
    }
}