﻿// <auto-generated />
using System;
using Faces.Database.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Faces.Database.Migrations
{
    [DbContext(typeof(FacesDbContext))]
    partial class FacesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("DocNumber")
                        .IsUnique();

                    b.HasIndex("JobFunctionCode");

                    b.HasIndex("ManagerID");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            ID = -1,
                            BirthDate = "1990-01-01",
                            CreationDate = new DateTime(2025, 3, 17, 14, 35, 0, 0, DateTimeKind.Utc),
                            DocNumber = "0001",
                            Email = "admin.rh@faces.com",
                            FirstName = "RH",
                            IsDeleted = false,
                            JobFunctionCode = "0001",
                            LastName = "Admin",
                            PasswordHash = "i+rcXhRFTdUTkNVTJ07ydQ==.HXcBql16eQsd/uam7bZdKvPhAIAotA8kbwx7FBsrRBc="
                        });
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
                            HierarchyLevel = (short)1000,
                            Name = "RH Manager"
                        },
                        new
                        {
                            Code = "0002",
                            HierarchyLevel = (short)500,
                            Name = "RH Analyst"
                        },
                        new
                        {
                            Code = "0003",
                            HierarchyLevel = (short)500,
                            Name = "Tech Leader"
                        },
                        new
                        {
                            Code = "0004",
                            HierarchyLevel = (short)100,
                            Name = "IT Analyst"
                        },
                        new
                        {
                            Code = "0005",
                            HierarchyLevel = (short)1,
                            Name = "Intern"
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
