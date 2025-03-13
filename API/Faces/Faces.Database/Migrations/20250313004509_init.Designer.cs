﻿// <auto-generated />
using System;
using Faces.Database.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Faces.Database.Migrations
{
    [DbContext(typeof(FacesDbContext))]
    [Migration("20250313004509_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Faces.Database.EF.DbEmployee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("BirthDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DocNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("JobFunctionCode")
                        .IsRequired()
                        .HasColumnType("character varying(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int?>("ManagerID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("JobFunctionCode");

                    b.HasIndex("ManagerID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Faces.Database.EF.DbJobFunction", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<short>("HierarchyLevel")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Code");

                    b.ToTable("JobFunctions");

                    b.HasData(
                        new
                        {
                            Code = "0001",
                            HierarchyLevel = (short)10,
                            Name = "Analyst"
                        },
                        new
                        {
                            Code = "0002",
                            HierarchyLevel = (short)1000,
                            Name = "CEO"
                        },
                        new
                        {
                            Code = "0003",
                            HierarchyLevel = (short)100,
                            Name = "Tech Leader"
                        });
                });

            modelBuilder.Entity("Faces.Database.EF.DbPhone", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("EmployeeID")
                        .HasColumnType("integer");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("Faces.Database.EF.DbEmployee", b =>
                {
                    b.HasOne("Faces.Database.EF.DbJobFunction", "JobFunction")
                        .WithMany()
                        .HasForeignKey("JobFunctionCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Faces.Database.EF.DbEmployee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerID");

                    b.Navigation("JobFunction");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Faces.Database.EF.DbPhone", b =>
                {
                    b.HasOne("Faces.Database.EF.DbEmployee", "Employee")
                        .WithMany("Phones")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Faces.Database.EF.DbEmployee", b =>
                {
                    b.Navigation("Phones");
                });
#pragma warning restore 612, 618
        }
    }
}
