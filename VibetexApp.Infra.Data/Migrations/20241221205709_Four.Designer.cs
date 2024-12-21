﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UsuariosApp.Infra.Data.Contexts;

#nullable disable

namespace VibetexApp.Infra.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241221205709_Four")]
    partial class Four
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VibetexApp.Domain.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("NOME");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("SENHA");

                    b.Property<int>("TipoPerfil")
                        .HasColumnType("int")
                        .HasColumnName("TIPO_PERFIL");

                    b.HasKey("Id");

                    b.ToTable("USUARIO", (string)null);
                });

            modelBuilder.Entity("VibetexApp.Domain.Entities.VibetexApp.Domain.Entities.Ponto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("FimExpediente")
                        .HasColumnType("datetime2")
                        .HasColumnName("FIM_EXPEDIENTE");

                    b.Property<TimeSpan>("HorasDevidas")
                        .HasColumnType("TIME")
                        .HasColumnName("HORAS_DEVIDAS");

                    b.Property<TimeSpan>("HorasExtras")
                        .HasColumnType("TIME")
                        .HasColumnName("HORAS_EXTRAS");

                    b.Property<TimeSpan>("HorasTrabalhadas")
                        .HasColumnType("TIME")
                        .HasColumnName("HORAS_TRABALHADAS");

                    b.Property<DateTime?>("InicioExpediente")
                        .HasColumnType("datetime2")
                        .HasColumnName("INICIO_EXPEDIENTE");

                    b.Property<DateTime?>("InicioPausa")
                        .HasColumnType("datetime2")
                        .HasColumnName("INICIO_PAUSA");

                    b.Property<string>("Latitude")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("LATITUDE");

                    b.Property<string>("Longitude")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("LONGITUDE");

                    b.Property<string>("Observacoes")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("OBSERVACOES");

                    b.Property<DateTime?>("RetornoPausa")
                        .HasColumnType("datetime2")
                        .HasColumnName("RETORNO_PAUSA");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("USUARIO_ID");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("PONTO", (string)null);
                });

            modelBuilder.Entity("VibetexApp.Domain.Entities.VibetexApp.Domain.Entities.Ponto", b =>
                {
                    b.HasOne("VibetexApp.Domain.Entities.Usuario", "Usuario")
                        .WithMany("Pontos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("VibetexApp.Domain.Entities.Usuario", b =>
                {
                    b.Navigation("Pontos");
                });
#pragma warning restore 612, 618
        }
    }
}