﻿// <auto-generated />
using System;
using Client.Core.Shared.Api.LocalDatabase.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Client.Core.Shared.Api.LocalDatabase.Migrations
{
    [DbContext(typeof(ClientEatCalculatorDbContext))]
    partial class ClientEatCalculatorDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("Client.Core.Shared.Api.Models.Day", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CarbohydrateMealCount")
                        .HasColumnType("INTEGER");

                    b.Property<double>("CarbohydratePercentages")
                        .HasColumnType("REAL");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("FatMealCount")
                        .HasColumnType("INTEGER");

                    b.Property<double>("FatPercentages")
                        .HasColumnType("REAL");

                    b.Property<double>("Kilocalories")
                        .HasColumnType("REAL");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProteinMealCount")
                        .HasColumnType("INTEGER");

                    b.Property<double>("ProteinPercentages")
                        .HasColumnType("REAL");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("Client.Core.Shared.Api.Models.DayDateBind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("DayId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.ToTable("DayDateBinds");
                });

            modelBuilder.Entity("Client.Core.Shared.Api.Models.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DayId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("Client.Core.Shared.Api.Models.Portion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("CarbohydratePercentages")
                        .HasColumnType("REAL");

                    b.Property<double>("FatPercentages")
                        .HasColumnType("REAL");

                    b.Property<int>("MealId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("ProteinPercentages")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.HasIndex("ProductId");

                    b.ToTable("Portions");
                });

            modelBuilder.Entity("Client.Core.Shared.Api.Models.Product", b =>
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

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Protein")
                        .HasColumnType("REAL");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Carbohydrate = 20.600000000000001,
                            Fat = 1.1000000000000001,
                            Grams = 100.0,
                            Order = 1,
                            Protein = 4.2000000000000002,
                            Title = "Гречка"
                        },
                        new
                        {
                            Id = 2,
                            Carbohydrate = 24.690000000000001,
                            Fat = 0.5,
                            Grams = 100.0,
                            Order = 2,
                            Protein = 2.2000000000000002,
                            Title = "Рис"
                        },
                        new
                        {
                            Id = 3,
                            Carbohydrate = 0.59999999999999998,
                            Fat = 8.1999999999999993,
                            Grams = 100.0,
                            Order = 3,
                            Protein = 21.0,
                            Title = "Курица"
                        },
                        new
                        {
                            Id = 4,
                            Carbohydrate = 0.0,
                            Fat = 7.2999999999999998,
                            Grams = 100.0,
                            Order = 4,
                            Protein = 26.800000000000001,
                            Title = "Индейка"
                        },
                        new
                        {
                            Id = 5,
                            Carbohydrate = 3.0,
                            Fat = 5.0,
                            Grams = 100.0,
                            Order = 5,
                            Protein = 16.0,
                            Title = "Творог"
                        },
                        new
                        {
                            Id = 6,
                            Carbohydrate = 5.7000000000000002,
                            Fat = 6.2000000000000002,
                            Grams = 100.0,
                            Order = 6,
                            Protein = 80.299999999999997,
                            Title = "Протеин"
                        },
                        new
                        {
                            Id = 7,
                            Carbohydrate = 0.0,
                            Fat = 0.90000000000000002,
                            Grams = 100.0,
                            Order = 7,
                            Protein = 15.9,
                            Title = "Минтай"
                        },
                        new
                        {
                            Id = 8,
                            Carbohydrate = 61.799999999999997,
                            Fat = 6.2000000000000002,
                            Grams = 100.0,
                            Order = 8,
                            Protein = 12.300000000000001,
                            Title = "Овсянка"
                        },
                        new
                        {
                            Id = 9,
                            Carbohydrate = 0.0,
                            Fat = 99.799999999999997,
                            Grams = 100.0,
                            Order = 9,
                            Protein = 0.0,
                            Title = "Масло льняное"
                        },
                        new
                        {
                            Id = 10,
                            Carbohydrate = 0.0,
                            Fat = 99.799999999999997,
                            Grams = 100.0,
                            Order = 10,
                            Protein = 0.0,
                            Title = "Масло оливковое"
                        },
                        new
                        {
                            Id = 11,
                            Carbohydrate = 0.69999999999999996,
                            Fat = 11.5,
                            Grams = 100.0,
                            Order = 11,
                            Protein = 12.699999999999999,
                            Title = "Яйцо"
                        },
                        new
                        {
                            Id = 12,
                            Carbohydrate = 21.399999999999999,
                            Fat = 0.69999999999999996,
                            Grams = 100.0,
                            Order = 12,
                            Protein = 3.6000000000000001,
                            Title = "Макароны из твёрдых сортов"
                        },
                        new
                        {
                            Id = 13,
                            Carbohydrate = 68.0,
                            Fat = 1.5,
                            Grams = 100.0,
                            Order = 13,
                            Protein = 13.0,
                            Title = "Булгур"
                        });
                });

            modelBuilder.Entity("Client.Core.Shared.Api.Models.DayDateBind", b =>
                {
                    b.HasOne("Client.Core.Shared.Api.Models.Day", "Day")
                        .WithMany("DayDateBinds")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Day");
                });

            modelBuilder.Entity("Client.Core.Shared.Api.Models.Meal", b =>
                {
                    b.HasOne("Client.Core.Shared.Api.Models.Day", null)
                        .WithMany("Meals")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Client.Core.Shared.Api.Models.Portion", b =>
                {
                    b.HasOne("Client.Core.Shared.Api.Models.Meal", null)
                        .WithMany("Portions")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Client.Core.Shared.Api.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Client.Core.Shared.Api.Models.Day", b =>
                {
                    b.Navigation("DayDateBinds");

                    b.Navigation("Meals");
                });

            modelBuilder.Entity("Client.Core.Shared.Api.Models.Meal", b =>
                {
                    b.Navigation("Portions");
                });
#pragma warning restore 612, 618
        }
    }
}
