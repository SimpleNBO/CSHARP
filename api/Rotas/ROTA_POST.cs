using Microsoft.EntityFrameworkCore;
using apiCSHARP.apiAluguel.Models;

public static class Rota_POST
{
    public static void MapPostRoutes(this WebApplication app)
    {
        app.MapPost("/api/alugueis", async (Pessoa pessoa, Locatarios Dados, portifolio portifolioDados) =>
        {

            var imovel = await portifolioDados.Imoveis.FindAsync(pessoa.IdImovel);
            if (imovel == null)
            {
                return Results.BadRequest(new { erro = true, message = "O imóvel especificado não existe." });
            }

            var imovelJaAssociado = await Dados.Locacoes.AnyAsync(p => p.IdImovel == pessoa.IdImovel);
            if (imovelJaAssociado)
            {
                return Results.BadRequest(new { erro = true, message = "O imóvel já está associado a outra pessoa." });
            }

            Dados.Locacoes.Add(pessoa);
            await Dados.SaveChangesAsync();
            return Results.Created($"/api/alugueis/{pessoa.Id}", pessoa);
        });

        app.MapPost("/api/imoveis", async (Imovel imovel, portifolio Dados) =>
        {
            Dados.Imoveis.Add(imovel);
            await Dados.SaveChangesAsync();
            return Results.Created($"/api/imoveis/{imovel.Id}", imovel);
        });
    }
}
