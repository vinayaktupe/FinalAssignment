// <auto-generated />
using System;
using FinalAssignment.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinalAssignment.DAL.Migrations.UserDb
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20210923061019_UserTables")]
    partial class UserTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FinalAssignment.DAL.Data.Models.Crew", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 9, 23, 11, 40, 19, 712, DateTimeKind.Local).AddTicks(9209));

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Crews");
                });

            modelBuilder.Entity("FinalAssignment.DAL.Data.Models.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 9, 23, 11, 40, 19, 712, DateTimeKind.Local).AddTicks(3815));

                    b.Property<int>("EmployeeType")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SupervisorID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("SupervisorID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("FinalAssignment.DAL.Data.Models.Minute", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ApprovalHistory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("ApprovalStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 9, 23, 11, 40, 19, 713, DateTimeKind.Local).AddTicks(4546));

                    b.Property<int>("CrewID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<int>("MinuteType")
                        .HasColumnType("int");

                    b.Property<int>("Topic")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("CrewID");

                    b.ToTable("Minutes");
                });

            modelBuilder.Entity("FinalAssignment.DAL.Data.Models.Supervisor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MinuteID")
                        .HasColumnType("int");

                    b.Property<int>("SupervisorID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MinuteID");

                    b.ToTable("Supervisor");
                });

            modelBuilder.Entity("FinalAssignment.DAL.Data.Models.Employee", b =>
                {
                    b.HasOne("FinalAssignment.DAL.Data.Models.Minute", "Minutes")
                        .WithMany("Employees")
                        .HasForeignKey("SupervisorID");
                });

            modelBuilder.Entity("FinalAssignment.DAL.Data.Models.Minute", b =>
                {
                    b.HasOne("FinalAssignment.DAL.Data.Models.Crew", "Crews")
                        .WithMany("Minutes")
                        .HasForeignKey("CrewID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinalAssignment.DAL.Data.Models.Supervisor", b =>
                {
                    b.HasOne("FinalAssignment.DAL.Data.Models.Minute", null)
                        .WithMany("SupervisorID")
                        .HasForeignKey("MinuteID");
                });
#pragma warning restore 612, 618
        }
    }
}
