﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace VeterinarskaStanica.Migrations
{
    [DbContext(typeof(VetStanicaContext))]
    [Migration("20220313175707_V2")]
    partial class V2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Models.Pregled", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Mesec")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("ID");

                    b.ToTable("Pregledi");
                });

            modelBuilder.Entity("Models.Veterinar", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BrojTelefona")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Veterinari");
                });

            modelBuilder.Entity("Models.Veza", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Lek")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("PregledID")
                        .HasColumnType("int");

                    b.Property<int?>("VeterinarID")
                        .HasColumnType("int");

                    b.Property<int?>("ZivotinjaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PregledID");

                    b.HasIndex("VeterinarID");

                    b.HasIndex("ZivotinjaID");

                    b.ToTable("ZivotinjeVeterinari");
                });

            modelBuilder.Entity("Models.Zivotinja", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BrojKartona")
                        .HasColumnType("int");

                    b.Property<string>("ImeVlasnika")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ImeZivotinje")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VrstaZivotinje")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Zivotinje");
                });

            modelBuilder.Entity("Models.Veza", b =>
                {
                    b.HasOne("Models.Pregled", "Pregled")
                        .WithMany("ZivotinjeVeterinari")
                        .HasForeignKey("PregledID");

                    b.HasOne("Models.Veterinar", "Veterinar")
                        .WithMany("VeterinarZivotinja")
                        .HasForeignKey("VeterinarID");

                    b.HasOne("Models.Zivotinja", "Zivotinja")
                        .WithMany("ZivotinjaVeterinar")
                        .HasForeignKey("ZivotinjaID");

                    b.Navigation("Pregled");

                    b.Navigation("Veterinar");

                    b.Navigation("Zivotinja");
                });

            modelBuilder.Entity("Models.Pregled", b =>
                {
                    b.Navigation("ZivotinjeVeterinari");
                });

            modelBuilder.Entity("Models.Veterinar", b =>
                {
                    b.Navigation("VeterinarZivotinja");
                });

            modelBuilder.Entity("Models.Zivotinja", b =>
                {
                    b.Navigation("ZivotinjaVeterinar");
                });
#pragma warning restore 612, 618
        }
    }
}