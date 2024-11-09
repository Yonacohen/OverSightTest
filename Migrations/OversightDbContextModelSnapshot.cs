﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OverSightTest;

#nullable disable

namespace OverSightTest.Migrations
{
    [DbContext(typeof(OversightDbContext))]
    partial class OversightDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("OverSightTest.Entities.Coupon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("Discount")
                        .HasColumnType("float");

                    b.Property<bool>("DoublePromotions")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("ExpiredDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsLimited")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("LimitedUseNum")
                        .HasColumnType("int");

                    b.Property<Guid>("UserCreatorId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Coupons");
                });
#pragma warning restore 612, 618
        }
    }
}