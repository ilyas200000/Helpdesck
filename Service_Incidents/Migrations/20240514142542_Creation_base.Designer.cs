﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Service_Incidents.Data;

#nullable disable

namespace Service_Incidents.Migrations
{
    [DbContext(typeof(IncidentsDbContext))]
    [Migration("20240514142542_Creation_base")]
    partial class Creation_base
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Service_Incidents.Models.Incident", b =>
                {
                    b.Property<int>("INCD_ID")
                        .HasColumnType("int");

                    b.Property<string>("INCD_DESC")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("INCD_ENTT_SG_ID")
                        .HasColumnType("int");

                    b.Property<int>("INCD_NUM_TICK")
                        .HasColumnType("int");

                    b.Property<int>("INCD_PRIO_ID")
                        .HasColumnType("int");

                    b.Property<int>("INCD_STAT_ID")
                        .HasColumnType("int");

                    b.Property<int>("INCD_TYPE_ID")
                        .HasColumnType("int");

                    b.Property<int>("INCD_UTIL_ID")
                        .HasColumnType("int");

                    b.Property<bool>("IsSendSms1")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSendSms2")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSendSms3")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSendSms4")
                        .HasColumnType("bit");

                    b.Property<int>("Motif_id")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("agn_code")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("incd_audit")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("incd_date_cloture")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("incd_date_creation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("incd_date_resolution")
                        .HasColumnType("datetime2");

                    b.Property<int?>("niveau_escalade")
                        .HasColumnType("int");

                    b.Property<int?>("pres_id")
                        .HasColumnType("int");

                    b.HasKey("INCD_ID");

                    b.HasIndex("INCD_NUM_TICK")
                        .IsUnique();

                    b.HasIndex("INCD_PRIO_ID");

                    b.HasIndex("INCD_STAT_ID");

                    b.HasIndex("INCD_TYPE_ID");

                    b.ToTable("Incidents");
                });

            modelBuilder.Entity("Service_Incidents.Models.Priorite", b =>
                {
                    b.Property<int>("INCD_PRIO_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("INCD_PRIO_ID"));

                    b.Property<string>("PRIO_DESC")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("INCD_PRIO_ID");

                    b.ToTable("Priorites");
                });

            modelBuilder.Entity("Service_Incidents.Models.Statut", b =>
                {
                    b.Property<int>("INCD_STAT_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("INCD_STAT_ID"));

                    b.Property<string>("STAT_DESC")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("INCD_STAT_ID");

                    b.ToTable("Statuts");
                });

            modelBuilder.Entity("Service_Incidents.Models.Ticket", b =>
                {
                    b.Property<int>("INCD_NUM_TICK")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("INCD_NUM_TICK"));

                    b.Property<string>("TICK_DESC")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("INCD_NUM_TICK");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Service_Incidents.Models.Types", b =>
                {
                    b.Property<int>("INCD_TYPE_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("INCD_TYPE_ID"));

                    b.Property<string>("TYPE_DESC")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("INCD_TYPE_ID");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("Service_Incidents.Models.Incident", b =>
                {
                    b.HasOne("Service_Incidents.Models.Ticket", null)
                        .WithOne()
                        .HasForeignKey("Service_Incidents.Models.Incident", "INCD_NUM_TICK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Service_Incidents.Models.Priorite", null)
                        .WithMany()
                        .HasForeignKey("INCD_PRIO_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Service_Incidents.Models.Statut", null)
                        .WithMany()
                        .HasForeignKey("INCD_STAT_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Service_Incidents.Models.Types", null)
                        .WithMany()
                        .HasForeignKey("INCD_TYPE_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
