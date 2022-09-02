﻿// <auto-generated />
using System;
using MamisSolidarias.Infrastructure.Beneficiaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MamisSolidarias.WebAPI.Beneficiaries.Migrations
{
    [DbContext(typeof(BeneficiariesDbContext))]
    [Migration("20220831144156_beneficiaries_updates")]
    partial class beneficiaries_updates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MamisSolidarias.Infrastructure.Beneficiaries.Models.Beneficiary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("date");

                    b.Property<int?>("ClothesId")
                        .HasColumnType("integer");

                    b.Property<string>("Comments")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<int?>("EducationId")
                        .HasColumnType("integer");

                    b.Property<string>("FamilyId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<int?>("HealthId")
                        .HasColumnType("integer");

                    b.Property<int?>("JobId")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Likes")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClothesId");

                    b.HasIndex("Dni")
                        .IsUnique();

                    b.HasIndex("EducationId");

                    b.HasIndex("FamilyId");

                    b.HasIndex("HealthId");

                    b.HasIndex("JobId");

                    b.ToTable("Beneficiaries");
                });

            modelBuilder.Entity("MamisSolidarias.Infrastructure.Beneficiaries.Models.Clothes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("PantsSize")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ShirtSize")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ShoeSize")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Clothes");
                });

            modelBuilder.Entity("MamisSolidarias.Infrastructure.Beneficiaries.Models.Community", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("Id");

                    b.ToTable("Communities");
                });

            modelBuilder.Entity("MamisSolidarias.Infrastructure.Beneficiaries.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("FamilyInternalId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsPreferred")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FamilyInternalId");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("MamisSolidarias.Infrastructure.Beneficiaries.Models.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("School")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("TransportationMethod")
                        .HasColumnType("integer");

                    b.Property<string>("Year")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Education");
                });

            modelBuilder.Entity("MamisSolidarias.Infrastructure.Beneficiaries.Models.Family", b =>
                {
                    b.Property<int>("InternalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("InternalId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("CommunityId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Details")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int>("FamilyNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("InternalId");

                    b.HasIndex("CommunityId");

                    b.HasIndex("FamilyNumber");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Families");
                });

            modelBuilder.Entity("MamisSolidarias.Infrastructure.Beneficiaries.Models.Health", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool?>("HasCovidVaccine")
                        .HasColumnType("boolean");

                    b.Property<bool?>("HasMandatoryVaccines")
                        .HasColumnType("boolean");

                    b.Property<string>("Observations")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.HasKey("Id");

                    b.ToTable("Health");
                });

            modelBuilder.Entity("MamisSolidarias.Infrastructure.Beneficiaries.Models.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("MamisSolidarias.Infrastructure.Beneficiaries.Models.Beneficiary", b =>
                {
                    b.HasOne("MamisSolidarias.Infrastructure.Beneficiaries.Models.Clothes", "Clothes")
                        .WithMany()
                        .HasForeignKey("ClothesId");

                    b.HasOne("MamisSolidarias.Infrastructure.Beneficiaries.Models.Education", "Education")
                        .WithMany()
                        .HasForeignKey("EducationId");

                    b.HasOne("MamisSolidarias.Infrastructure.Beneficiaries.Models.Family", "Family")
                        .WithMany("Beneficiaries")
                        .HasForeignKey("FamilyId")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MamisSolidarias.Infrastructure.Beneficiaries.Models.Health", "Health")
                        .WithMany()
                        .HasForeignKey("HealthId");

                    b.HasOne("MamisSolidarias.Infrastructure.Beneficiaries.Models.Job", "Job")
                        .WithMany()
                        .HasForeignKey("JobId");

                    b.Navigation("Clothes");

                    b.Navigation("Education");

                    b.Navigation("Family");

                    b.Navigation("Health");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("MamisSolidarias.Infrastructure.Beneficiaries.Models.Contact", b =>
                {
                    b.HasOne("MamisSolidarias.Infrastructure.Beneficiaries.Models.Family", null)
                        .WithMany("Contacts")
                        .HasForeignKey("FamilyInternalId");
                });

            modelBuilder.Entity("MamisSolidarias.Infrastructure.Beneficiaries.Models.Family", b =>
                {
                    b.HasOne("MamisSolidarias.Infrastructure.Beneficiaries.Models.Community", "Community")
                        .WithMany("Families")
                        .HasForeignKey("CommunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Community");
                });

            modelBuilder.Entity("MamisSolidarias.Infrastructure.Beneficiaries.Models.Community", b =>
                {
                    b.Navigation("Families");
                });

            modelBuilder.Entity("MamisSolidarias.Infrastructure.Beneficiaries.Models.Family", b =>
                {
                    b.Navigation("Beneficiaries");

                    b.Navigation("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}
