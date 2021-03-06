﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using SystemMenu.Model.Entities.Permission;

namespace SystemMenu.Model
{
    public class SystemMenuDBContext : DbContext
    {
        public SystemMenuDBContext()
        {
        }

        public SystemMenuDBContext(DbContextOptions<SystemMenuDBContext> options) : base(options)
        {

        }

        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Manager>().HasMany(c => c.Loginrecords).WithOne(c => c.Manager).HasForeignKey(c => c.Mid);
            modelBuilder.Entity<ManagerMenu>().HasKey(c => new { c.Mid, c.Meid }); //设置主键
            modelBuilder.Entity<ManagerMenu>().HasOne(c => c.Manager).WithMany(c => c.ManagerMenus).HasForeignKey(c =>c.Mid);
            modelBuilder.Entity<ManagerMenu>().HasOne(c => c.SystemMenu).WithMany(c => c.ManagerMenus).HasForeignKey(c => c.Meid);
            base.OnModelCreating(modelBuilder);
        }



        public DbSet<SystemMenuEntity> systemMenus { get; set; }
        public DbSet<Manager> managers { get; set; }
        public DbSet<Loginrecord> loginrecords { get; set; }
        public DbSet<ManagerMenu> managerMenus { get; set; }
    }
}
