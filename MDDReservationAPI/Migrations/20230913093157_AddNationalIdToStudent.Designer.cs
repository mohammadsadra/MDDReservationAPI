﻿// <auto-generated />
using System;
using MDDReservationAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MDDReservationAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230913093157_AddNationalIdToStudent")]
    partial class AddNationalIdToStudent
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("MDDReservationAPI.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("MDDReservationAPI.Models.RegistrationForm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("ManagerId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SchoolClassId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SchoolId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SchoolClassId");

                    b.HasIndex("SchoolId");

                    b.ToTable("RegistrationForms");
                });

            modelBuilder.Entity("MDDReservationAPI.Models.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Gender")
                        .HasMaxLength(2)
                        .HasColumnType("INTEGER");

                    b.Property<int>("ManagerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int?>("SchoolType")
                        .HasMaxLength(10)
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("MDDReservationAPI.Models.SchoolClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Gender")
                        .HasMaxLength(2)
                        .HasColumnType("INTEGER");

                    b.Property<int>("Grade")
                        .HasMaxLength(10)
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsProgrammer")
                        .HasMaxLength(10)
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProgrammingLanguage")
                        .HasMaxLength(10)
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SchoolId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("SchoolsClasses");
                });

            modelBuilder.Entity("MDDReservationAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsVerify")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("MDDReservationAPI.Models.Admin", b =>
                {
                    b.HasBaseType("MDDReservationAPI.Models.User");

                    b.HasDiscriminator().HasValue("Admin");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 9, 13, 9, 31, 57, 427, DateTimeKind.Utc).AddTicks(3045),
                            Email = "mohammadsadrahaeri@gmail.com",
                            IsVerify = true,
                            Name = "MohammadSadra Haeri",
                            Phone = "09127959211",
                            Role = 0
                        });
                });

            modelBuilder.Entity("MDDReservationAPI.Models.Manager", b =>
                {
                    b.HasBaseType("MDDReservationAPI.Models.User");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Manager");
                });

            modelBuilder.Entity("MDDReservationAPI.Models.Student", b =>
                {
                    b.HasBaseType("MDDReservationAPI.Models.User");

                    b.Property<string>("NationalId")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SchoolClassId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SchoolClassId");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("MDDReservationAPI.Models.RegistrationForm", b =>
                {
                    b.HasOne("MDDReservationAPI.Models.Manager", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MDDReservationAPI.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.HasOne("MDDReservationAPI.Models.SchoolClass", "SchoolClass")
                        .WithMany()
                        .HasForeignKey("SchoolClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MDDReservationAPI.Models.School", "School")
                        .WithMany()
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manager");

                    b.Navigation("Project");

                    b.Navigation("School");

                    b.Navigation("SchoolClass");
                });

            modelBuilder.Entity("MDDReservationAPI.Models.School", b =>
                {
                    b.HasOne("MDDReservationAPI.Models.Manager", "manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("manager");
                });

            modelBuilder.Entity("MDDReservationAPI.Models.SchoolClass", b =>
                {
                    b.HasOne("MDDReservationAPI.Models.School", null)
                        .WithMany("SchoolClasses")
                        .HasForeignKey("SchoolId");
                });

            modelBuilder.Entity("MDDReservationAPI.Models.Student", b =>
                {
                    b.HasOne("MDDReservationAPI.Models.Project", null)
                        .WithMany("Students")
                        .HasForeignKey("ProjectId");

                    b.HasOne("MDDReservationAPI.Models.SchoolClass", "SchoolClass")
                        .WithMany("Students")
                        .HasForeignKey("SchoolClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SchoolClass");
                });

            modelBuilder.Entity("MDDReservationAPI.Models.Project", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("MDDReservationAPI.Models.School", b =>
                {
                    b.Navigation("SchoolClasses");
                });

            modelBuilder.Entity("MDDReservationAPI.Models.SchoolClass", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}