﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodTruckRodeo.API.Migrations
{
  [DbContext(typeof(DataContext))]
  [Migration("20200726213015_AddedUserEntity")]
  partial class AddedUserEntity
  {
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
      modelBuilder
          .HasAnnotation("ProductVersion", "3.1.6");

      modelBuilder.Entity("Models.User", b =>
          {
            b.Property<int>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("INTEGER");

            b.Property<byte[]>("PasswordHash")
                      .HasColumnType("BLOB");

            b.Property<byte[]>("PasswordSalt")
                      .HasColumnType("BLOB");

            b.Property<string>("Username")
                      .HasColumnType("TEXT");

            b.HasKey("Id");

            b.ToTable("Users");
          });

      modelBuilder.Entity("Models.Value", b =>
          {
            b.Property<int>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("INTEGER");

            b.Property<string>("Name")
                      .HasColumnType("TEXT");

            b.HasKey("Id");

            b.ToTable("Values");
          });
#pragma warning restore 612, 618
    }
  }
}
