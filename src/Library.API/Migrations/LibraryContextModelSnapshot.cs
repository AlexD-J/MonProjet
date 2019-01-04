using Library.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;

namespace Library.API.Migrations
{
    [DbContext(typeof(LibraryContext))]
    partial class LibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Library.API.Entities.UserAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("UserAccounts");
                });

            
            modelBuilder.Entity("Library.API.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 500);

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Library.API.Entities.ProductArticle", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<Guid>("ProductId");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasAnnotation("MaxLength", 100);

                b.Property<string>("Description")
                    .HasAnnotation("MaxLength", 500);

                b.HasKey("Id");

                b.HasIndex("ProductId");

                b.ToTable("ProductArticles");
            });

            modelBuilder.Entity("Library.API.Entities.ProductArticle", b =>
                {
                    b.HasOne("Library.API.Entities.Product", "Product")
                        .WithMany("ProductActicles")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
