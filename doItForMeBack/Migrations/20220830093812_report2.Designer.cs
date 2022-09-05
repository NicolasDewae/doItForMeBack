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
    [Migration("20220830093812_report2")]
    partial class report2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("doItForMeBack.Entities.Ban", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("BanDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsBan")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("UserBanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Bans");
                });

            modelBuilder.Entity("doItForMeBack.Entities.Mission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("BanId")
                        .HasColumnType("int");

                    b.Property<int>("ClaimantId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("MakerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("MissionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Picture")
                        .HasColumnType("longtext");

                    b.Property<float>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Tag")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("BanId");

                    b.HasIndex("MakerId");

                    b.ToTable("Missions");
                });

            modelBuilder.Entity("doItForMeBack.Entities.Rate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("Star")
                        .HasColumnType("float");

                    b.Property<int>("UserRateId")
                        .HasColumnType("int");

                    b.Property<int>("UserRatedId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserRateId");

                    b.HasIndex("UserRatedId");

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("doItForMeBack.Entities.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("MissionId")
                        .HasColumnType("int");

                    b.Property<string>("Picture")
                        .HasColumnType("longtext");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("claimantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MissionId");

                    b.HasIndex("UserId");

                    b.HasIndex("claimantId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("doItForMeBack.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("BanId")
                        .HasColumnType("int");

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

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("BanId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("doItForMeBack.Entities.Mission", b =>
                {
                    b.HasOne("doItForMeBack.Entities.Ban", "Ban")
                        .WithMany()
                        .HasForeignKey("BanId");

                    b.HasOne("doItForMeBack.Entities.User", "Maker")
                        .WithMany("Mission")
                        .HasForeignKey("MakerId");

                    b.Navigation("Ban");

                    b.Navigation("Maker");
                });

            modelBuilder.Entity("doItForMeBack.Entities.Rate", b =>
                {
                    b.HasOne("doItForMeBack.Entities.User", "UserRate")
                        .WithMany()
                        .HasForeignKey("UserRateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("doItForMeBack.Entities.User", "UserRated")
                        .WithMany("Rate")
                        .HasForeignKey("UserRatedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRate");

                    b.Navigation("UserRated");
                });

            modelBuilder.Entity("doItForMeBack.Entities.Report", b =>
                {
                    b.HasOne("doItForMeBack.Entities.Mission", "Mission")
                        .WithMany("Report")
                        .HasForeignKey("MissionId");

                    b.HasOne("doItForMeBack.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("doItForMeBack.Entities.User", "claimant")
                        .WithMany()
                        .HasForeignKey("claimantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mission");

                    b.Navigation("User");

                    b.Navigation("claimant");
                });

            modelBuilder.Entity("doItForMeBack.Entities.User", b =>
                {
                    b.HasOne("doItForMeBack.Entities.Ban", "Ban")
                        .WithMany()
                        .HasForeignKey("BanId");

                    b.Navigation("Ban");
                });

            modelBuilder.Entity("doItForMeBack.Entities.Mission", b =>
                {
                    b.Navigation("Report");
                });

            modelBuilder.Entity("doItForMeBack.Entities.User", b =>
                {
                    b.Navigation("Mission");

                    b.Navigation("Rate");
                });
#pragma warning restore 612, 618
        }
    }
}
