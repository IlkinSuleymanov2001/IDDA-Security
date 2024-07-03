﻿// <auto-generated />
using System;
using Goverment.AuthApi.Repositories.Concretes.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    [DbContext(typeof(AuthContext))]
    [Migration("20240702081844_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<DateTime?>("CreatedTime")
                        .HasMaxLength(50)
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createdtime");

                    b.Property<DateTime?>("DeleteTime")
                        .HasMaxLength(50)
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deletedtime");

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

                    b.Property<bool>("IsDelete")
                        .HasMaxLength(50)
                        .HasColumnType("boolean")
                        .HasColumnName("isdelete");

                    b.Property<bool>("IsVerify")
                        .HasMaxLength(20)
                        .HasColumnType("boolean")
                        .HasColumnName("isverify");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasMaxLength(50)
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modifiedtime");

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
                            IsDelete = false,
                            IsVerify = true,
                            PasswordHash = new byte[] { 143, 201, 137, 145, 212, 183, 124, 157, 135, 158, 26, 246, 106, 23, 57, 208, 189, 90, 50, 182, 218, 157, 126, 53, 149, 13, 79, 80, 7, 108, 189, 170, 148, 62, 20, 194, 139, 250, 73, 115, 27, 240, 9, 21, 80, 96, 51, 212, 220, 100, 134, 89, 7, 252, 21, 200, 198, 240, 199, 37, 144, 13, 134, 57 },
                            PasswordSalt = new byte[] { 249, 205, 91, 201, 197, 148, 176, 253, 165, 43, 233, 165, 103, 141, 102, 190, 52, 43, 190, 194, 57, 1, 31, 85, 135, 182, 189, 62, 239, 111, 239, 209, 116, 96, 106, 146, 93, 102, 128, 12, 11, 239, 16, 32, 13, 9, 175, 58, 137, 35, 224, 253, 234, 150, 141, 227, 77, 205, 40, 253, 107, 16, 250, 87, 52, 202, 32, 221, 72, 31, 252, 230, 236, 255, 199, 171, 136, 93, 202, 151, 99, 42, 199, 39, 98, 130, 219, 20, 203, 78, 188, 40, 120, 225, 244, 10, 215, 85, 160, 34, 186, 213, 155, 250, 95, 108, 151, 207, 83, 204, 213, 72, 77, 237, 253, 219, 239, 195, 226, 186, 215, 114, 237, 108, 0, 61, 181, 16 },
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

            modelBuilder.Entity("Goverment.Core.Security.Entities.Audit.UserAudit", b =>
                {
                    b.Property<int>("AuditId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AuditId"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsVerify")
                        .HasColumnType("boolean");

                    b.Property<string>("Method")
                        .HasColumnType("text");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AuditId");

                    b.ToTable("UserAudits");
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
