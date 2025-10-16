using Livraria.Data;
using Livraria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Controllers
{
    public class EmprestimosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmprestimosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Emprestimo
        public async Task<IActionResult> Index()
        {
            var emprestimos = _context.Emprestimo
                .Include(e => e.Livro)
                .Include(e => e.Funcionario);
            return View(await emprestimos.ToListAsync());
        }

        // GET: Emprestimo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var emprestimo = await _context.Emprestimo
                .Include(e => e.Livro)
                .Include(e => e.Funcionario)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (emprestimo == null) return NotFound();

            return View(emprestimo);
        }

        // GET: Emprestimo/Create
        public IActionResult Create()
        {
            ViewData["LivroId"] = new SelectList(_context.Livros, "Id", "Titulo");
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome");
            return View();
        }

        // POST: Emprestimo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emprestimo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LivroId"] = new SelectList(_context.Livros, "Id", "Titulo", emprestimo.LivroId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", emprestimo.FuncionarioId);
            return View(emprestimo);
        }

        // GET: Emprestimo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var emprestimo = await _context.Emprestimo.FindAsync(id);
            if (emprestimo == null) return NotFound();

            ViewData["LivroId"] = new SelectList(_context.Livros, "Id", "Titulo", emprestimo.LivroId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", emprestimo.FuncionarioId);
            return View(emprestimo);
        }

        // POST: Emprestimo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Emprestimo emprestimo)
        {
            if (id != emprestimo.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emprestimo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Emprestimo.Any(e => e.Id == emprestimo.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["LivroId"] = new SelectList(_context.Livros, "Id", "Titulo", emprestimo.LivroId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", emprestimo.FuncionarioId);
            return View(emprestimo);
        }

        // GET: Emprestimo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var emprestimo = await _context.Emprestimo
                .Include(e => e.Livro)
                .Include(e => e.Funcionario)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (emprestimo == null) return NotFound();

            return View(emprestimo);
        }

        // POST: Emprestimo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emprestimo = await _context.Emprestimo.FindAsync(id);
            if (emprestimo != null)
            {
                _context.Emprestimo.Remove(emprestimo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
