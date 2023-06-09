﻿// <auto-generated />
using LabbAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LabbAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LabbAPI.Models.Interest", b =>
                {
                    b.Property<int>("InterestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InterestId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InterestId");

                    b.ToTable("Interests");
                });

            modelBuilder.Entity("LabbAPI.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");

                    b.HasData(
                        new
                        {
                            PersonId = 1,
                            FirstName = "Jesper",
                            LastName = "Andersson",
                            PhoneNr = "1231231233"
                        },
                        new
                        {
                            PersonId = 2,
                            FirstName = "Jens",
                            LastName = "Jansson",
                            PhoneNr = "2311232322"
                        },
                        new
                        {
                            PersonId = 3,
                            FirstName = "Lotta",
                            LastName = "Magnusson",
                            PhoneNr = "3213213211"
                        },
                        new
                        {
                            PersonId = 4,
                            FirstName = "Runar",
                            LastName = "Larsson",
                            PhoneNr = "2311321231"
                        },
                        new
                        {
                            PersonId = 5,
                            FirstName = "Madde",
                            LastName = "Karlsson",
                            PhoneNr = "3213213213"
                        });
                });

            modelBuilder.Entity("LabbAPI.Models.PersonInterest", b =>
                {
                    b.Property<int>("PersonInterestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonInterestId"));

                    b.Property<int>("FkInterestId")
                        .HasColumnType("int");

                    b.Property<int>("FkPersonId")
                        .HasColumnType("int");

                    b.HasKey("PersonInterestId");

                    b.HasIndex("FkInterestId");

                    b.HasIndex("FkPersonId");

                    b.ToTable("PersonInterests");
                });

            modelBuilder.Entity("LabbAPI.Models.WebURL", b =>
                {
                    b.Property<int>("WebURLId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WebURLId"));

                    b.Property<int>("FkInterestId")
                        .HasColumnType("int");

                    b.Property<int>("FkPersonId")
                        .HasColumnType("int");

                    b.Property<string>("WebLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WebURLId");

                    b.HasIndex("FkInterestId");

                    b.HasIndex("FkPersonId");

                    b.ToTable("WebURLs");
                });

            modelBuilder.Entity("LabbAPI.Models.PersonInterest", b =>
                {
                    b.HasOne("LabbAPI.Models.Interest", "Interest")
                        .WithMany()
                        .HasForeignKey("FkInterestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LabbAPI.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("FkPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interest");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("LabbAPI.Models.WebURL", b =>
                {
                    b.HasOne("LabbAPI.Models.Interest", "Interest")
                        .WithMany()
                        .HasForeignKey("FkInterestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LabbAPI.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("FkPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interest");

                    b.Navigation("Person");
                });
#pragma warning restore 612, 618
        }
    }
}
