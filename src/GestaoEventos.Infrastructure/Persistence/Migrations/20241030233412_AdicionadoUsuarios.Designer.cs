﻿// <auto-generated />
using System;
using GestaoEventos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GestaoEventos.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241030233412_AdicionadoUsuarios")]
    partial class AdicionadoUsuarios
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GestaoEventos.Domain.Eventos.Evento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.HasKey("Id");

                    b.ToTable("eventos", (string)null);
                });

            modelBuilder.Entity("GestaoEventos.Domain.Usuarios.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("nome");

                    b.Property<int>("Permissao")
                        .HasColumnType("integer")
                        .HasColumnName("permissao");

                    b.HasKey("Id");

                    b.ToTable("usuarios", (string)null);
                });

            modelBuilder.Entity("GestaoEventos.Domain.Eventos.Evento", b =>
                {
                    b.OwnsOne("GestaoEventos.Domain.Eventos.DetalhesEvento", "Detalhes", b1 =>
                        {
                            b1.Property<Guid>("EventoId")
                                .HasColumnType("uuid");

                            b1.Property<int>("CapacidadeMaxima")
                                .HasColumnType("integer")
                                .HasColumnName("capacidade_maxima");

                            b1.Property<DateTime>("DataHoraFim")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("data_hora_fim");

                            b1.Property<DateTime>("DataHoraInicio")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("data_hora_inico");

                            b1.Property<string>("Localizacao")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("character varying(500)")
                                .HasColumnName("localizacao");

                            b1.Property<string>("Nome")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("character varying(200)")
                                .HasColumnName("nome");

                            b1.Property<int>("Status")
                                .HasColumnType("integer")
                                .HasColumnName("status");

                            b1.HasKey("EventoId");

                            b1.ToTable("eventos");

                            b1.WithOwner()
                                .HasForeignKey("EventoId");
                        });

                    b.OwnsMany("GestaoEventos.Domain.Eventos.Ingresso", "Ingressos", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Preco")
                                .HasPrecision(8, 2)
                                .HasColumnType("numeric(8,2)")
                                .HasColumnName("preco");

                            b1.Property<int>("Quantidade")
                                .HasColumnType("integer")
                                .HasColumnName("quantidade");

                            b1.Property<Guid>("evento_id")
                                .HasColumnType("uuid");

                            b1.HasKey("Id");

                            b1.HasIndex("evento_id");

                            b1.ToTable("eventos_ingressos", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("evento_id");

                            b1.OwnsOne("GestaoEventos.Domain.Eventos.TipoIngresso", "Tipo", b2 =>
                                {
                                    b2.Property<Guid>("IngressoId")
                                        .HasColumnType("uuid");

                                    b2.Property<string>("Descricao")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)")
                                        .HasColumnName("descricao");

                                    b2.Property<string>("Nome")
                                        .IsRequired()
                                        .HasMaxLength(200)
                                        .HasColumnType("character varying(200)")
                                        .HasColumnName("nome");

                                    b2.HasKey("IngressoId");

                                    b2.ToTable("eventos_ingressos");

                                    b2.WithOwner()
                                        .HasForeignKey("IngressoId");
                                });

                            b1.Navigation("Tipo")
                                .IsRequired();
                        });

                    b.OwnsMany("GestaoEventos.Domain.Eventos.Sessao", "Sessoes", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<DateTime>("DataHoraFim")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("data_hora_fim");

                            b1.Property<DateTime>("DataHoraInicio")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("data_hora_inico");

                            b1.Property<string>("Nome")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("character varying(200)")
                                .HasColumnName("nome");

                            b1.Property<Guid>("evento_id")
                                .HasColumnType("uuid");

                            b1.HasKey("Id");

                            b1.HasIndex("evento_id");

                            b1.ToTable("eventos_sessoes", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("evento_id");
                        });

                    b.Navigation("Detalhes")
                        .IsRequired();

                    b.Navigation("Ingressos");

                    b.Navigation("Sessoes");
                });
#pragma warning restore 612, 618
        }
    }
}
