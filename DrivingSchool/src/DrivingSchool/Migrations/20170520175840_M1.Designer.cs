using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DrivingSchool.Entities.Context;

namespace DrivingSchool.Migrations
{
    [DbContext(typeof(DrivingSchoolDbContext))]
    [Migration("20170520175840_M1")]
    partial class M1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DrivingSchool.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Brand");

                    b.Property<int>("Gearbox");

                    b.Property<string>("LicensePlate");

                    b.Property<DateTime>("ManufactureDate");

                    b.Property<int>("Mileage");

                    b.Property<string>("Model");

                    b.Property<int>("State");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("DrivingSchool.Entities.CarUsage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CarId");

                    b.Property<int?>("ClassUsedInId");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("InstructorId");

                    b.Property<int>("MileageAfter");

                    b.Property<int>("MileageBefore");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("ClassUsedInId")
                        .IsUnique();

                    b.HasIndex("InstructorId")
                        .IsUnique();

                    b.ToTable("CarUsages");
                });

            modelBuilder.Entity("DrivingSchool.Entities.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("EndTime");

                    b.Property<int?>("InstructorId");

                    b.Property<int?>("MarkId");

                    b.Property<DateTime>("StartTime");

                    b.Property<int>("State");

                    b.Property<int?>("StudentId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.HasIndex("MarkId")
                        .IsUnique();

                    b.HasIndex("StudentId");

                    b.ToTable("Classes");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Class");
                });

            modelBuilder.Entity("DrivingSchool.Entities.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<int?>("OwnerCarId");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("OwnerCarId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("DrivingSchool.Entities.Mark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClassMark");

                    b.Property<string>("Comment");

                    b.Property<int?>("StudentId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Marks");
                });

            modelBuilder.Entity("DrivingSchool.Entities.MileagePoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Mileage");

                    b.Property<string>("Name");

                    b.Property<int?>("OwnerCarId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerCarId");

                    b.ToTable("MileagePoints");
                });

            modelBuilder.Entity("DrivingSchool.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("FirstName");

                    b.Property<string>("IdentityUserId");

                    b.Property<string>("LastName");

                    b.Property<string>("PersonalNo");

                    b.Property<int>("State");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("IdentityUserId");

                    b.ToTable("GenericUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DrivingSchool.Entities.TheoryClasses", b =>
                {
                    b.HasBaseType("DrivingSchool.Entities.Class");

                    b.Property<bool>("IsMain");

                    b.Property<int?>("MainClassesId");

                    b.Property<int>("Seats");

                    b.Property<int>("Weeks");

                    b.HasIndex("MainClassesId");

                    b.ToTable("TheoryClasses");

                    b.HasDiscriminator().HasValue("TheoryClasses");
                });

            modelBuilder.Entity("DrivingSchool.Entities.Instructor", b =>
                {
                    b.HasBaseType("DrivingSchool.Entities.User");

                    b.Property<int?>("AssignedCarId");

                    b.HasIndex("AssignedCarId");

                    b.ToTable("Instructor");

                    b.HasDiscriminator().HasValue("Instructor");
                });

            modelBuilder.Entity("DrivingSchool.Entities.Student", b =>
                {
                    b.HasBaseType("DrivingSchool.Entities.User");

                    b.Property<bool>("HasTheoryClasses");

                    b.Property<int>("PracticeCount");

                    b.Property<int?>("TheoryClassesId");

                    b.HasIndex("TheoryClassesId");

                    b.ToTable("Student");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("DrivingSchool.Entities.CarUsage", b =>
                {
                    b.HasOne("DrivingSchool.Entities.Car", "Car")
                        .WithMany("CarUsages")
                        .HasForeignKey("CarId");

                    b.HasOne("DrivingSchool.Entities.Class", "ClassUsedIn")
                        .WithOne("CarUsage")
                        .HasForeignKey("DrivingSchool.Entities.CarUsage", "ClassUsedInId");

                    b.HasOne("DrivingSchool.Entities.Instructor", "Instructor")
                        .WithOne("CarUsages")
                        .HasForeignKey("DrivingSchool.Entities.CarUsage", "InstructorId");
                });

            modelBuilder.Entity("DrivingSchool.Entities.Class", b =>
                {
                    b.HasOne("DrivingSchool.Entities.Instructor", "Instructor")
                        .WithMany("TaughtClasses")
                        .HasForeignKey("InstructorId");

                    b.HasOne("DrivingSchool.Entities.Mark", "Mark")
                        .WithOne("GivenForClass")
                        .HasForeignKey("DrivingSchool.Entities.Class", "MarkId");

                    b.HasOne("DrivingSchool.Entities.Student", "Student")
                        .WithMany("AttendedClasses")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("DrivingSchool.Entities.Document", b =>
                {
                    b.HasOne("DrivingSchool.Entities.Car", "OwnerCar")
                        .WithMany("Documents")
                        .HasForeignKey("OwnerCarId");
                });

            modelBuilder.Entity("DrivingSchool.Entities.Mark", b =>
                {
                    b.HasOne("DrivingSchool.Entities.Student", "Student")
                        .WithMany("ExamMarks")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("DrivingSchool.Entities.MileagePoint", b =>
                {
                    b.HasOne("DrivingSchool.Entities.Car", "OwnerCar")
                        .WithMany("MileagePoints")
                        .HasForeignKey("OwnerCarId");
                });

            modelBuilder.Entity("DrivingSchool.Entities.User", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser", "IdentityUser")
                        .WithMany()
                        .HasForeignKey("IdentityUserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DrivingSchool.Entities.TheoryClasses", b =>
                {
                    b.HasOne("DrivingSchool.Entities.TheoryClasses", "MainClasses")
                        .WithMany("AdditionalClasses")
                        .HasForeignKey("MainClassesId");
                });

            modelBuilder.Entity("DrivingSchool.Entities.Instructor", b =>
                {
                    b.HasOne("DrivingSchool.Entities.Car", "AssignedCar")
                        .WithMany("InstructorsWithAccess")
                        .HasForeignKey("AssignedCarId");
                });

            modelBuilder.Entity("DrivingSchool.Entities.Student", b =>
                {
                    b.HasOne("DrivingSchool.Entities.TheoryClasses", "TheoryClasses")
                        .WithMany("Students")
                        .HasForeignKey("TheoryClassesId");
                });
        }
    }
}
