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

        // GET: Emprestimos
        public async Task<IActionResult> Index()
        {
            var emprestimos = _context.Emprestimo
                .Include(e => e.Livro)
                .Include(e => e.Funcionario)
                .Include(e => e.Cliente);
            return View(await emprestimos.ToListAsync());
        }

        // GET: Emprestimos/Create
        public IActionResult Create()
        {
            ViewData["LivroId"] = new SelectList(_context.Livros, "Id", "Titulo");
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome");
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome");

            var model = new Emprestimo
            {
                DataEmprestimo = DateTime.Today,
                DataPrevistaDevolucao = DateTime.Today.AddDays(7),
                Status = "Em andamento"
            };

            return View(model);
        }

        // POST: Emprestimos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Emprestimo emprestimo)
        {
            if (emprestimo.DataEmprestimo == default)
                emprestimo.DataEmprestimo = DateTime.Today;

            if (emprestimo.DataPrevistaDevolucao == default)
                emprestimo.DataPrevistaDevolucao = emprestimo.DataEmprestimo.AddDays(7);

            if (string.IsNullOrEmpty(emprestimo.Status))
                emprestimo.Status = "Em andamento";

            if (ModelState.IsValid)
            {
                _context.Add(emprestimo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["LivroId"] = new SelectList(_context.Livros, "Id", "Titulo", emprestimo.LivroId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", emprestimo.FuncionarioId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", emprestimo.ClienteId);

            return View(emprestimo);
        }

        // GET: Emprestimos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var emprestimo = await _context.Emprestimo
                .Include(e => e.Livro)
                .Include(e => e.Funcionario)
                .Include(e => e.Cliente)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (emprestimo == null)
                return NotFound();

            ViewData["LivroId"] = new SelectList(_context.Livros, "Id", "Titulo", emprestimo.LivroId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", emprestimo.FuncionarioId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", emprestimo.ClienteId);

            return View(emprestimo);
        }

        // POST: Emprestimos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Emprestimo emprestimo)
        {
            if (id != emprestimo.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emprestimo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Emprestimo.Any(e => e.Id == emprestimo.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["LivroId"] = new SelectList(_context.Livros, "Id", "Titulo", emprestimo.LivroId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", emprestimo.FuncionarioId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", emprestimo.ClienteId);

            return View(emprestimo);
        }

        // GET: Emprestimos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var emprestimo = await _context.Emprestimo
                .Include(e => e.Livro)
                .Include(e => e.Funcionario)
                .Include(e => e.Cliente)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (emprestimo == null)
                return NotFound();

            return View(emprestimo);
        }

        // GET: Emprestimos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var emprestimo = await _context.Emprestimo
                .Include(e => e.Livro)
                .Include(e => e.Funcionario)
                .Include(e => e.Cliente)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (emprestimo == null)
                return NotFound();

            return View(emprestimo);
        }

        // POST: Emprestimos/Delete/5
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
