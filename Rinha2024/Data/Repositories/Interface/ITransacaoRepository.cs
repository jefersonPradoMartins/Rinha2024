using Rinha2024.Entitdade;

namespace Rinha2024.Data.Repositories.Interface
{
    public interface ITransacaoRepository
    {
        Task CreateAsync(Transacao transacao);
        Task<IList<Transacao>> GetLastTenByClientIdAsync(int id);
    }
}
