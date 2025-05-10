using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Repositories
{
    public class PedidoRepository : EFRepository<Pedido>, IPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Pedido?> ObterPorIdAsync(Guid id)
        {
            return await _context.Pedidos.AsNoTracking()
                .Include(p => p.Usuario)
                .Include(p => p.Jogo)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Pedido>> ObterPedidosAsync(Guid usuarioId)
        {
            return await _context.Pedidos.AsNoTracking()
                .Include(p => p.Usuario)
                .Include(p => p.Jogo)
                .Where(e => usuarioId.Equals(Guid.Empty) || e.UsuarioId.Equals(usuarioId)).ToListAsync();
        }

        public async Task<bool> ExistePedidoAsync(Pedido pedido)
        {
            return await _context.Pedidos
                .AnyAsync(p => p.Id != pedido.Id && p.UsuarioId == pedido.UsuarioId && p.JogoId == pedido.JogoId);
        }
    }
}
