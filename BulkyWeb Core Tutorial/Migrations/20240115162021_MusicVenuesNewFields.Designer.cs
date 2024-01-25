﻿// <auto-generated />
using MGTConcerts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MGTConcerts.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240115162021_MusicVenuesNewFields")]
    partial class MusicVenuesNewFields
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MGTConcerts.Models.MusicVenue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Music_Venues");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Capacity = 5000,
                            Description = "Τεχνόπολη Δήμου Αθηναίων",
                            Location = "Κεραμεικός",
                            Name = "Τεχνοπολις"
                        },
                        new
                        {
                            Id = 2,
                            Capacity = 2000,
                            Description = "Music Club",
                            Location = "Ταύρος",
                            Name = "Fuzz"
                        },
                        new
                        {
                            Id = 3,
                            Capacity = 2000,
                            Description = "Συναυλιακός Χώρος Δήμου Πειραιά",
                            Location = "Πειραιάς",
                            Name = "Λιπάσματα"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
