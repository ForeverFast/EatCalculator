﻿// <auto-generated />
using EatCalculator.UI.Shared.Api.LocalDatabase.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EatCalculator.UI.Shared.Api.LocalDatabase.Migrations
{
    [DbContext(typeof(EatCalculatorDbContext))]
    [Migration("20230305202009_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("EatCalculator.UI.Shared.Api.Models.Day", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("CarbohydratePercentages")
                        .HasColumnType("REAL");

                    b.Property<int>("CarbohydratePortions")
                        .HasColumnType("INTEGER");

                    b.Property<double>("CarbohydrateTotal")
                        .HasColumnType("REAL");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<double>("FatPercentages")
                        .HasColumnType("REAL");

                    b.Property<int>("FatPortions")
                        .HasColumnType("INTEGER");

                    b.Property<double>("FatTotal")
                        .HasColumnType("REAL");

                    b.Property<int>("MealCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<double>("ProteinPercentages")
                        .HasColumnType("REAL");

                    b.Property<int>("ProteinPortions")
                        .HasColumnType("INTEGER");

                    b.Property<double>("ProteinTotal")
                        .HasColumnType("REAL");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("EatCalculator.UI.Shared.Api.Models.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DayId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("EatCalculator.UI.Shared.Api.Models.Portion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Grams")
                        .HasColumnType("REAL");

                    b.Property<int>("MealId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.HasIndex("ProductId");

                    b.ToTable("Portions");
                });

            modelBuilder.Entity("EatCalculator.UI.Shared.Api.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Carbohydrate")
                        .HasColumnType("REAL");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<double>("Fat")
                        .HasColumnType("REAL");

                    b.Property<double>("Grams")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Protein")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("EatCalculator.UI.Shared.Api.Models.Meal", b =>
                {
                    b.HasOne("EatCalculator.UI.Shared.Api.Models.Day", null)
                        .WithMany("Meals")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EatCalculator.UI.Shared.Api.Models.Portion", b =>
                {
                    b.HasOne("EatCalculator.UI.Shared.Api.Models.Meal", null)
                        .WithMany("Portions")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EatCalculator.UI.Shared.Api.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EatCalculator.UI.Shared.Api.Models.Day", b =>
                {
                    b.Navigation("Meals");
                });

            modelBuilder.Entity("EatCalculator.UI.Shared.Api.Models.Meal", b =>
                {
                    b.Navigation("Portions");
                });
#pragma warning restore 612, 618
        }
    }
}
