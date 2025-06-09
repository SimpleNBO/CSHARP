using Microsoft.EntityFrameworkCore;

using apiCSHARP.apiAluguel.Models;

public static class Rota_DELETE
{
    public static void MapDeleteRoutes(this WebApplication app)
    {
        app.MapDelete("/api/alugueis/{id}", async (int id, Locatarios Dados) =>
        {
            var locacao = await Dados.Locacoes.FindAsync(id);
            if (locacao is null) return Results.NotFound("Locação não encontrada.");

            Dados.Locacoes.Remove(locacao);

            await Dados.SaveChangesAsync();
            return Results.Ok("Locação excluída com sucesso.");
        });

        app.MapDelete("/api/imoveis/{id}", async (int id, portifolio Dados, Locatarios locatarios) =>
        {
            var imovel = await Dados.Imoveis.FindAsync(id);
            if (imovel is null) return Results.NotFound("Imóvel não encontrado.");

            var locacao = await locatarios.Locacoes.FirstOrDefaultAsync(l => l.IdImovel == id);
            if (locacao != null)
            {
                return Results.Conflict(new { erro = true,  message = "Não é possível excluir o imóvel porque ele está alugado." });
            }

            Dados.Imoveis.Remove(imovel);

            await Dados.SaveChangesAsync();
            return Results.Ok("Imóvel excluído com sucesso.");
        });
    }
}
