﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SystemMenu.Model;

namespace SystemMenu.Model.Migrations
{
    [DbContext(typeof(SystemMenuDBContext))]
    [Migration("20201023143922_text1")]
    partial class text1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SystemMenu.Model.Entities.Permission.Loginrecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("COMport")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("IPconfig")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Mid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Mid");

                    b.ToTable("bee_login_record");
                });

            modelBuilder.Entity("SystemMenu.Model.Entities.Permission.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Account")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsEnable")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("bee_manager");
                });

            modelBuilder.Entity("SystemMenu.Model.Entities.Permission.ManagerMenu", b =>
                {
                    b.Property<int>("Mid")
                        .HasColumnType("int");

                    b.Property<int>("Meid")
                        .HasColumnType("int");

                    b.HasKey("Mid", "Meid");

                    b.HasIndex("Meid");

                    b.ToTable("bee_managerMenu");
                });

            modelBuilder.Entity("SystemMenu.Model.Entities.Permission.SystemMenuEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Href")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pid")
                        .HasColumnType("int");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Target")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("bee_system_menu");
                });

            modelBuilder.Entity("SystemMenu.Model.Entities.Permission.Loginrecord", b =>
                {
                    b.HasOne("SystemMenu.Model.Entities.Permission.Manager", "Manager")
                        .WithMany("Loginrecords")
                        .HasForeignKey("Mid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SystemMenu.Model.Entities.Permission.ManagerMenu", b =>
                {
                    b.HasOne("SystemMenu.Model.Entities.Permission.SystemMenuEntity", "SystemMenu")
                        .WithMany("ManagerMenus")
                        .HasForeignKey("Meid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SystemMenu.Model.Entities.Permission.Manager", "Manager")
                        .WithMany("ManagerMenus")
                        .HasForeignKey("Mid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
