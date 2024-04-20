﻿// <auto-generated />
using System;
using Base.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Base.Service.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20240420122216_init2")]
    partial class init2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Base.Core.Domain.Category", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Name")
                        .HasName("pk_categories");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("Base.Core.Domain.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<double?>("Cost")
                        .HasColumnType("double precision")
                        .HasColumnName("cost");

                    b.Property<long>("CreatorId")
                        .HasColumnType("bigint")
                        .HasColumnName("creator_id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("location");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("StartDT")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_dt");

                    b.HasKey("Id")
                        .HasName("pk_events");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("ix_events_creator_id");

                    b.ToTable("events", (string)null);
                });

            modelBuilder.Entity("Base.Core.Domain.EventGuest", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.HasKey("UserId", "EventId")
                        .HasName("pk_event_guests");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_event_guests_event_id");

                    b.ToTable("event_guests", (string)null);
                });

            modelBuilder.Entity("Base.Core.Domain.RefreshToken", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("text")
                        .HasColumnName("token");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expiration");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("boolean")
                        .HasColumnName("is_used");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Token")
                        .HasName("pk_refresh_tokens");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_refresh_tokens_user_id");

                    b.ToTable("refresh_tokens", (string)null);
                });

            modelBuilder.Entity("Base.Core.Domain.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<Guid?>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean")
                        .HasColumnName("is_blocked");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nickname");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<bool>("TwoFactorAuth")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_auth");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasAlternateKey("Login")
                        .HasName("ak_users_login");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_users_event_id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("CategoryEvent", b =>
                {
                    b.Property<string>("CategoryName")
                        .HasColumnType("text")
                        .HasColumnName("category_name");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.HasKey("CategoryName", "EventId")
                        .HasName("pk_category_event");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_category_event_event_id");

                    b.ToTable("category_event", (string)null);
                });

            modelBuilder.Entity("CategoryUser", b =>
                {
                    b.Property<string>("CategoriesName")
                        .HasColumnType("text")
                        .HasColumnName("categories_name");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("CategoriesName", "UserId")
                        .HasName("pk_category_user");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_category_user_user_id");

                    b.ToTable("category_user", (string)null);
                });

            modelBuilder.Entity("Base.Core.Domain.Event", b =>
                {
                    b.HasOne("Base.Core.Domain.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_events_users_creator_id");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Base.Core.Domain.EventGuest", b =>
                {
                    b.HasOne("Base.Core.Domain.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_guests_events_event_id");

                    b.HasOne("Base.Core.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_guests_users_user_id");

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Base.Core.Domain.RefreshToken", b =>
                {
                    b.HasOne("Base.Core.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_refresh_tokens_users_user_id");
                });

            modelBuilder.Entity("Base.Core.Domain.User", b =>
                {
                    b.HasOne("Base.Core.Domain.Event", null)
                        .WithMany("Guests")
                        .HasForeignKey("EventId")
                        .HasConstraintName("fk_users_events_event_id");
                });

            modelBuilder.Entity("CategoryEvent", b =>
                {
                    b.HasOne("Base.Core.Domain.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_category_event_categories_category_name");

                    b.HasOne("Base.Core.Domain.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_category_event_events_event_id");
                });

            modelBuilder.Entity("CategoryUser", b =>
                {
                    b.HasOne("Base.Core.Domain.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_category_user_categories_categories_name");

                    b.HasOne("Base.Core.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_category_user_users_user_id");
                });

            modelBuilder.Entity("Base.Core.Domain.Event", b =>
                {
                    b.Navigation("Guests");
                });
#pragma warning restore 612, 618
        }
    }
}
