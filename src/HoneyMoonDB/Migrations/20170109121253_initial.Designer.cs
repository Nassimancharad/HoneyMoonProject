using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HoneyMoonDB.Data;

namespace HoneyMoonDB.Migrations {
    [DbContext(typeof(HoneyMoonDbContext))]
    [Migration("20170109120311_initial")]
    partial class initial {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HoneyMoonDB.Models.Afspraak", b =>
            {
                b.Property<int>("AfspraakId")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime?>("Datum");

                b.Property<string>("Email")
                    .IsRequired();

                b.Property<string>("HerhaalEmail");

                b.Property<string>("Naam")
                    .IsRequired();

                b.Property<bool>("Nieuwsbrief");

                b.Property<int>("Telefoonnummer");

                b.Property<DateTime>("TrouwDatum");

                b.HasKey("AfspraakId");

                b.HasIndex("Datum");

                b.ToTable("Afspraak");
            });

            modelBuilder.Entity("HoneyMoonDB.Models.ApplicationUser", b =>
            {
                b.Property<string>("Id");

                b.Property<int>("AccessFailedCount");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken();

                b.Property<string>("Email")
                    .HasAnnotation("MaxLength", 256);

                b.Property<bool>("EmailConfirmed");

                b.Property<bool>("LockoutEnabled");

                b.Property<DateTimeOffset?>("LockoutEnd");

                b.Property<string>("NormalizedEmail")
                    .HasAnnotation("MaxLength", 256);

                b.Property<string>("NormalizedUserName")
                    .HasAnnotation("MaxLength", 256);

                b.Property<string>("PasswordHash");

                b.Property<string>("PhoneNumber");

                b.Property<bool>("PhoneNumberConfirmed");

                b.Property<string>("SecurityStamp");

                b.Property<bool>("TwoFactorEnabled");

                b.Property<string>("UserName")
                    .HasAnnotation("MaxLength", 256);

                b.HasKey("Id");

                b.HasIndex("NormalizedEmail")
                    .HasName("EmailIndex");

                b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasName("UserNameIndex");

                b.ToTable("AspNetUsers");
            });

            modelBuilder.Entity("HoneyMoonDB.Models.Datum", b =>
            {
                b.Property<DateTime>("datum");

                b.HasKey("datum");

                b.ToTable("Datum");
            });

            modelBuilder.Entity("HoneyMoonDB.Models.Tijden", b =>
            {
                b.Property<DateTime>("AfspraakTijd");

                b.Property<DateTime?>("Tijden");

                b.HasKey("AfspraakTijd");

                b.HasIndex("Tijden");

                b.ToTable("Tijden");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
            {
                b.Property<string>("Id");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken();

                b.Property<string>("Name")
                    .HasAnnotation("MaxLength", 256);

                b.Property<string>("NormalizedName")
                    .HasAnnotation("MaxLength", 256);

                b.HasKey("Id");

                b.HasIndex("NormalizedName")
                    .HasName("RoleNameIndex");

                b.ToTable("AspNetRoles");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("ClaimType");

                b.Property<string>("ClaimValue");

                b.Property<string>("RoleId")
                    .IsRequired();

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.ToTable("AspNetRoleClaims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("ClaimType");

                b.Property<string>("ClaimValue");

                b.Property<string>("UserId")
                    .IsRequired();

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserClaims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
            {
                b.Property<string>("LoginProvider");

                b.Property<string>("ProviderKey");

                b.Property<string>("ProviderDisplayName");

                b.Property<string>("UserId")
                    .IsRequired();

                b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserLogins");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
            {
                b.Property<string>("UserId");

                b.Property<string>("RoleId");

                b.HasKey("UserId", "RoleId");

                b.HasIndex("RoleId");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserRoles");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
            {
                b.Property<string>("UserId");

                b.Property<string>("LoginProvider");

                b.Property<string>("Name");

                b.Property<string>("Value");

                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("AspNetUserTokens");
            });

            modelBuilder.Entity("HoneyMoonDB.Models.Afspraak", b =>
            {
                b.HasOne("HoneyMoonDB.Models.Datum", "AfspraakDatum")
                    .WithMany()
                    .HasForeignKey("Datum");
            });

            modelBuilder.Entity("HoneyMoonDB.Models.Tijden", b =>
            {
                b.HasOne("HoneyMoonDB.Models.Datum")
                    .WithMany("AfspraakTijd")
                    .HasForeignKey("Tijden");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                    .WithMany("Claims")
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
            {
                b.HasOne("HoneyMoonDB.Models.ApplicationUser")
                    .WithMany("Claims")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
            {
                b.HasOne("HoneyMoonDB.Models.ApplicationUser")
                    .WithMany("Logins")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                    .WithMany("Users")
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("HoneyMoonDB.Models.ApplicationUser")
                    .WithMany("Roles")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
