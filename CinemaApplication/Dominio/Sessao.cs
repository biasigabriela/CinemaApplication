using CinemaApplication.WebApi.Controllers;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplication.Dominio
{
    public class Sessao
    {
        private string _hashConcorrencia;
        private Sessao() { }
        private Sessao(Guid id, DateTime dia, Horario horario, int quantidadeLugares, int totalOcupado, double preco, Guid filmeExibicao, string Hash)
        {
            Id = id;
            Dia = dia;
            Horario = horario;
            QuantidadeLugares = quantidadeLugares;
            TotalOcupado = totalOcupado;
            Preco = preco;
            FilmeExibicao = filmeExibicao;
            _hashConcorrencia = Hash;

        }

        public Guid Id { get; }
        public DateTime Dia { get; private set; }
        public Horario Horario { get; private set; }
        public int QuantidadeLugares { get; private set; }
        public int TotalOcupado { get;  private set; }
        public double Preco { get; private set; }
        public Guid FilmeExibicao { get; }

        public void AtualizarVendas(int quantidadeIngressos)
        {
            TotalOcupado = TotalOcupado + quantidadeIngressos;
            AtualizarHashConcorrencia();
        }

        public static Result<Sessao> Criar(DateTime dia, Horario horario, int quantidadeLugares, int totalOcupado, double preco, Guid filmeExibicao)
        {
            if (filmeExibicao == null)
                return Result.Failure<Sessao>("Adicione um filme a seção");
            if (quantidadeLugares <= 0)
                return Result.Failure<Sessao>("Quantidade de vagas deve ser maior que 0");

            var secao = new Sessao(Guid.NewGuid(), dia, horario, quantidadeLugares, totalOcupado, preco, filmeExibicao, "");
            secao.AtualizarHashConcorrencia();
            return secao;
        }

        public string AlterarQuantidadeOcupada(int quantidadeIngressos, out string erro)
        {
            if (QuantidadeLugares - TotalOcupado < quantidadeIngressos)
            {
                erro = "Quantidade de ingressos maior que a quantidade disponível";
                return erro;
            }

            AtualizarVendas(quantidadeIngressos);
            erro = null;
            return erro;
        }

        private void AtualizarHashConcorrencia()
        {
            using var hash = MD5.Create();
            var data = hash.ComputeHash(
                Encoding.UTF8.GetBytes(
                    $"{Id}{Dia}{Horario}{QuantidadeLugares}{TotalOcupado}{Preco}{FilmeExibicao}"));
            var sBuilder = new StringBuilder();
            foreach (var tbyte in data)
                sBuilder.Append(tbyte.ToString("x2"));
            _hashConcorrencia = sBuilder.ToString();
        }
        public void Alterar(DateTime dia, Horario horario, int quantidadeLugares, int totalOcupado, double preco)
        {
            //if (totalOcupado > quantidadeLugares)
            //    return Result.Failure<Secao>("Voce nao pode diminuir a quantidade de lugares para menos que o total de ingressos vendidos.");
            //if (string.IsNullOrEmpty(sinopse))
            //    return Result.Failure<Filme>("O filme deve ter uma sinopse.");

            Dia = dia;
            Horario = horario;
            QuantidadeLugares = quantidadeLugares;
            TotalOcupado = totalOcupado;
            Preco = preco;

        }
    }
}
