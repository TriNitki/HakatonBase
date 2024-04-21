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
    [Migration("20240421033811_new_key")]
    partial class new_key
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

                    b.Property<bool>("IsModerated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_moderated");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("location");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("PublishedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("published_at");

                    b.Property<int>("Reward")
                        .HasColumnType("integer")
                        .HasColumnName("reward");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_at");

                    b.HasKey("Id")
                        .HasName("pk_events");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("ix_events_creator_id");

                    b.ToTable("events", (string)null);
                });

            modelBuilder.Entity("Base.Core.Domain.EventToCategory", b =>
                {
                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<string>("CategoryName")
                        .HasColumnType("text")
                        .HasColumnName("category_name");

                    b.HasKey("EventId", "CategoryName")
                        .HasName("pk_event_to_category");

                    b.HasIndex("CategoryName")
                        .HasDatabaseName("ix_event_to_category_category_name");

                    b.ToTable("event_to_category", (string)null);
                });

            modelBuilder.Entity("Base.Core.Domain.EventToUser", b =>
                {
                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.HasKey("EventId", "UserId")
                        .HasName("pk_event_to_user");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_event_to_user_user_id");

                    b.ToTable("event_to_user", (string)null);
                });

            modelBuilder.Entity("Base.Core.Domain.Merch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Image")
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<double>("Price")
                        .HasColumnType("double precision")
                        .HasColumnName("price");

                    b.Property<long>("Stock")
                        .HasColumnType("bigint")
                        .HasColumnName("stock");

                    b.HasKey("Id")
                        .HasName("pk_merch");

                    b.ToTable("merch", (string)null);
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

                    b.Property<double>("Points")
                        .HasColumnType("double precision")
                        .HasColumnName("points");

                    b.Property<bool>("TwoFactorAuth")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_auth");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasAlternateKey("Login")
                        .HasName("ak_users_login");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Base.Core.Domain.UserToCategory", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<string>("CategoryName")
                        .HasColumnType("text")
                        .HasColumnName("category_name");

                    b.HasKey("UserId", "CategoryName")
                        .HasName("pk_user_to_category");

                    b.HasIndex("CategoryName")
                        .HasDatabaseName("ix_user_to_category_category_name");

                    b.ToTable("user_to_category", (string)null);
                });

            modelBuilder.Entity("Base.Core.Domain.UserToMerch", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<Guid>("MerchId")
                        .HasColumnType("uuid")
                        .HasColumnName("merch_id");

                    b.Property<DateTime>("PurchasedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("purchased_at");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint")
                        .HasColumnName("amount");

                    b.HasKey("UserId", "MerchId", "PurchasedAt")
                        .HasName("pk_user_to_merch");

                    b.HasIndex("MerchId")
                        .HasDatabaseName("ix_user_to_merch_merch_id");

                    b.ToTable("user_to_merch", (string)null);
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

            modelBuilder.Entity("Base.Core.Domain.EventToCategory", b =>
                {
                    b.HasOne("Base.Core.Domain.Category", "Category")
                        .WithMany("EventToCategory")
                        .HasForeignKey("CategoryName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_to_category_categories_category_temp_id");

                    b.HasOne("Base.Core.Domain.Event", "Event")
                        .WithMany("EventToCategory")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_to_category_events_event_id");

                    b.Navigation("Category");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Base.Core.Domain.EventToUser", b =>
                {
                    b.HasOne("Base.Core.Domain.Event", "Event")
                        .WithMany("EventToUser")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_to_user_events_event_id");

                    b.HasOne("Base.Core.Domain.User", "User")
                        .WithMany("EventToUser")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_to_user_users_user_id");

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

            modelBuilder.Entity("Base.Core.Domain.UserToCategory", b =>
                {
                    b.HasOne("Base.Core.Domain.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_to_category_categories_category_temp_id1");

                    b.HasOne("Base.Core.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_to_category_users_user_id");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Base.Core.Domain.UserToMerch", b =>
                {
                    b.HasOne("Base.Core.Domain.Merch", "Merch")
                        .WithMany()
                        .HasForeignKey("MerchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_to_merch_merch_merch_id");

                    b.HasOne("Base.Core.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_to_merch_users_user_id");

                    b.Navigation("Merch");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Base.Core.Domain.Category", b =>
                {
                    b.Navigation("EventToCategory");
                });

            modelBuilder.Entity("Base.Core.Domain.Event", b =>
                {
                    b.Navigation("EventToCategory");

                    b.Navigation("EventToUser");
                });

            modelBuilder.Entity("Base.Core.Domain.User", b =>
                {
                    b.Navigation("EventToUser");
                });
#pragma warning restore 612, 618
        }
    }
}
