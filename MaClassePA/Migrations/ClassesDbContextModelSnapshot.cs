﻿// <auto-generated />
using System;
using MaClassePA.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MaClassePA.Migrations
{
    [DbContext(typeof(ClassesDbContext))]
    partial class ClassesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("MaClassePA.Models.ClasseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("EstSupprime")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("MaClassePA.Models.MatiereModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClasseId")
                        .HasColumnType("int");

                    b.Property<bool>("EstSupprime")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ClasseId");

                    b.ToTable("Matieres");
                });

            modelBuilder.Entity("MaClassePA.Models.RessourceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Contenu")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("EstSupprime")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MatiereId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Rendu")
                        .HasColumnType("longtext");

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
