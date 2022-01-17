using CinemaApplication.Dominio;
using CinemaApplication.Models;
using CinemaApplication.WebApi.Infraestrutura;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CinemaApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly ILogger<FilmesController> _logger;
        private readonly FilmesRepositorio _filmesRepositorio;

        public FilmesController(
            ILogger<FilmesController> logger,
            FilmesRepositorio filmesRepositorio)
        {
            _logger = logger;
            _filmesRepositorio = filmesRepositorio;
        }

        [HttpPost]
        public async Task<IActionResult> IncluirAsync([FromBody] NovoFilmeInputModel filmeinputModel, CancellationToken cancellationToken)
        {
            var horario = Horario.Criar(filmeinputModel.Duracao);

            var filme = Filme.Criar(filmeinputModel.Titulo, horario.Value, filmeinputModel.Sinopse);
            if (filme.IsFailure)
            {
                _logger.LogError("Erro ao criar filme");
                return BadRequest(filme.Error);
            }

            _logger.LogInformation("Filme {filme} criado em memória", filme.Value.Id);

            await _filmesRepositorio.InserirAsync(filme.Value, cancellationToken);
            await _filmesRepositorio.CommitAsync(cancellationToken);
            return CreatedAtAction(nameof(RecuperarPorId), new { id = filme.Value.Id }, filme.Value.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarPorId(Guid id, CancellationToken cancellationToken)
        {
          
            var filme = await _filmesRepositorio.RecuperarPorIdAsync(id, cancellationToken);
            if (filme == null)
                return NotFound();
            return Ok(filme);
        }

        [HttpGet()]
        public async Task<IActionResult> RecuperarTodosAsync(CancellationToken cancellationToken)
        {
            var filme = await _filmesRepositorio.RecuperarTodosAsync();

            if (filme == null)
                return NotFound();

            return Ok(filme);
        }

        [HttpPut]
        public async Task<IActionResult> AlterarFilme([FromBody] AlterarFilmeInputModel alterarFilmeInputModel, CancellationToken cancellationToken)
        {
            var horario = Horario.Criar(alterarFilmeInputModel.Duracao);
            var id = Guid.Parse(alterarFilmeInputModel.Id);

            var filme = await _filmesRepositorio.RecuperarPorIdAsync(id, cancellationToken);
            if (filme == null)
            {
                return NotFound();
            }
            filme.Alterar(alterarFilmeInputModel.Titulo, horario.Value, alterarFilmeInputModel.Sinopse);
            _filmesRepositorio.Alterar(filme);
            await _filmesRepositorio.CommitAsync(cancellationToken);
            _logger.LogInformation("Filme {filme} alterado", filme.Id);
            return Ok(filme);
        }

    }
}
