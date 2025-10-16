using Livraria.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Estatísticas
            ViewBag.TotalClientes = await _context.Clientes.CountAsync();
            ViewBag.TotalFuncionarios = await _context.Funcionarios.CountAsync();
            ViewBag.TotalLivros = await _context.Livros.CountAsync();
            ViewBag.EmprestimosAndamento = await _context.Emprestimo.CountAsync(e => e.Status == "Em andamento");

            // Últimos registros
            ViewBag.UltimosClientes = await _context.Clientes
                .OrderByDescending(c => c.Id)
                .Take(5)
                .ToListAsync();

            ViewBag.UltimosEmprestimos = await _context.Emprestimo
                .Include(e => e.Cliente)
                .Include(e => e.Livro)
                .OrderByDescending(e => e.Id)
                .Take(5)
                .ToListAsync();

            ViewBag.UltimosLivros = await _context.Livros
                .OrderByDescending(l => l.Id)
                .Take(5)
                .ToListAsync();

            return View();
        }
    }
}
