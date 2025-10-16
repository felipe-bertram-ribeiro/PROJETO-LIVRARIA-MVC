using Livraria.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ==============================================
// Configuração de serviços (Service Container)
// ==============================================

// Adiciona o Entity Framework Core e conecta ao SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona o suporte ao padrão MVC (Controllers e Views)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ==============================================
// Configuração do pipeline de requisições HTTP
// ==============================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Página de erro amigável em produção
    app.UseHsts(); // Força HTTPS
}

app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS
app.UseStaticFiles();      // Habilita uso de CSS, JS e imagens em wwwroot

app.UseRouting();          // Habilita roteamento (MVC)
app.UseAuthorization();    // Habilita controle de acesso (autenticação/roles, futuramente)

// ==============================================
// Configuração das rotas padrão
// ==============================================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
