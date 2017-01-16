﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HoneymoonShop.Model.DressModels;
using HoneymoonShop.Model.AppointmentModels;
using HoneyMoonDB.Models;

namespace HoneymoonShop.Model
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        { }

        public DBContext()
            : base()
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dress>().ToTable("Dresses");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<DressProperty>().ToTable("DressProperties").
                HasKey(x => new { x.DressId, x.PropertyId });
            modelBuilder.Entity<DressCategory>().ToTable("DressCategories").
                HasKey(x => new { x.DressId, x.CategoryId });
            modelBuilder.Entity<Brand>().ToTable("Brands");
            modelBuilder.Entity<Property>().ToTable("Properties");
            modelBuilder.Entity<Image>().ToTable("Images").
                HasKey(x => new { x.DressId, x.DressURL });
            modelBuilder.Entity<Appointment>().ToTable("Appointments");
            modelBuilder.Entity<Jewelry>().ToTable("Jewelry");
            modelBuilder.Entity<DressJewelry>().ToTable("DressJewelry").
                HasKey(x => new {x.DressId, x.JewelryId});
            modelBuilder.Entity<Admin>().ToTable("Admins");

        }
        public virtual DbSet<Dress> Dresses { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<DressProperty> DressProperties { get; set; }
        public virtual DbSet<DressCategory> DressCategories { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Jewelry> Jewelry { get; set; } 
        public virtual DbSet<Admin> Admins { get; set; }

        public DbSet<Afspraak> Afspraak { get; set; }
        public DbSet<Afspraak> Datum { get; set; }
        public DbSet<Afspraak> Tijden { get; set; }


        public bool areDressesPresent()
        {
            List<Dress> dresses = Dresses.ToList();
            if (dresses.Count == 0)
            {
                return false;
            }
            return true;
        }

    }
}