// <auto-generated />
using System;
using LabReservation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LabReservation.Migrations
{
    [DbContext(typeof(LabReservationContext))]
    [Migration("20210412092418_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("LabReservation.Models.Blacklist", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("user_id")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Blacklist");
                });

            modelBuilder.Entity("LabReservation.Models.Equipment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("lab_id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("maximum")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("LabReservation.Models.Labinfo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("equip")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Labinfo");
                });

            modelBuilder.Entity("LabReservation.Models.Reserveinfo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("end_time")
                        .HasColumnType("TEXT");

                    b.Property<int>("lab_id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("reserve_by")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("start_time")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Reserveinfo");
                });

            modelBuilder.Entity("LabReservation.Models.Userinfo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("email")
                        .HasColumnType("TEXT");

                    b.Property<string>("password")
                        .HasColumnType("TEXT");

                    b.Property<int>("role")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Userinfo");
                });
#pragma warning restore 612, 618
        }
    }
}
