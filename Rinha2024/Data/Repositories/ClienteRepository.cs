using Rinha2024.Data.Repositories.Interface;
using Rinha2024.Entitdade;

namespace Rinha2024.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly Context.RinhaContext _context;
        public ClienteRepository(Context.RinhaContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Cliente cliente)
        {
            await _context.AddAsync(cliente);
            _context.SaveChanges();
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            return await _context.Cliente.FindAsync(id);

        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Update(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
