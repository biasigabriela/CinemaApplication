using CinemaApplication.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CinemaApplication.Infraestrutura
{
    public sealed class SessaoRepositorio
    {
        private readonly CinemaDbContext _cinemaDbContext;
        private readonly IConfiguration _configuracao;


        public SessaoRepositorio(CinemaDbContext cinemaDbContext, IConfiguration configuracao)
        {
            _cinemaDbContext = cinemaDbContext;
            _configuracao = configuracao;
        }
        public void Alterar(Sessao sessao)
        {

        }
        public async Task InserirAsync(Sessao sessao, CancellationToken cancellationToken = default)
        {
            await _cinemaDbContext.AddAsync(sessao, cancellationToken);
        }


        public async Task<Sessao> RecuperarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _cinemaDbContext
                .sessao
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
        public async Task<IEnumerable<Sessao>>RecuperarPorFilmeData(Guid filme, DateTime data)
        {   
            return await _cinemaDbContext
                .sessao
                .AsQueryable().Where(c => c.FilmeExibicao == filme && c.Dia == data).ToListAsync();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _cinemaDbContext.SaveChangesAsync(cancellationToken);
        }
    }

}
