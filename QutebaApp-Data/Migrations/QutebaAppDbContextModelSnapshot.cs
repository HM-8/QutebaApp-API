﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QutebaApp_Data.Data;

namespace QutebaApp_Data.Migrations
{
    [DbContext(typeof(QutebaAppDbContext))]
    partial class QutebaAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("QutebaApp_Data.Models.Account", b =>
                {
                    b.Property<string>("Uid")
                        .HasColumnName("UID")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45)
                        .IsUnicode(false);

                    b.Property<string>("CreatedAccountWith")
                        .IsRequired()
                        .HasColumnName("created_account_with")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45)
                        .IsUnicode(false);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnName("creation_time")
                        .HasColumnType("datetime");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnName("display_name")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45)
                        .IsUnicode(false);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45)
                        .IsUnicode(false);

                    b.Property<byte?>("EmailVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("email_verified")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValueSql("'0'");

                    b.Property<DateTime?>("LastRefreshTime")
                        .HasColumnName("last_refresh_time")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("LastSigninTime")
                        .HasColumnName("last_signin_time")
                        .HasColumnType("datetime");

                    b.Property<int>("RoleId")
                        .HasColumnName("role_ID")
                        .HasColumnType("int");

                    b.HasKey("Uid")
                        .HasName("PRIMARY");

                    b.HasIndex("RoleId")
                        .HasName("role_ID_idx");

                    b.ToTable("accounts");
                });

            modelBuilder.Entity("QutebaApp_Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnName("creation_time")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("QutebaApp_Data.Models.Profile", b =>
                {
                    b.Property<string>("UserUid")
                        .HasColumnName("user_UID")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45)
                        .IsUnicode(false);

                    b.Property<string>("PhotoUrl")
                        .HasColumnName("photo_url")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<double?>("Salary")
                        .HasColumnName("salary")
                        .HasColumnType("double");

                    b.Property<DateTime?>("SalaryCreationTime")
                        .HasColumnName("salary_creation_time")
                        .HasColumnType("datetime");

                    b.HasKey("UserUid")
                        .HasName("PRIMARY");

                    b.ToTable("profiles");
                });

            modelBuilder.Entity("QutebaApp_Data.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnName("role")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("QutebaApp_Data.Models.Spending", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnName("amount")
                        .HasColumnType("double");

                    b.Property<int>("CategoryId")
                        .HasColumnName("category_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnName("creation_time")
                        .HasColumnType("datetime");

                    b.Property<string>("Reason")
                        .HasColumnName("reason")
                        .HasColumnType("text");

                    b.Property<string>("UserUid")
                        .IsRequired()
                        .HasColumnName("user_UID")
                        .HasColumnType("varchar(45)")
                        .HasMaxLength(45)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("CategoryId")
                        .HasName("category_ID_idx");

                    b.HasIndex("UserUid")
                        .HasName("user_ID_idx");

                    b.ToTable("spendings");
                });

            modelBuilder.Entity("QutebaApp_Data.Models.Account", b =>
                {
                    b.HasOne("QutebaApp_Data.Models.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("role_ID")
                        .IsRequired();
                });

            modelBuilder.Entity("QutebaApp_Data.Models.Profile", b =>
                {
                    b.HasOne("QutebaApp_Data.Models.Account", "User")
                        .WithOne("Profile")
                        .HasForeignKey("QutebaApp_Data.Models.Profile", "UserUid")
                        .HasConstraintName("user_UID")
                        .IsRequired();
                });

            modelBuilder.Entity("QutebaApp_Data.Models.Spending", b =>
                {
                    b.HasOne("QutebaApp_Data.Models.Category", "Category")
                        .WithMany("Spendings")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("category_ID")
                        .IsRequired();

                    b.HasOne("QutebaApp_Data.Models.Profile", "User")
                        .WithMany("Spendings")
                        .HasForeignKey("UserUid")
                        .HasConstraintName("user_ID")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
