using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Open_School_Library.Data;

namespace OpenSchoolLibrary.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20161025194139_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Open_School_Library.Data.Entities.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<int>("DeweyID");

                    b.Property<int>("GenreID");

                    b.Property<int>("ISBN");

                    b.Property<string>("SubTitle");

                    b.Property<string>("Title");

                    b.HasKey("BookId");

                    b.HasIndex("DeweyID");

                    b.HasIndex("GenreID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Open_School_Library.Data.Entities.BookLoan", b =>
                {
                    b.Property<int>("BookLoanID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookID");

                    b.Property<DateTime>("CheckedOutOn");

                    b.Property<DateTime>("DueOn");

                    b.Property<DateTime?>("ReturnedOn");

                    b.Property<int?>("StudentID");

                    b.HasKey("BookLoanID");

                    b.HasIndex("BookID");

                    b.HasIndex("StudentID");

                    b.ToTable("BookLoans");
                });

            modelBuilder.Entity("Open_School_Library.Data.Entities.Dewey", b =>
                {
                    b.Property<int>("DeweyID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<float>("Number");

                    b.HasKey("DeweyID");

                    b.ToTable("Deweys");
                });

            modelBuilder.Entity("Open_School_Library.Data.Entities.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Open_School_Library.Data.Entities.Setting", b =>
                {
                    b.Property<int>("SettingID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AreFinesEnabled");

                    b.Property<int>("CheckoutDurationInDays");

                    b.Property<decimal?>("FineAmountPerDay")
                        .HasColumnType("Money");

                    b.HasKey("SettingID");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Open_School_Library.Data.Entities.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<decimal?>("Fines")
                        .HasColumnType("Money");

                    b.Property<string>("FirstName");

                    b.Property<int>("Grade");

                    b.Property<int?>("IssuedID");

                    b.Property<string>("LastName");

                    b.Property<int>("TeacherID");

                    b.HasKey("StudentID");

                    b.HasIndex("TeacherID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Open_School_Library.Data.Entities.Teacher", b =>
                {
                    b.Property<int>("TeacherID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<int>("Grade");

                    b.Property<string>("LastName");

                    b.HasKey("TeacherID");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Open_School_Library.Data.Entities.Book", b =>
                {
                    b.HasOne("Open_School_Library.Data.Entities.Dewey", "Dewey")
                        .WithMany()
                        .HasForeignKey("DeweyID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Open_School_Library.Data.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Open_School_Library.Data.Entities.BookLoan", b =>
                {
                    b.HasOne("Open_School_Library.Data.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Open_School_Library.Data.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentID");
                });

            modelBuilder.Entity("Open_School_Library.Data.Entities.Student", b =>
                {
                    b.HasOne("Open_School_Library.Data.Entities.Teacher", "Teacher")
                        .WithMany("Students")
                        .HasForeignKey("TeacherID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
