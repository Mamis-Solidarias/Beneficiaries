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
    [Migration("20220827125925_family")]
    partial class family
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

                    b.HasIndex("Id");

                    b.ToTable("Families");
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
                    b.Navigation("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}
