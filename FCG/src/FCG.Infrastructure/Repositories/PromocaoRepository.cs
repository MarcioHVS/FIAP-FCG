﻿using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Repositories
{
    public class PromocaoRepository : EFRepository<Promocao>, IPromocaoRepository
    {
        private readonly ApplicationDbContext _context;

        public PromocaoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Promocao?> ObterPromocaoPorCupomAsync(string cupom)
        {
            return await _context.Promocoes
                .FirstOrDefaultAsync(p => p.Cupom.Equals(cupom) && p.DataValidade >= DateTime.Now);
        }

        public override async Task<IEnumerable<Promocao>> ObterTodosAsync()
        {
            return await _context.Promocoes.AsNoTracking()
                .Where(p => p.DataValidade >= DateTime.Now).ToListAsync();
        }
    }
}
