﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QutebaApp_Data.Data;

namespace QutebaApp_Data.Migrations
{
    [DbContext(typeof(QutebaAppDbContext))]
    [Migration("20201105211937_main")]
    partial class main
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("QutebaApp_Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CategoryCreationTime")
                        .HasColumnName("category_creation_time")
                        .HasColumnType("datetime");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnName("category_name")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45);

                    b.Property<string>("CategoryType")
                        .IsRequired()
                        .HasColumnName("category_type")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45);

                    b.Property<int>("UserId")
                        .HasColumnName("user_cID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .HasName("user_ID_idx");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("QutebaApp_Data.Models.Code", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int");

                    b.Property<int>("CodeDigit")
                        .HasColumnName("code_digit")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnName("code_creation_time")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnName("user_cID")
                        .HasColumnType("int");

                    b.HasKey("ID")
                        .HasName("PRIMARY");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasName("user_ID_UNIQUE");

                    b.ToTable("codes");
                });

            modelBuilder.Entity("QutebaApp_Data.Models.Income", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int");

                    b.Property<double>("IncomeAmount")
                        .HasColumnName("income_amount")
                        .HasColumnType("double");

                    b.Property<int>("IncomeCategoryId")
                        .HasColumnName("income_category_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("IncomeCreationTime")
                        .HasColumnName("income_creation_time")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnName("user_iID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IncomeCategoryId")
                        .HasName("category_iid_idx");

                    b.HasIndex("UserId")
                        .HasName("user_iid_idx");

                    b.ToTable("incomes");
                });

            modelBuilder.Entity("QutebaApp_Data.Models.Profile", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnName("user_pID")
                        .HasColumnType("int");

                    b.Property<string>("PhotoUrl")
                        .HasColumnName("photo_url")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime>("ProfileCreationTime")
                        .HasColumnName("profile_creation_time")
                        .HasColumnType("datetime");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnName("username")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45);

                    b.HasKey("UserId")
                        .HasName("PRIMARY");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasName("user_ID_UNIQUE");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasName("username_UNIQUE");

                    b.ToTable("profiles");
                });

            modelBuilder.Entity("QutebaApp_Data.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("RoleCreationTime")
                        .HasColumnName("role_creation_time")
                        .HasColumnType("datetime");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnName("role_name")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45);

                    b.HasKey("Id");

                    b.HasIndex("RoleName")
                        .IsUnique()
                        .HasName("role_name_UNIQUE");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("QutebaApp_Data.Models.Spending", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnName("reason")
                        .HasColumnType("text");

                    b.Property<double>("SpendingAmount")
                        .HasColumnName("spending_amount")
                        .HasColumnType("double");

                    b.Property<int>("SpendingCategoryId")
                        .HasColumnName("spending_category_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("SpendingCreationTime")
                        .HasColumnName("spending_creation_time")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnName("user_sID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpendingCategoryId")
                        .HasName("category_ID_idx");

                    b.HasIndex("UserId")
                        .HasName("user_id_idx");

                    b.ToTable("spendings");
                });

            modelBuilder.Entity("QutebaApp_Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int");

                    b.Property<string>("CreatedAccountWith")
                        .IsRequired()
                        .HasColumnName("created_account_with")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45);

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnName("fullname")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45);

                    b.Property<int>("RoleId")
                        .HasColumnName("role_uID")
                        .HasColumnType("int");

                    b.Property<DateTime>("UserCreationTime")
                        .HasColumnName("user_creation_time")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("email_UNIQUE");

                    b.HasIndex("RoleId")
                        .HasName("role_ID_idx");

                    b.ToTable("users");
                });

            modelBuilder.Entity("QutebaApp_Data.Models.Category", b =>
                {
                    b.HasOne("QutebaApp_Data.Models.User", "User")
                        .WithMany("Categories")
                        .HasForeignKey("UserId")
                        .HasConstraintName("user_id")
                        .IsRequired();
                });

            modelBuilder.Entity("QutebaApp_Data.Models.Code", b =>
                {
                    b.HasOne("QutebaApp_Data.Models.User", "User")
                        .WithOne("Code")
                        .HasForeignKey("QutebaApp_Data.Models.Code", "UserId")
                        .HasConstraintName("user_cid")
                        .IsRequired();
                });

            modelBuilder.Entity("QutebaApp_Data.Models.Income", b =>
                {
                    b.HasOne("QutebaApp_Data.Models.Category", "IncomeCategory")
                        .WithMany("Incomes")
                        .HasForeignKey("IncomeCategoryId")
                        .HasConstraintName("category_iid")
                        .IsRequired();

                    b.HasOne("QutebaApp_Data.Models.User", "User")
                        .WithMany("Incomes")
                        .HasForeignKey("UserId")
                        .HasConstraintName("user_iid")
                        .IsRequired();
                });

            modelBuilder.Entity("QutebaApp_Data.Models.Profile", b =>
                {
                    b.HasOne("QutebaApp_Data.Models.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("QutebaApp_Data.Models.Profile", "UserId")
                        .HasConstraintName("user_pid")
                        .IsRequired();
                });

            modelBuilder.Entity("QutebaApp_Data.Models.Spending", b =>
                {
                    b.HasOne("QutebaApp_Data.Models.Category", "SpendingCategory")
                        .WithMany("Spendings")
                        .HasForeignKey("SpendingCategoryId")
                        .HasConstraintName("category_sid")
                        .IsRequired();

                    b.HasOne("QutebaApp_Data.Models.User", "User")
                        .WithMany("Spendings")
                        .HasForeignKey("UserId")
                        .HasConstraintName("user_sid")
                        .IsRequired();
                });

            modelBuilder.Entity("QutebaApp_Data.Models.User", b =>
                {
                    b.HasOne("QutebaApp_Data.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("role_uid")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}