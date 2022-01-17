using CinemaApplication.Dominio;
using CinemaApplication.Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CinemaApplication.WebApi.Infraestrutura
{
    public class FilmesRepositorio
    {
        private readonly CinemaDbContext _dbContext;
        public FilmesRepositorio(CinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task InserirAsync(Filme novoFilme, CancellationToken cancellationToken = default)
        {
            await _dbContext.Filme.AddAsync(novoFilme, cancellationToken);
        }
        public void Alterar(Filme filme)
        {
            
        }
        public async Task<Filme>RecuperarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                            .Filme
                            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
        public async Task<IEnumerable<Filme>> RecuperarTodosAsync()
        {
            return await _dbContext
                            .Filme
                            .AsQueryable().ToListAsync();
        }
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
