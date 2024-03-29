﻿// <auto-generated />
using System;
using InstitutionOfHigherEducation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InstitutionOfHigherEducation.Migrations
{
    [DbContext(typeof(IHEContext))]
    [Migration("20190802023602_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Model.Entries.Department", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("InstitutionId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("InstitutionId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Model.Entries.Institution", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Institutions");
                });

            modelBuilder.Entity("Model.Entries.Department", b =>
                {
                    b.HasOne("Model.Entries.Institution", "Institution")
                        .WithMany("Departments")
                        .HasForeignKey("InstitutionId");
                });
#pragma warning restore 612, 618
        }
    }
}
