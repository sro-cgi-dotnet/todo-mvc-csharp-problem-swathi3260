﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoApi1.Models;

namespace TodoApi1.Migrations
{
    [DbContext(typeof(TodoContext))]
    partial class TodoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TodoApi1.Models.CheckListItem", b =>
                {
                    b.Property<int>("checkId");

                    b.Property<bool>("Checked");

                    b.Property<string>("Name");

                    b.Property<int>("NoteId");

                    b.HasKey("checkId");

                    b.HasIndex("NoteId");

                    b.ToTable("CheckLists");
                });

            modelBuilder.Entity("TodoApi1.Models.Note", b =>
                {
                    b.Property<int>("NoteId");

                    b.Property<string>("Label");

                    b.Property<bool>("Pinned");

                    b.Property<string>("PlainText");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("NoteId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("TodoApi1.Models.CheckListItem", b =>
                {
                    b.HasOne("TodoApi1.Models.Note")
                        .WithMany("CheckList")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}