using Microsoft.EntityFrameworkCore;
using apiCSHARP.apiAluguel.Models;

public static class Rota_PUT
{
    public static void MapPutRoutes(this WebApplication app)
    {
        app.MapPut("/api/alugueis/{id}", async (int id, Pessoa pessoa, Locatarios Dados, portifolio portifolioDados) =>
        {

            var imovel = await portifolioDados.Imoveis.FindAsync(pessoa.IdImovel);
            if (imovel == null)
            {
                return Results.BadRequest(new { erro = true, mensagem = "O imóvel especificado não existe." });
            }

            Dados.Locacoes.Update(pessoa);
            await Dados.SaveChangesAsync();
            return Results.Ok(pessoa);
        });

        app.MapPut("/api/imoveis/{id}", async (Imovel imovel, portifolio Dados) =>
        {
            Dados.Imoveis.Update(imovel);
            await Dados.SaveChangesAsync();
            return Results.Ok(imovel);
        });
    }
}
