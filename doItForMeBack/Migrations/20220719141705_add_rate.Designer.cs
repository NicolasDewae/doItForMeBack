﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using doItForMeBack.Data;

#nullable disable

namespace doItForMeBack.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220719141705_add_rate")]
    partial class add_rate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("doItForMeBack.Entities.Rate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdUserRate")
                        .HasColumnType("int");

                    b.Property<int>("IdUserRated")
                        .HasColumnType("int");

                    b.Property<float>("star")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Rate");
                });

            modelBuilder.Entity("doItForMeBack.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Picture")
                        .HasColumnType("longtext");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("RateId")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RateId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("doItForMeBack.Entities.User", b =>
                {
                    b.HasOne("doItForMeBack.Entities.Rate", "Rate")
                        .WithMany()
                        .HasForeignKey("RateId");

                    b.Navigation("Rate");
                });
#pragma warning restore 612, 618
        }
    }
}
