using CinemaApplication.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApplication.Infraestrutura.EntityConfiuguration
{
    public sealed class FilmeTypeConfiguration : IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder){
            
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Titulo).HasColumnType("varchar(100)");
            builder.Property(c => c.Duracao).HasConversion(EFConversores.HorarioConverter).HasColumnType("varchar(5)"); ;
            builder.Property(c => c.Sinopse).HasColumnType("varchar(1000)");

            

        }
    }
}
