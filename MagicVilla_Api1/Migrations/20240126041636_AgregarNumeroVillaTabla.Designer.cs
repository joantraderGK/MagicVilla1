﻿// <auto-generated />
using System;
using MagicVilla_Api1.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla_Api1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240126041636_AgregarNumeroVillaTabla")]
    partial class AgregarNumeroVillaTabla
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_Api1.Modelos.NumeroVilla", b =>
                {
                    b.Property<int>("VillaNo")
                        .HasColumnType("int");

                    b.Property<string>("DealleEspecial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("VillaId")
                        .HasColumnType("int");

                    b.HasKey("VillaNo");

                    b.HasIndex("VillaId");

                    b.ToTable("numerovillas");
                });

            modelBuilder.Entity("MagicVilla_Api1.Modelos.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActulizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagenUr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MetroCuadrado")
                        .HasColumnType("float");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocupante")
                        .HasColumnType("int");

                    b.Property<int>("Tarifa")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenidad = "",
                            Detalle = "Detalle de la Villa...",
                            FechaActulizacion = new DateTime(2024, 1, 26, 0, 16, 36, 604, DateTimeKind.Local).AddTicks(7827),
                            FechaCreacion = new DateTime(2024, 1, 26, 0, 16, 36, 604, DateTimeKind.Local).AddTicks(7815),
                            ImagenUr = "",
                            MetroCuadrado = 50.0,
                            Nombre = "Villa Real",
                            Ocupante = 5,
                            Tarifa = 200
                        });
                });

            modelBuilder.Entity("MagicVilla_Api1.Modelos.NumeroVilla", b =>
                {
                    b.HasOne("MagicVilla_Api1.Modelos.Villa", "Villa")
                        .WithMany()
                        .HasForeignKey("VillaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Villa");
                });
#pragma warning restore 612, 618
        }
    }
}
