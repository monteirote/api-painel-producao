﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api_painel_producao.Data;

#nullable disable

namespace api_painel_producao.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("api_painel_producao.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DeactivatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeactivatedById")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("LastModifiedById")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("DeactivatedById");

                    b.HasIndex("LastModifiedById");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("api_painel_producao.Models.FramedArtwork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BackgroundId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FrameId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GlassId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Height")
                        .HasColumnType("REAL");

                    b.Property<string>("ImageDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("ImageFile")
                        .HasColumnType("BLOB");

                    b.Property<string>("ImageFilePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PaperId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("TEXT");

                    b.Property<float>("Width")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("BackgroundId");

                    b.HasIndex("FrameId");

                    b.HasIndex("GlassId");

                    b.HasIndex("OrderId");

                    b.HasIndex("PaperId");

                    b.ToTable("FramedArtworks");
                });

            modelBuilder.Entity("api_painel_producao.Models.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MeasurementUnit")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("ValueByUnit")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("api_painel_producao.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CanceledAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CanceledById")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CreatedForId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCanceled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("LastModifiedById")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CanceledById");

                    b.HasIndex("CreatedById");

                    b.HasIndex("CreatedForId");

                    b.HasIndex("LastModifiedById");

                    b.HasIndex("Reference")
                        .IsUnique();

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("api_painel_producao.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DataLastModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DataLastModifiedById")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("StatusLastModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("StatusLastModifiedById")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)");

                    b.HasKey("Id");

                    b.HasIndex("DataLastModifiedById");

                    b.HasIndex("StatusLastModifiedById");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("api_painel_producao.Models.Customer", b =>
                {
                    b.HasOne("api_painel_producao.Models.User", "CreatedBy")
                        .WithMany("CreatedCustomers")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("api_painel_producao.Models.User", "DeactivatedBy")
                        .WithMany("DeactivatedCustomers")
                        .HasForeignKey("DeactivatedById")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("api_painel_producao.Models.User", "LastModifiedBy")
                        .WithMany("ModifiedCustomers")
                        .HasForeignKey("LastModifiedById")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("CreatedBy");

                    b.Navigation("DeactivatedBy");

                    b.Navigation("LastModifiedBy");
                });

            modelBuilder.Entity("api_painel_producao.Models.FramedArtwork", b =>
                {
                    b.HasOne("api_painel_producao.Models.Material", "Background")
                        .WithMany()
                        .HasForeignKey("BackgroundId");

                    b.HasOne("api_painel_producao.Models.Material", "Frame")
                        .WithMany()
                        .HasForeignKey("FrameId");

                    b.HasOne("api_painel_producao.Models.Material", "Glass")
                        .WithMany()
                        .HasForeignKey("GlassId");

                    b.HasOne("api_painel_producao.Models.Order", "Order")
                        .WithMany("FramedArtworks")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("api_painel_producao.Models.Material", "Paper")
                        .WithMany()
                        .HasForeignKey("PaperId");

                    b.Navigation("Background");

                    b.Navigation("Frame");

                    b.Navigation("Glass");

                    b.Navigation("Order");

                    b.Navigation("Paper");
                });

            modelBuilder.Entity("api_painel_producao.Models.Order", b =>
                {
                    b.HasOne("api_painel_producao.Models.User", "CanceledBy")
                        .WithMany("CanceledOrders")
                        .HasForeignKey("CanceledById")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("api_painel_producao.Models.User", "CreatedBy")
                        .WithMany("CreatedOrders")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("api_painel_producao.Models.Customer", "CreatedFor")
                        .WithMany("Orders")
                        .HasForeignKey("CreatedForId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("api_painel_producao.Models.User", "LastModifiedBy")
                        .WithMany("ModifiedOrders")
                        .HasForeignKey("LastModifiedById")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("CanceledBy");

                    b.Navigation("CreatedBy");

                    b.Navigation("CreatedFor");

                    b.Navigation("LastModifiedBy");
                });

            modelBuilder.Entity("api_painel_producao.Models.User", b =>
                {
                    b.HasOne("api_painel_producao.Models.User", "DataLastModifiedBy")
                        .WithMany("ModifiedUsersData")
                        .HasForeignKey("DataLastModifiedById")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("api_painel_producao.Models.User", "StatusLastModifiedBy")
                        .WithMany("ModifiedUsersStatus")
                        .HasForeignKey("StatusLastModifiedById")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("DataLastModifiedBy");

                    b.Navigation("StatusLastModifiedBy");
                });

            modelBuilder.Entity("api_painel_producao.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("api_painel_producao.Models.Order", b =>
                {
                    b.Navigation("FramedArtworks");
                });

            modelBuilder.Entity("api_painel_producao.Models.User", b =>
                {
                    b.Navigation("CanceledOrders");

                    b.Navigation("CreatedCustomers");

                    b.Navigation("CreatedOrders");

                    b.Navigation("DeactivatedCustomers");

                    b.Navigation("ModifiedCustomers");

                    b.Navigation("ModifiedOrders");

                    b.Navigation("ModifiedUsersData");

                    b.Navigation("ModifiedUsersStatus");
                });
#pragma warning restore 612, 618
        }
    }
}
