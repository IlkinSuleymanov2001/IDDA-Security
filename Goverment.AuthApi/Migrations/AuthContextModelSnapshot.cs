﻿// <auto-generated />
using System;
using Goverment.AuthApi.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    [DbContext(typeof(AuthContext))]
    partial class AuthContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Security.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Name = "USER"
                        },
                        new
                        {
                            Id = 1,
                            Name = "ADMIN"
                        });
                });

            modelBuilder.Entity("Core.Security.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConfirmToken")
                        .HasMaxLength(400)
                        .HasColumnType("text")
                        .HasColumnName("confirmtoken");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("firstname");

                    b.Property<bool>("IsVerify")
                        .HasMaxLength(20)
                        .HasColumnType("bit")
                        .HasColumnName("isverify");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("lastname");

                    b.Property<DateTime?>("OptCreatedDate")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2")
                        .HasColumnName("otpcreateddate");

                    b.Property<string>("OtpCode")
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)")
                        .HasColumnName("otpcode");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("passwordhash");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("passwordsalt");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "ilkinsuleymanov200@gmail.com",
                            FirstName = "Ilkin",
                            IsVerify = true,
                            LastName = "Suleymanov",
                            PasswordHash = new byte[] { 15, 209, 155, 50, 58, 102, 242, 134, 157, 176, 105, 230, 251, 95, 162, 204, 49, 37, 14, 7, 63, 3, 174, 232, 106, 50, 245, 150, 206, 143, 5, 190, 149, 8, 175, 57, 103, 236, 76, 148, 161, 204, 90, 4, 197, 109, 181, 106, 205, 22, 183, 120, 12, 31, 148, 95, 10, 65, 215, 101, 26, 219, 199, 125 },
                            PasswordSalt = new byte[] { 145, 92, 28, 164, 53, 248, 216, 235, 69, 9, 25, 57, 213, 168, 219, 206, 218, 117, 98, 44, 128, 68, 70, 247, 59, 15, 46, 244, 254, 179, 125, 165, 185, 63, 129, 230, 169, 243, 112, 181, 98, 141, 199, 27, 124, 221, 19, 49, 120, 110, 196, 105, 230, 252, 96, 129, 161, 115, 41, 78, 175, 96, 187, 230, 54, 109, 78, 122, 191, 223, 112, 79, 119, 106, 244, 40, 117, 117, 154, 209, 118, 41, 212, 8, 214, 246, 25, 155, 170, 21, 40, 31, 137, 209, 23, 250, 54, 121, 39, 70, 23, 202, 150, 118, 47, 40, 114, 215, 121, 243, 60, 231, 43, 213, 109, 200, 2, 127, 186, 136, 82, 217, 59, 193, 231, 248, 147, 139 },
                            Status = false
                        });
                });

            modelBuilder.Entity("Core.Security.Entities.UserLoginSecurity", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userid");

                    b.Property<DateTime?>("AccountBlockedTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("AccountBlockedTime");

                    b.Property<DateTime?>("AccountUnblockedTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("AccountUnBlockedTime");

                    b.Property<bool>("IsAccountBlock")
                        .HasMaxLength(5)
                        .HasColumnType("bit")
                        .HasColumnName("isBlock");

                    b.Property<int>("LoginRetryCount")
                        .HasMaxLength(2)
                        .HasColumnType("int")
                        .HasColumnName("LoginRetryCount");

                    b.HasKey("UserId");

                    b.ToTable("UserLoginSecurities", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            IsAccountBlock = false,
                            LoginRetryCount = 0
                        });
                });

            modelBuilder.Entity("Core.Security.Entities.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userid");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("roleid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("userroles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 1,
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("Core.Security.Entities.UserLoginSecurity", b =>
                {
                    b.HasOne("Core.Security.Entities.User", "User")
                        .WithOne("UserLoginSecurity")
                        .HasForeignKey("Core.Security.Entities.UserLoginSecurity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Security.Entities.UserRole", b =>
                {
                    b.HasOne("Core.Security.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Security.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Security.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Core.Security.Entities.User", b =>
                {
                    b.Navigation("UserLoginSecurity")
                        .IsRequired();

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
