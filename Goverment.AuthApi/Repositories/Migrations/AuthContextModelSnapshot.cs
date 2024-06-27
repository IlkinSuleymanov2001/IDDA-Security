﻿// <auto-generated />
using System;
using Goverment.AuthApi.Repositories.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Security.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("firstname");

                    b.Property<string>("IDToken")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("idtoken");

                    b.Property<DateTime?>("IDTokenExpireDate")
                        .HasMaxLength(50)
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("idtokenexpiredate");

                    b.Property<bool>("IsVerify")
                        .HasMaxLength(20)
                        .HasColumnType("boolean")
                        .HasColumnName("isverify");

                    b.Property<DateTime?>("OptCreatedDate")
                        .HasMaxLength(50)
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("otpcreateddate");

                    b.Property<string>("OtpCode")
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("otpcode");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("passwordhash");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "ilkinsuleymanov200@gmail.com",
                            FullName = "Ilkin  Suleymanov",
                            IsVerify = true,
                            PasswordHash = new byte[] { 115, 158, 237, 30, 13, 244, 54, 12, 208, 48, 81, 139, 163, 188, 124, 110, 5, 69, 177, 132, 122, 234, 80, 132, 159, 116, 146, 233, 58, 131, 240, 73, 210, 167, 176, 32, 205, 43, 242, 29, 244, 51, 42, 25, 54, 241, 228, 253, 45, 196, 35, 218, 67, 34, 134, 4, 214, 192, 114, 21, 245, 179, 63, 183 },
                            PasswordSalt = new byte[] { 127, 54, 19, 63, 101, 131, 159, 243, 248, 1, 166, 169, 5, 100, 73, 143, 226, 111, 229, 72, 176, 116, 76, 195, 81, 208, 124, 248, 75, 193, 74, 74, 97, 106, 106, 216, 148, 155, 200, 146, 70, 213, 238, 41, 201, 102, 162, 238, 125, 88, 250, 4, 146, 73, 3, 78, 118, 176, 129, 201, 170, 227, 23, 165, 126, 57, 166, 210, 33, 39, 5, 68, 129, 243, 137, 253, 203, 254, 126, 222, 221, 58, 2, 10, 250, 85, 160, 186, 121, 64, 72, 234, 147, 148, 186, 214, 211, 248, 25, 112, 115, 100, 171, 86, 109, 176, 236, 57, 68, 154, 155, 139, 72, 188, 228, 211, 12, 128, 244, 246, 147, 32, 66, 162, 48, 193, 95, 215 },
                            Status = false
                        });
                });

            modelBuilder.Entity("Core.Security.Entities.UserLoginSecurity", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userid");

                    b.Property<DateTime?>("AccountBlockedTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("AccountBlockedTime");

                    b.Property<DateTime?>("AccountUnblockedTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("AccountUnBlockedTime");

                    b.Property<bool>("IsAccountBlock")
                        .HasMaxLength(5)
                        .HasColumnType("boolean")
                        .HasColumnName("isBlock");

                    b.Property<int>("LoginRetryCount")
                        .HasMaxLength(2)
                        .HasColumnType("integer")
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

            modelBuilder.Entity("Goverment.Core.Security.Entities.UserOtpSecurity", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("userid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<int>("TryOtpCount")
                        .HasMaxLength(2)
                        .HasColumnType("integer")
                        .HasColumnName("tryotpcount");

                    b.Property<int>("UserId1")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("UserOtpSecurities", (string)null);
                });

            modelBuilder.Entity("Goverment.Core.Security.Entities.UserResendOtpSecurity", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userid");

                    b.Property<bool>("IsLock")
                        .HasMaxLength(5)
                        .HasColumnType("boolean")
                        .HasColumnName("islock");

                    b.Property<int>("TryOtpCount")
                        .HasMaxLength(2)
                        .HasColumnType("integer")
                        .HasColumnName("tryotpcount");

                    b.Property<DateTime?>("unBlockDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("unblockdate");

                    b.HasKey("UserId");

                    b.ToTable("UserResendOtpSecurities", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            IsLock = false,
                            TryOtpCount = 0
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

            modelBuilder.Entity("Goverment.Core.Security.Entities.UserOtpSecurity", b =>
                {
                    b.HasOne("Core.Security.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Goverment.Core.Security.Entities.UserResendOtpSecurity", b =>
                {
                    b.HasOne("Core.Security.Entities.User", "User")
                        .WithOne("UserResendOtpSecurity")
                        .HasForeignKey("Goverment.Core.Security.Entities.UserResendOtpSecurity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

                    b.Navigation("UserResendOtpSecurity")
                        .IsRequired();

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
