using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HoneymoonShop.Model;
using HoneymoonShop.Model.AppointmentModels;
using HoneymoonShop.Model.DressModels;

namespace HoneymoonShop.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20170117153856_Afspraak-Dress")]
    partial class AfspraakDress
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HoneyMoonDB.Models.Afspraak", b =>
                {
                    b.Property<int>("AfspraakId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AfspraakDatum");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("HerhaalEmail");

                    b.Property<string>("Naam")
                        .IsRequired();

                    b.Property<bool>("Nieuwsbrief");

                    b.Property<int>("Telefoonnummer");

                    b.Property<int>("Tijd");

                    b.Property<DateTime>("TrouwDatum");

                    b.HasKey("AfspraakId");

                    b.HasIndex("Tijd");

                    b.ToTable("Afspraak");
                });

            modelBuilder.Entity("HoneymoonShop.Model.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("AdminId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("HoneymoonShop.Model.AppointmentModels.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AppointmentTime");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15);

                    b.Property<int>("Type");

                    b.Property<DateTime>("WeddingDate");

                    b.HasKey("AppointmentId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("HoneymoonShop.Model.BeschikbareTijden", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("tijd");

                    b.HasKey("ID");

                    b.ToTable("BeschikbareTijden");
                });

            modelBuilder.Entity("HoneymoonShop.Model.DressModels.Brand", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Name");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("HoneymoonShop.Model.DressModels.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("HoneymoonShop.Model.DressModels.Dress", b =>
                {
                    b.Property<int>("DressId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrandName")
                        .IsRequired();

                    b.Property<int>("Colors");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PriceIndication");

                    b.HasKey("DressId");

                    b.HasIndex("BrandName");

                    b.ToTable("Dresses");
                });

            modelBuilder.Entity("HoneymoonShop.Model.DressModels.DressCategory", b =>
                {
                    b.Property<int>("DressId");

                    b.Property<int>("CategoryId");

                    b.HasKey("DressId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("DressCategories");
                });

            modelBuilder.Entity("HoneymoonShop.Model.DressModels.DressJewelry", b =>
                {
                    b.Property<int>("DressId");

                    b.Property<int>("JewelryId");

                    b.HasKey("DressId", "JewelryId");

                    b.HasIndex("JewelryId");

                    b.ToTable("DressJewelry");
                });

            modelBuilder.Entity("HoneymoonShop.Model.DressModels.DressProperty", b =>
                {
                    b.Property<int>("DressId");

                    b.Property<int>("PropertyId");

                    b.Property<int?>("CategoryId");

                    b.HasKey("DressId", "PropertyId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PropertyId");

                    b.ToTable("DressProperties");
                });

            modelBuilder.Entity("HoneymoonShop.Model.DressModels.Image", b =>
                {
                    b.Property<int>("DressId");

                    b.Property<string>("DressURL");

                    b.HasKey("DressId", "DressURL");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("HoneymoonShop.Model.DressModels.Jewelry", b =>
                {
                    b.Property<int>("JewelryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImageURL");

                    b.Property<string>("Name");

                    b.HasKey("JewelryId");

                    b.HasIndex("Name");

                    b.ToTable("Jewelry");
                });

            modelBuilder.Entity("HoneymoonShop.Model.DressModels.Property", b =>
                {
                    b.Property<int>("PropertyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("Type");

                    b.HasKey("PropertyId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("HoneyMoonDB.Models.Afspraak", b =>
                {
                    b.HasOne("HoneymoonShop.Model.BeschikbareTijden", "tijdFK")
                        .WithMany()
                        .HasForeignKey("Tijd")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HoneymoonShop.Model.DressModels.Dress", b =>
                {
                    b.HasOne("HoneymoonShop.Model.DressModels.Brand", "Brand")
                        .WithMany("Dresses")
                        .HasForeignKey("BrandName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HoneymoonShop.Model.DressModels.DressCategory", b =>
                {
                    b.HasOne("HoneymoonShop.Model.DressModels.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HoneymoonShop.Model.DressModels.Dress", "Dress")
                        .WithMany("Categories")
                        .HasForeignKey("DressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HoneymoonShop.Model.DressModels.DressJewelry", b =>
                {
                    b.HasOne("HoneymoonShop.Model.DressModels.Dress", "Dress")
                        .WithMany("Jewelry")
                        .HasForeignKey("DressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HoneymoonShop.Model.DressModels.Jewelry", "Jewelry")
                        .WithMany("Dresses")
                        .HasForeignKey("JewelryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HoneymoonShop.Model.DressModels.DressProperty", b =>
                {
                    b.HasOne("HoneymoonShop.Model.DressModels.Category")
                        .WithMany("Dresses")
                        .HasForeignKey("CategoryId");

                    b.HasOne("HoneymoonShop.Model.DressModels.Dress", "Dress")
                        .WithMany("Properties")
                        .HasForeignKey("DressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HoneymoonShop.Model.DressModels.Property", "Property")
                        .WithMany("Dresses")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HoneymoonShop.Model.DressModels.Image", b =>
                {
                    b.HasOne("HoneymoonShop.Model.DressModels.Dress", "Dress")
                        .WithMany("Images")
                        .HasForeignKey("DressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HoneymoonShop.Model.DressModels.Jewelry", b =>
                {
                    b.HasOne("HoneymoonShop.Model.DressModels.Brand")
                        .WithMany("Sieraden")
                        .HasForeignKey("Name");
                });
        }
    }
}
