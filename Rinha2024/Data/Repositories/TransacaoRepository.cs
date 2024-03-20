using Microsoft.EntityFrameworkCore;
using Rinha2024.Data.Repositories.Interface;
using Rinha2024.Entitdade;

namespace Rinha2024.Data.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly Context.RinhaContext _context;

        public TransacaoRepository(Context.RinhaContext context)
        {
            _context = context;
        }


        public async Task CreateAsync(Transacao transacao)
        {
            await _context.AddAsync(transacao);
            _context.SaveChanges();
        }

        public async Task<IList<Transacao>> GetLastTenByClientIdAsync(int id)
        {
            return _context.Transacao.AsNoTracking().Where(x => x.Cliente.Id == id).OrderByDescending(x => x.RealizadaEm).Take(10).ToList();
        }
    }
}
