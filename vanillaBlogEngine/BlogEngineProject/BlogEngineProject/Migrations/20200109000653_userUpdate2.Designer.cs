﻿// <auto-generated />
using System;
using BlogEngineProject.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlogEngineProject.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200109000653_userUpdate2")]
    partial class userUpdate2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlogEngineProject.Models.Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("DatePublished");

                    b.Property<int?>("PostID");

                    b.Property<int?>("UserID");

                    b.Property<string>("Username");

                    b.HasKey("CommentID");

                    b.HasIndex("PostID");

                    b.HasIndex("UserID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("BlogEngineProject.Models.Post", b =>
                {
                    b.Property<int>("PostID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<string>("ImageURL");

                    b.Property<int?>("ThreadID");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("Title");

                    b.HasKey("PostID");

                    b.HasIndex("ThreadID");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("BlogEngineProject.Models.Thread", b =>
                {
                    b.Property<int>("ThreadID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Category");

                    b.Property<string>("CreatorName");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ProfilePicURL");

                    b.Property<int?>("UserID");

                    b.HasKey("ThreadID");

                    b.HasIndex("UserID");

                    b.ToTable("Threads");
                });

            modelBuilder.Entity("BlogEngineProject.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConfirmPassword")
                        .IsRequired();

                    b.Property<DateTime>("DateJoined");

                    b.Property<string>("Gender");

                    b.Property<int?>("OwnedThreadThreadID");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("UserID");

                    b.HasIndex("OwnedThreadThreadID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BlogEngineProject.Models.Comment", b =>
                {
                    b.HasOne("BlogEngineProject.Models.Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostID");

                    b.HasOne("BlogEngineProject.Models.User")
                        .WithMany("CommentHistory")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("BlogEngineProject.Models.Post", b =>
                {
                    b.HasOne("BlogEngineProject.Models.Thread")
                        .WithMany("Posts")
                        .HasForeignKey("ThreadID");
                });

            modelBuilder.Entity("BlogEngineProject.Models.Thread", b =>
                {
                    b.HasOne("BlogEngineProject.Models.User")
                        .WithMany("FavoriteThreads")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("BlogEngineProject.Models.User", b =>
                {
                    b.HasOne("BlogEngineProject.Models.Thread", "OwnedThread")
                        .WithMany()
                        .HasForeignKey("OwnedThreadThreadID");
                });
#pragma warning restore 612, 618
        }
    }
}
