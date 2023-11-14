﻿// <auto-generated />
using System;
using Manero.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Manero.Migrations.Data
{
    [DbContext(typeof(DataContext))]
    [Migration("20231114085341_Fixed foreignkey and created date reviews")]
    partial class Fixedforeignkeyandcreateddatereviews
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.CategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Dresses"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Pants"
                        },
                        new
                        {
                            Id = 3,
                            Category = "Accessories"
                        },
                        new
                        {
                            Id = 4,
                            Category = "Shoes"
                        },
                        new
                        {
                            Id = 5,
                            Category = "T-shirts"
                        });
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.ColorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Colors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Color = "Red"
                        },
                        new
                        {
                            Id = 2,
                            Color = "Blue"
                        },
                        new
                        {
                            Id = 3,
                            Color = "Yellow"
                        },
                        new
                        {
                            Id = 4,
                            Color = "Green"
                        },
                        new
                        {
                            Id = 5,
                            Color = "Black"
                        });
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.ImageEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsMainImage")
                        .HasColumnType("bit");

                    b.Property<string>("ProductArticleNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProductArticleNumber");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.ProductCategoryEntity", b =>
                {
                    b.Property<string>("ArticleNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("ArticleNumber", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.ProductColorEntity", b =>
                {
                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<string>("ArticleNumber")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ColorId", "ArticleNumber");

                    b.HasIndex("ArticleNumber");

                    b.ToTable("ProductColors");
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.ProductEntity", b =>
                {
                    b.Property<string>("ArticleNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("ProductDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ArticleNumber");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.ProductSizeEntity", b =>
                {
                    b.Property<int>("SizeId")
                        .HasColumnType("int");

                    b.Property<string>("ArticleNumber")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SizeId", "ArticleNumber");

                    b.HasIndex("ArticleNumber");

                    b.ToTable("ProductSizes");
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.ProductTagEntity", b =>
                {
                    b.Property<string>("ArticleNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("ArticleNumber", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ProductTags");
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.ReviewEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArticleNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Reviewer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArticleNumber");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.SizeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sizes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Size = "XS"
                        },
                        new
                        {
                            Id = 2,
                            Size = "S"
                        },
                        new
                        {
                            Id = 3,
                            Size = "M"
                        },
                        new
                        {
                            Id = 4,
                            Size = "L"
                        },
                        new
                        {
                            Id = 5,
                            Size = "XL"
                        },
                        new
                        {
                            Id = 6,
                            Size = "XXL"
                        });
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.TagEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Tag = "Featured Products"
                        },
                        new
                        {
                            Id = 2,
                            Tag = "Best Sellers"
                        },
                        new
                        {
                            Id = 3,
                            Tag = "Sale"
                        },
                        new
                        {
                            Id = 4,
                            Tag = "New"
                        });
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.ImageEntity", b =>
                {
                    b.HasOne("Manero.Models.Entities.ProductEntities.ProductEntity", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductArticleNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.ProductCategoryEntity", b =>
                {
                    b.HasOne("Manero.Models.Entities.ProductEntities.ProductEntity", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ArticleNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Manero.Models.Entities.ProductEntities.CategoryEntity", "Category")
                        .WithMany("CategoryProducts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.ProductColorEntity", b =>
                {
                    b.HasOne("Manero.Models.Entities.ProductEntities.ProductEntity", "Product")
                        .WithMany("ProductColors")
                        .HasForeignKey("ArticleNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Manero.Models.Entities.ProductEntities.ColorEntity", "Color")
                        .WithMany("ColorsProducts")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.ProductSizeEntity", b =>
                {
                    b.HasOne("Manero.Models.Entities.ProductEntities.ProductEntity", "Product")
                        .WithMany("ProductSizes")
                        .HasForeignKey("ArticleNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Manero.Models.Entities.ProductEntities.SizeEntity", "Size")
                        .WithMany("SizeProducts")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.ProductTagEntity", b =>
                {
                    b.HasOne("Manero.Models.Entities.ProductEntities.ProductEntity", "Product")
                        .WithMany("ProductTags")
                        .HasForeignKey("ArticleNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Manero.Models.Entities.ProductEntities.TagEntity", "Tag")
                        .WithMany("TagProducts")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.ReviewEntity", b =>
                {
                    b.HasOne("Manero.Models.Entities.ProductEntities.ProductEntity", "Product")
                        .WithMany("ProductReviews")
                        .HasForeignKey("ArticleNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.CategoryEntity", b =>
                {
                    b.Navigation("CategoryProducts");
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.ColorEntity", b =>
                {
                    b.Navigation("ColorsProducts");
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.ProductEntity", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("ProductCategories");

                    b.Navigation("ProductColors");

                    b.Navigation("ProductReviews");

                    b.Navigation("ProductSizes");

                    b.Navigation("ProductTags");
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.SizeEntity", b =>
                {
                    b.Navigation("SizeProducts");
                });

            modelBuilder.Entity("Manero.Models.Entities.ProductEntities.TagEntity", b =>
                {
                    b.Navigation("TagProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
