using CinemaApplication.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApplication.Infraestrutura.EntityConfiuguration
{
    public sealed class sessaoTypeConfiguration : IEntityTypeConfiguration<Sessao>
    {
        public void Configure(EntityTypeBuilder<Sessao> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Dia).HasColumnType("date");
            builder.Property(c => c.Horario).HasConversion(EFConversores.HorarioConverter).HasColumnType("varchar(5)"); ;
            builder.Property(c => c.QuantidadeLugares).HasColumnType("integer");
            builder.Property(c => c.TotalOcupado).HasColumnType("integer");
            builder.Property(c => c.Preco).HasColumnType("float");
            builder.Property(c => c.FilmeExibicao);
            builder.Property("_hashConcorrencia").HasColumnName("Hash").HasConversion<string>().IsConcurrencyToken();


        }
    }
}
