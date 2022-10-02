﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShootingClub.WebAPI.Context;

#nullable disable

namespace ShootingClub.WebAPI.Migrations
{
    [DbContext(typeof(WebAPIContext))]
    [Migration("20221002210843_PopulateClients")]
    partial class PopulateClients
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ShootingClub.WebAPI.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"), 1L, 1);

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("char(8)");

                    b.Property<DateTime?>("ChangeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("char(2)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AddressId");

                    b.HasIndex("ClientId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ShootingClub.WebAPI.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientId"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("char(11)");

                    b.Property<DateTime?>("ChangeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("char(11)");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("char(1)");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("ShootingClub.WebAPI.Models.File", b =>
                {
                    b.Property<int>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FileId"), 1L, 1);

                    b.Property<DateTime?>("ChangeDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("FileId");

                    b.HasIndex("ClientId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("ShootingClub.WebAPI.Models.Address", b =>
                {
                    b.HasOne("ShootingClub.WebAPI.Models.Client", "Cliente")
                        .WithMany("Addresses")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("ShootingClub.WebAPI.Models.File", b =>
                {
                    b.HasOne("ShootingClub.WebAPI.Models.Client", "Client")
                        .WithMany("Files")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("ShootingClub.WebAPI.Models.Client", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
