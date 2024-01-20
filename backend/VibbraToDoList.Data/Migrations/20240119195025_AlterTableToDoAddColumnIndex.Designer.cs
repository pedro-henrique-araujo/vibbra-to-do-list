﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VibbraToDoList.Data;

#nullable disable

namespace VibbraToDoList.Data.Migrations
{
    [DbContext(typeof(VibbraToDoListDbContext))]
    [Migration("20240119195025_AlterTableToDoAddColumnIndex")]
    partial class AlterTableToDoAddColumnIndex
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("VibbraToDoList.Data.Models.ToDo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Index")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDone")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ToDoId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ToDoListId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ToDoId");

                    b.HasIndex("ToDoListId");

                    b.ToTable("ToDos");
                });

            modelBuilder.Entity("VibbraToDoList.Data.Models.ToDoList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OwnerUserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ToDoLists");
                });

            modelBuilder.Entity("VibbraToDoList.Data.Models.ToDoListUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ToDoListId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ToDoListId");

                    b.ToTable("ToDoListsUsers");
                });

            modelBuilder.Entity("VibbraToDoList.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("VibbraToDoList.Data.Models.ToDo", b =>
                {
                    b.HasOne("VibbraToDoList.Data.Models.ToDo", null)
                        .WithMany("Children")
                        .HasForeignKey("ToDoId");

                    b.HasOne("VibbraToDoList.Data.Models.ToDoList", null)
                        .WithMany("ToDos")
                        .HasForeignKey("ToDoListId");
                });

            modelBuilder.Entity("VibbraToDoList.Data.Models.ToDoListUser", b =>
                {
                    b.HasOne("VibbraToDoList.Data.Models.ToDoList", null)
                        .WithMany("ToDoListsUsers")
                        .HasForeignKey("ToDoListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VibbraToDoList.Data.Models.ToDo", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("VibbraToDoList.Data.Models.ToDoList", b =>
                {
                    b.Navigation("ToDoListsUsers");

                    b.Navigation("ToDos");
                });
#pragma warning restore 612, 618
        }
    }
}