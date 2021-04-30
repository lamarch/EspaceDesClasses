﻿// <auto-generated />
using System;
using MaClassePA.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MaClassePA.Migrations
{
    [DbContext(typeof(ClassesDbContext))]
    [Migration("20210430212255_RessourceRequiredField")]
    partial class RessourceRequiredField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MaClassePA.Models.ClasseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("EstSupprime")
                        .HasColumnType("bit");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("MaClassePA.Models.MatiereModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClasseId")
                        .HasColumnType("int");

                    b.Property<bool>("EstSupprime")
                        .HasColumnType("bit");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClasseId");

                    b.ToTable("Matieres");
                });

            modelBuilder.Entity("MaClassePA.Models.RessourceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contenu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EstSupprime")
                        .HasColumnType("bit");

                    b.Property<int>("MatiereId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rendu")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MatiereId");

                    b.ToTable("Ressources");
                });

            modelBuilder.Entity("MaClassePA.Models.MatiereModel", b =>
                {
                    b.HasOne("MaClassePA.Models.ClasseModel", "Classe")
                        .WithMany("Matieres")
                        .HasForeignKey("ClasseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classe");
                });

            modelBuilder.Entity("MaClassePA.Models.RessourceModel", b =>
                {
                    b.HasOne("MaClassePA.Models.MatiereModel", "Matiere")
                        .WithMany("Ressources")
                        .HasForeignKey("MatiereId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Matiere");
                });

            modelBuilder.Entity("MaClassePA.Models.ClasseModel", b =>
                {
                    b.Navigation("Matieres");
                });

            modelBuilder.Entity("MaClassePA.Models.MatiereModel", b =>
                {
                    b.Navigation("Ressources");
                });
#pragma warning restore 612, 618
        }
    }
}
