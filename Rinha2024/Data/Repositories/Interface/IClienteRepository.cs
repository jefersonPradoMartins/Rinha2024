using Rinha2024.Entitdade;
using System.Drawing;

namespace Rinha2024.Data.Repositories.Interface
{
    public interface IClienteRepository
    {
        Task CreateAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task<Cliente> GetByIdAsync(int id);
    }
}
