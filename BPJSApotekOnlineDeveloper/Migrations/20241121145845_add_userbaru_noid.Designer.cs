﻿// <auto-generated />
using System;
using BPJSApotekOnlineDeveloper.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BPJSApotekOnlineDeveloper.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241121145845_add_userbaru_noid")]
    partial class add_userbaru_noid
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BPJSApotekOnlineDeveloper.Areas.MasterData.Models.Pendaftaran", b =>
                {
                    b.Property<Guid>("PendaftaranId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AlamatTinggal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CreateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreateDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("DeleteBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DeleteDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InformasiObat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCancel")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("JenisKelamin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NamaApotek")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NamaKeluarga")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NamaLengkapPeserta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoIdentitasKTPSIM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoKartuBpjs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoTelepon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusKepesertaanBpjs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusVerifikasi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TanggalLahir")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TanggalPendaftaranBpjs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipeKepesertaanBpjs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UpdateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("UpdateDateTime")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("PendaftaranId");

                    b.ToTable("MasterPendaftaran", "dbo");
                });

            modelBuilder.Entity("BPJSApotekOnlineDeveloper.Areas.MasterData.Models.UserActive", b =>
                {
                    b.Property<Guid>("UserActiveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CreateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreateDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateOfBirth")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("DeleteBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DeleteDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Handphone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCancel")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("PlaceOfBirth")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UpdateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("UpdateDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UserActiveCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserActiveId");

                    b.ToTable("MstUserActive", "dbo");
                });
#pragma warning restore 612, 618
        }
    }
}