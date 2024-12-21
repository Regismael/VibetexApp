using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using VibetexApp.Domain.Entities;
using VibetexApp.Domain.Entities.VibetexApp.Domain.Entities;

namespace VibetexApp.Infra.Data.Mappings
{
    public class PontoMap : IEntityTypeConfiguration<Ponto>
    {
        public void Configure(EntityTypeBuilder<Ponto> builder)
        {
            builder.ToTable("PONTO");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.InicioExpediente)
                .HasColumnName("INICIO_EXPEDIENTE");

            builder.Property(p => p.FimExpediente)
                .HasColumnName("FIM_EXPEDIENTE");

            builder.Property(p => p.InicioPausa)
                .HasColumnName("INICIO_PAUSA");

            builder.Property(p => p.RetornoPausa)
                .HasColumnName("RETORNO_PAUSA");

            builder.Property(p => p.HorasTrabalhadas)
                .HasColumnName("HORAS_TRABALHADAS")
                .HasColumnType("TIME") 
                .IsRequired();

            builder.Property(p => p.HorasExtras)
                .HasColumnName("HORAS_EXTRAS")
                .HasColumnType("TIME")
                .IsRequired();

            builder.Property(p => p.HorasDevidas)
                .HasColumnName("HORAS_DEVIDAS")
                .HasColumnType("TIME")
                .IsRequired();

            builder.Property(p => p.Observacoes)
                .HasColumnName("OBSERVACOES")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(p => p.Latitude)
                .HasColumnName("LATITUDE")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Longitude)
                .HasColumnName("LONGITUDE")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.UsuarioId)
                .HasColumnName("USUARIO_ID")
                .IsRequired();

            builder.HasOne(p => p.Usuario)
                .WithMany(u => u.Pontos)
                .HasForeignKey(p => p.UsuarioId);
        }
    }
}
