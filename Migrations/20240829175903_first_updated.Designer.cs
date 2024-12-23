﻿// <auto-generated />
using System;
using Hr_Vacancy_Managment.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hr_Vacancy_Managment.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240829175903_first_updated")]
    partial class first_updated
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hr_Vacancy_Managment.Models.Department", b =>
                {
                    b.Property<int>("Department_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Department_ID"));

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Department_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Department_Status")
                        .HasColumnType("int");

                    b.HasKey("Department_ID");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("Hr_Vacancy_Managment.Models.Employee", b =>
                {
                    b.Property<int>("Employee_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Employee_ID"));

                    b.Property<int>("Depart_ID")
                        .HasColumnType("int");

                    b.Property<string>("Employee_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Employee_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Employee_ID");

                    b.HasIndex("Depart_ID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Hr_Vacancy_Managment.Models.Employee", b =>
                {
                    b.HasOne("Hr_Vacancy_Managment.Models.Department", "department")
                        .WithMany("employees")
                        .HasForeignKey("Depart_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("department");
                });

            modelBuilder.Entity("Hr_Vacancy_Managment.Models.Department", b =>
                {
                    b.Navigation("employees");
                });
#pragma warning restore 612, 618
        }
    }
}
