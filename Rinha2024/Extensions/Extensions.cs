using Rinha2024.Controllers;
using Rinha2024.Entitdade;
using static Rinha2024.Controllers.ClientesController;

namespace Rinha2024.Extensions
{
    public static class TransacaoExtension
    {
        public static IList<UltimasTransacoes> ToUltimasTransacoes(IList<Transacao> transacoes)
        {
            var ultimasTransacoes = new List<UltimasTransacoes>();
            foreach (var transacao in transacoes)
            {
                ultimasTransacoes.Add(
                    new UltimasTransacoes
                    {
                        Descricao = transacao.Descricao,
                        RealizadaEm = transacao.RealizadaEm,
                        Tipo = transacao.Tipo,
                        Valor = transacao.Valor,
                    });
            }
            return ultimasTransacoes;
        }

    }
}
