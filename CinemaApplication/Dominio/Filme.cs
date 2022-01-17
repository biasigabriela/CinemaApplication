using CSharpFunctionalExtensions;
using System;

namespace CinemaApplication.Dominio
{
    public class Filme
    {
        private Filme() { }

        private Filme(Guid id, string titulo, Horario duracao, string sinopse)
        {
            Id = id;
            Titulo = titulo;
            Duracao = duracao;
            Sinopse = sinopse;
        }

        public Guid Id { get; }
        public string Titulo { get; private set; }
        public Horario Duracao { get; private set; }
        public string Sinopse { get; private set; }

        public static Result<Filme> Criar(string titulo, Horario duracao, string sinopse)
        {
            if (string.IsNullOrEmpty(titulo))
                return Result.Failure<Filme>("Titulo deve ser preenchido");            
            return new Filme(Guid.NewGuid(), titulo, duracao, sinopse);
        }
        public void Alterar(string titulo, Horario duracao, string sinopse)
        {
            //if (duracao.Hora == 0 && duracao.Minuto == 0) 
            //    return Result.Failure<Filme>("O filme deve ter uma duração.");
            //if (string.IsNullOrEmpty(sinopse))
            //    return Result.Failure<Filme>("O filme deve ter uma sinopse.");
            Titulo = titulo;
            Duracao = duracao;
            Sinopse = sinopse;
        }
    }
}
