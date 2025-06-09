using Microsoft.EntityFrameworkCore;
using apiCSHARP.apiAluguel.Models;
using apiCSHARP.apiAluguel.Rotas;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Locatarios>(options => options.UseSqlite("Data Source=locacoes.db"));
builder.Services.AddDbContext<portifolio>(options => options.UseSqlite("Data Source=imoveis.db"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("AllowAll");

app.MapGetRoutes();
app.MapPostRoutes();
app.MapDeleteRoutes();
app.MapPutRoutes();

PopularBancoDeDados(app);

app.Run();

void PopularBancoDeDados(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var locatariosContext = scope.ServiceProvider.GetRequiredService<Locatarios>();
    var portifolioContext = scope.ServiceProvider.GetRequiredService<portifolio>();

    locatariosContext.Database.Migrate();
    portifolioContext.Database.Migrate();
    
    if (!locatariosContext.Locacoes.Any())
    {
        var locacoesIniciais = new List<Pessoa>
        {
            new() { Id = 1, Nome = "João", Sobrenome = "Silva", CPF = "123.456.789-00", IdImovel = 3 },
            new() { Id = 2, Nome = "Maria", Sobrenome = "Oliveira", CPF = "987.654.321-00", IdImovel = 5 },
            new() { Id = 3, Nome = "Pedro", Sobrenome = "Santos", CPF = "456.789.123-00", IdImovel = 1 },
            new() { Id = 4, Nome = "Ana", Sobrenome = "Costa", CPF = "321.654.987-00", IdImovel = 7 },
            new() { Id = 5, Nome = "Lucas", Sobrenome = "Pereira", CPF = "654.321.987-00", IdImovel = 2 },
            new() { Id = 6, Nome = "Carla", Sobrenome = "Rocha", CPF = "789.123.456-00", IdImovel = 9 },
            new() { Id = 7, Nome = "Rafael", Sobrenome = "Fernandes", CPF = "147.258.369-00", IdImovel = 4 },
            new() { Id = 8, Nome = "Beatriz", Sobrenome = "Alves", CPF = "963.852.741-00", IdImovel = 6 },
            new() { Id = 9, Nome = "Gustavo", Sobrenome = "Martins", CPF = "258.369.147-00", IdImovel = 10 },
            new() { Id = 10, Nome = "Fernanda", Sobrenome = "Barbosa", CPF = "369.147.258-00", IdImovel = 8 },
        };

        locatariosContext.Locacoes.AddRange(locacoesIniciais);
        locatariosContext.SaveChanges();
    }

    if (!portifolioContext.Imoveis.Any())
    {
        var imoveisIniciais = new List<Imovel>
        {
            new() { Id = 1, Endereco = "Rua A, 123", ValorAluguel = 1500, Metragem = 50 },
            new() { Id = 2, Endereco = "Rua B, 456", ValorAluguel = 2000, Metragem = 100 },
            new() { Id = 3, Endereco = "Rua C, 789", ValorAluguel = 3000, Metragem = 150 },
            new() { Id = 4, Endereco = "Avenida D, 101", ValorAluguel = 1800, Metragem = 70 },
            new() { Id = 5, Endereco = "Travessa E, 202", ValorAluguel = 2200, Metragem = 90 },
            new() { Id = 6, Endereco = "Praça F, 303", ValorAluguel = 2500, Metragem = 120 },
            new() { Id = 7, Endereco = "Rua G, 404", ValorAluguel = 1600, Metragem = 55 },
            new() { Id = 8, Endereco = "Avenida H, 505", ValorAluguel = 2700, Metragem = 130 },
            new() { Id = 9, Endereco = "Rua I, 606", ValorAluguel = 3200, Metragem = 170 },
            new() { Id = 10, Endereco = "Travessa J, 707", ValorAluguel = 1900, Metragem = 65 },
        };

        portifolioContext.Imoveis.AddRange(imoveisIniciais);
        portifolioContext.SaveChanges();
    }
}