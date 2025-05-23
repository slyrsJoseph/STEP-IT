﻿// <auto-generated />
using System;
using LogAnalizerServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LogAnalizerServer.Migrations
{
    [DbContext(typeof(LogAnalizerServerDbContext))]
    [Migration("20250414181727_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LogAnalizerServer.Models.AlarmLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AlarmClass")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AlarmId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AlarmMessage")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("FinalState")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("GenerationTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("GenerationTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LocalZoneTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LogAction")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LoggedBy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PrevState")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Project")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Resource")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("SequenceNumber")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("TimeWhenLogged")
                        .HasColumnType("datetime2");

                    b.Property<int>("WeekType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AlarmLogs");
                });

            modelBuilder.Entity("LogAnalizerServer.Models.ComparisonResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AlarmMessage")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("CountWeek1")
                        .HasColumnType("int");

                    b.Property<int>("CountWeek2")
                        .HasColumnType("int");

                    b.Property<int>("Week1Type")
                        .HasColumnType("int");

                    b.Property<int>("Week2Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ComparisonResults");
                });
#pragma warning restore 612, 618
        }
    }
}
