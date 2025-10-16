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
                .Include(e => e.Funcionario);
            return View(await emprestimos.ToListAsync());
        }

        // GET: Emprestimos/Details/5
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

        // GET: Emprestimos/Create
        public IActionResult Create()
        {
            ViewData["LivroId"] = new SelectList(_context.Livros, "Id", "Titulo");
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome");

            // Preencher valores padrão para a view
            var model = new Emprestimo
            {
                DataEmprestimo = DateTime.Today,
                DataPrevistaDevolucao = DateTime.Today.AddDays(7)
            };

            return View(model);
        }

        // POST: Emprestimos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Emprestimo emprestimo)
        {
            // --- LINHA CRÍTICA: garantir que DataPrevistaDevolucao nunca seja nula ---
            // O usuário pode ter enviado vazio, ou o binding pode ter falhado.
            // Vamos inspecionar explicitamente o formulário e atribuir padrão se necessário.
            var formValue = Request.Form["DataPrevistaDevolucao"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(formValue))
            {
                // Se DataEmprestimo também estiver vazia, garante valor
                if (emprestimo.DataEmprestimo == default(DateTime))
                    emprestimo.DataEmprestimo = DateTime.Today;

                emprestimo.DataPrevistaDevolucao = emprestimo.DataEmprestimo.AddDays(7);
            }
            else
            {
                // tenta parse seguro (caso o browser envie em cultura diferente)
                if (!DateTime.TryParse(formValue, out var parsed))
                {
                    // fallback
                    if (emprestimo.DataEmprestimo == default(DateTime))
                        emprestimo.DataEmprestimo = DateTime.Today;
                    emprestimo.DataPrevistaDevolucao = emprestimo.DataEmprestimo.AddDays(7);
                }
                else
                {
                    emprestimo.DataPrevistaDevolucao = parsed.Date;
                }
            }

            // se DataEmprestimo estiver vazia, garantir
            var formEmp = Request.Form["DataEmprestimo"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(formEmp) && emprestimo.DataEmprestimo == default(DateTime))
                emprestimo.DataEmprestimo = DateTime.Today;
            else if (!string.IsNullOrWhiteSpace(formEmp) && DateTime.TryParse(formEmp, out var dEmp))
                emprestimo.DataEmprestimo = dEmp.Date;

            if (ModelState.IsValid)
            {
                _context.Add(emprestimo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // repopula dropdowns
            ViewData["LivroId"] = new SelectList(_context.Livros, "Id", "Titulo", emprestimo.LivroId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", emprestimo.FuncionarioId);
            return View(emprestimo);
        }

        // GET: Emprestimos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var emprestimo = await _context.Emprestimo.FindAsync(id);
            if (emprestimo == null) return NotFound();

            ViewData["LivroId"] = new SelectList(_context.Livros, "Id", "Titulo", emprestimo.LivroId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", emprestimo.FuncionarioId);
            return View(emprestimo);
        }

        // POST: Emprestimos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Emprestimo emprestimo)
        {
            if (id != emprestimo.Id) return NotFound();

            // mesma proteção: se DataPrevistaDevolucao vier vazia, atribuir default
            var formValue = Request.Form["DataPrevistaDevolucao"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(formValue))
            {
                if (emprestimo.DataEmprestimo == default(DateTime))
                    emprestimo.DataEmprestimo = DateTime.Today;
                emprestimo.DataPrevistaDevolucao = emprestimo.DataEmprestimo.AddDays(7);
            }
            else
            {
                if (!DateTime.TryParse(formValue, out var parsed))
                    emprestimo.DataPrevistaDevolucao = emprestimo.DataEmprestimo.AddDays(7);
                else
                    emprestimo.DataPrevistaDevolucao = parsed.Date;
            }

            var formEmp = Request.Form["DataEmprestimo"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(formEmp) && emprestimo.DataEmprestimo == default(DateTime))
                emprestimo.DataEmprestimo = DateTime.Today;
            else if (!string.IsNullOrWhiteSpace(formEmp) && DateTime.TryParse(formEmp, out var dEmp))
                emprestimo.DataEmprestimo = dEmp.Date;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emprestimo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Emprestimo.Any(e => e.Id == emprestimo.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["LivroId"] = new SelectList(_context.Livros, "Id", "Titulo", emprestimo.LivroId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", emprestimo.FuncionarioId);
            return View(emprestimo);
        }

        // GET: Emprestimos/Delete/5
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
