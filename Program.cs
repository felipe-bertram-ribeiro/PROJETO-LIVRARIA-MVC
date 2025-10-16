using Livraria.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ==============================================
// Configura��o de servi�os (Service Container)
// ==============================================

// Adiciona o Entity Framework Core e conecta ao SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona o suporte ao padr�o MVC (Controllers e Views)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ==============================================
// Configura��o do pipeline de requisi��es HTTP
// ==============================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // P�gina de erro amig�vel em produ��o
    app.UseHsts(); // For�a HTTPS
}

app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS
app.UseStaticFiles();      // Habilita uso de CSS, JS e imagens em wwwroot

app.UseRouting();          // Habilita roteamento (MVC)
app.UseAuthorization();    // Habilita controle de acesso (autentica��o/roles, futuramente)

// ==============================================
// Configura��o das rotas padr�o
// ==============================================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
