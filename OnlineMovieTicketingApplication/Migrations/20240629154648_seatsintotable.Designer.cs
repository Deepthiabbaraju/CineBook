﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineMovieTicketingApplication.Models;

#nullable disable

namespace OnlineMovieTicketingApplication.Migrations
{
    [DbContext(typeof(MovieContext))]
    [Migration("20240629154648_seatsintotable")]
    partial class seatsintotable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineMovieTicketingApplication.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("SeatNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("OnlineMovieTicketingApplication.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("duration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("OnlineMovieTicketingApplication.Models.Seat", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ShowTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TheatreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("TheatreId");

                    b.ToTable("Seats");

                    b.HasData(
                        new
                        {
                            Id = "A1",
                            Available = true,
                            MovieId = 1,
                            ShowTime = new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TheatreId = 2
                        },
                        new
                        {
                            Id = "A2",
                            Available = true,
                            MovieId = 1,
                            ShowTime = new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TheatreId = 2
                        },
                        new
                        {
                            Id = "A3",
                            Available = true,
                            MovieId = 1,
                            ShowTime = new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TheatreId = 2
                        },
                        new
                        {
                            Id = "A4",
                            Available = true,
                            MovieId = 1,
                            ShowTime = new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TheatreId = 2
                        },
                        new
                        {
                            Id = "A5",
                            Available = true,
                            MovieId = 1,
                            ShowTime = new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TheatreId = 2
                        },
                        new
                        {
                            Id = "A6",
                            Available = true,
                            MovieId = 1,
                            ShowTime = new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TheatreId = 2
                        },
                        new
                        {
                            Id = "A7",
                            Available = true,
                            MovieId = 1,
                            ShowTime = new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TheatreId = 2
                        },
                        new
                        {
                            Id = "A8",
                            Available = true,
                            MovieId = 1,
                            ShowTime = new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TheatreId = 2
                        },
                        new
                        {
                            Id = "A9",
                            Available = true,
                            MovieId = 1,
                            ShowTime = new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TheatreId = 2
                        },
                        new
                        {
                            Id = "A10",
                            Available = true,
                            MovieId = 1,
                            ShowTime = new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TheatreId = 2
                        },
                        new
                        {
                            Id = "A11",
                            Available = true,
                            MovieId = 1,
                            ShowTime = new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TheatreId = 2
                        },
                        new
                        {
                            Id = "A12",
                            Available = true,
                            MovieId = 1,
                            ShowTime = new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TheatreId = 2
                        },
                        new
                        {
                            Id = "A13",
                            Available = true,
                            MovieId = 1,
                            ShowTime = new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TheatreId = 2
                        },
                        new
                        {
                            Id = "A14",
                            Available = true,
                            MovieId = 1,
                            ShowTime = new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TheatreId = 2
                        },
                        new
                        {
                            Id = "A15",
                            Available = true,
                            MovieId = 1,
                            ShowTime = new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TheatreId = 2
                        },
                        new
                        {
                            Id = "A16",
                            Available = true,
                            MovieId = 1,
                            ShowTime = new DateTime(2024, 6, 29, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TheatreId = 2
                        });
                });

            modelBuilder.Entity("OnlineMovieTicketingApplication.Models.Shows", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ShowTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TheatreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("OnlineMovieTicketingApplication.Models.Theatre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Theatres");
                });

            modelBuilder.Entity("OnlineMovieTicketingApplication.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreferredLanguage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OnlineMovieTicketingApplication.Models.Seat", b =>
                {
                    b.HasOne("OnlineMovieTicketingApplication.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineMovieTicketingApplication.Models.Theatre", "Theatre")
                        .WithMany()
                        .HasForeignKey("TheatreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Theatre");
                });
#pragma warning restore 612, 618
        }
    }
}
