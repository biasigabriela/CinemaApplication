using CinemaApplication.Dominio;
using CinemaApplication.Infraestrutura;
using CinemaApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CinemaApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessaoController : ControllerBase
    {
        private readonly SessaoRepositorio _sessaoRepositorio;
        private readonly ILogger<SessaoController> _logger;

        public SessaoController(SessaoRepositorio sessaoRepositorio, ILogger<SessaoController> logger)
        {
            _logger = logger;
            _sessaoRepositorio = sessaoRepositorio;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarAsync([FromBody] NovaSessaoInputModel sessaoInputModel, CancellationToken cancellationToken)
        {
            var horario = Horario.Criar(sessaoInputModel.Horario);
            var dia = DateTime.Parse(sessaoInputModel.Dia);
            var filmeExibicao = Guid.Parse(sessaoInputModel.FilmeExibicao);

            var sessao = Sessao.Criar(dia, horario.Value, sessaoInputModel.QuantidadeLugares, sessaoInputModel.TotalOcupado, 
                                    sessaoInputModel.Preco, filmeExibicao);
            if (sessao.IsFailure)
            {
                _logger.LogError("Erro ao criar sessão");
                return BadRequest(sessao.Error);
            }           
            
            await _sessaoRepositorio.InserirAsync(sessao.Value, cancellationToken);
            await _sessaoRepositorio.CommitAsync(cancellationToken);
            _logger.LogInformation("Sessão {sessao} criada", sessao.Value.Id);
            return CreatedAtAction("RecuperarPorId", new { id = sessao.Value.Id }, sessao.Value.Id);
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var sessao = await _sessaoRepositorio.RecuperarPorIdAsync(id, cancellationToken);
            if (sessao == null)
            {
                return NotFound();
            }
            return Ok(sessao);
        }

        [HttpGet]
        public async Task<IActionResult> RecuperarPorFilmeData([FromBody] BuscarSessaoFilmeDataInputModel buscarSessaoFilmeDataInputModel, CancellationToken cancellationToken)
        {
            var dia = DateTime.Parse(buscarSessaoFilmeDataInputModel.Data);
            var id = Guid.Parse(buscarSessaoFilmeDataInputModel.Id);
            var sessao = await _sessaoRepositorio.RecuperarPorFilmeData(id, dia);
            var ListaSessoes = sessao.ToList();
            return Ok(ListaSessoes);
        }

        [HttpPut]
        public async Task<IActionResult> Alterarsessao([FromBody] AlterarSessaoInputModel alterarSessaoInputModel, CancellationToken cancellationToken)
        {
            var horario = Horario.Criar(alterarSessaoInputModel.Horario);
            var id = Guid.Parse(alterarSessaoInputModel.Id);
            var dia = DateTime.Parse(alterarSessaoInputModel.Dia);

            var sessao = await _sessaoRepositorio.RecuperarPorIdAsync(id, cancellationToken);
            if (sessao == null)
                return NotFound();

            sessao.Alterar(dia, horario.Value, alterarSessaoInputModel.QuantidadeLugares, alterarSessaoInputModel.TotalOcupado, alterarSessaoInputModel.Preco);
            _sessaoRepositorio.Alterar(sessao);
            await _sessaoRepositorio.CommitAsync(cancellationToken);
            _logger.LogInformation("Sessão {sessao} alterada", sessao.Id);
            return Ok(sessao);
        }

        

        [HttpPut("{id}/{quantidadeIngressos}")]
        public async Task<IActionResult> VenderIngressos(Guid id, int quantidadeIngressos, CancellationToken cancellationToken)
        {
           
            var sessao = await _sessaoRepositorio.RecuperarPorIdAsync(id, cancellationToken);
            if (sessao == null)
            {
                return NotFound();
            }


            sessao.AlterarQuantidadeOcupada(quantidadeIngressos, out string erro);
            if (string.IsNullOrEmpty(erro))
            {
                _sessaoRepositorio.Alterar(sessao);
                await _sessaoRepositorio.CommitAsync(cancellationToken);
                _logger.LogInformation("Ingressos vendidos {quantidade} para a {sessao}", quantidadeIngressos, sessao.Id);
                return Ok(sessao);
            }
            else
            {
                return BadRequest(erro);
            }

            
        }
    }
}
