﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ShareBook.Infrastructure;

#nullable disable

namespace ShareBook.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ShareBook.Domain.Books.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Author")
                        .HasColumnType("text")
                        .HasColumnName("author");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Labels")
                        .HasColumnType("text")
                        .HasColumnName("labels");

                    b.Property<string>("Owner")
                        .HasColumnType("text")
                        .HasColumnName("owner");

                    b.Property<int>("Pages")
                        .HasColumnType("integer")
                        .HasColumnName("pages");

                    b.Property<bool>("SharedByOwner")
                        .HasColumnType("boolean")
                        .HasColumnName("shared_by_owner");

                    b.Property<string>("Title")
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_books");

                    b.ToTable("books", (string)null);
                });

            modelBuilder.Entity("ShareBook.Domain.Shippings.Shipping", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid")
                        .HasColumnName("book_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.HasKey("Id")
                        .HasName("pk_shippings");

                    b.HasIndex("BookId")
                        .HasDatabaseName("ix_shippings_book_id");

                    b.ToTable("shippings", (string)null);
                });

            modelBuilder.Entity("ShareBook.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("ShareBook.Domain.Books.Book", b =>
                {
                    b.OwnsOne("ShareBook.Domain.Books.LoanRequest", "CurrentLoanRequest", b1 =>
                        {
                            b1.Property<Guid>("BookId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("current_loan_request_created_at");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("current_loan_request_id");

                            b1.Property<string>("RequestingUser")
                                .HasColumnType("text")
                                .HasColumnName("current_loan_request_requesting_user");

                            b1.Property<int>("Status")
                                .HasColumnType("integer")
                                .HasColumnName("current_loan_request_status");

                            b1.HasKey("BookId");

                            b1.ToTable("books");

                            b1.WithOwner()
                                .HasForeignKey("BookId")
                                .HasConstraintName("fk_books_books_id");
                        });

                    b.Navigation("CurrentLoanRequest");
                });

            modelBuilder.Entity("ShareBook.Domain.Shippings.Shipping", b =>
                {
                    b.HasOne("ShareBook.Domain.Books.Book", null)
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_shippings_books_book_id");
                });

            modelBuilder.Entity("ShareBook.Domain.Users.User", b =>
                {
                    b.OwnsOne("ShareBook.Domain.Shared.ValueObjects.Email", "_email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .HasColumnType("text")
                                .HasColumnName("Email");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId")
                                .HasConstraintName("fk_users_users_id");
                        });

                    b.OwnsOne("ShareBook.Domain.Shared.ValueObjects.Password", "_password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("PasswordHash")
                                .HasColumnType("text")
                                .HasColumnName("Password");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId")
                                .HasConstraintName("fk_users_users_id");
                        });

                    b.Navigation("_email");

                    b.Navigation("_password");
                });
#pragma warning restore 612, 618
        }
    }
}
