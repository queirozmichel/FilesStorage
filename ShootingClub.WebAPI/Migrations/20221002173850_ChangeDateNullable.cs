﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShootingClub.WebAPI.Migrations
{
  public partial class ChangeDateNullable : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<DateTime>(
          name: "ChangeDate",
          table: "Files",
          type: "datetime2",
          nullable: true,
          oldClrType: typeof(DateTime),
          oldType: "datetime2");

      migrationBuilder.AlterColumn<DateTime>(
          name: "ChangeDate",
          table: "Clients",
          type: "datetime2",
          nullable: true,
          oldClrType: typeof(DateTime),
          oldType: "datetime2");

      migrationBuilder.AlterColumn<DateTime>(
          name: "ChangeDate",
          table: "Addresses",
          type: "datetime2",
          nullable: true,
          oldClrType: typeof(DateTime),
          oldType: "datetime2");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<DateTime>(
          name: "ChangeDate",
          table: "Files",
          type: "datetime2",
          nullable: false,
          defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
          oldClrType: typeof(DateTime),
          oldType: "datetime2",
          oldNullable: true);

      migrationBuilder.AlterColumn<DateTime>(
          name: "ChangeDate",
          table: "Clients",
          type: "datetime2",
          nullable: false,
          defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
          oldClrType: typeof(DateTime),
          oldType: "datetime2",
          oldNullable: true);

      migrationBuilder.AlterColumn<DateTime>(
          name: "ChangeDate",
          table: "Addresses",
          type: "datetime2",
          nullable: false,
          defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
          oldClrType: typeof(DateTime),
          oldType: "datetime2",
          oldNullable: true);
    }
  }
}
