using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsWebApp.Migrations
{
    public partial class InsertInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a946a04a-1b06-44f6-b72b-388f7e5c5773", 0, "c463ddb3-ba0d-4816-8f0d-edeaf3eb6df1", "v.pavardenis@gmail.com", true, true, null, "V.PAVARDENIS@GMAIL.COM", "V PAVARDENIS", "AQAAAAEAACcQAAAAEKGAVOKhL9/tlVFm93GZyaAOzTQ3ToRCmchisKzYrSX3bT5JgLrPpfDrDT7Aoe7YrQ==", null, false, "2LMI6XECPFVDBOHSLKZUVCXUK7V7547S", false, "V Pavardenis" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Music" },
                    { 2, "Arts" },
                    { 3, "Film" },
                    { 4, "Food" },
                    { 5, "Fitness" },
                    { 6, "Networking" },
                    { 7, "Nightlife" },
                    { 8, "Other" }
                });

            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "Id", "Address", "CategoryId", "City", "DateAndTime", "Description", "ImageName", "Name", "UserId" },
                values: new object[] { 1, "720 9th Ave", 1, "New York", new DateTime(2021, 1, 14, 17, 0, 0, 0, DateTimeKind.Local), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed suscipit fermentum tortor, non pharetra erat. Donec at felis purus. Vivamus nisi lorem, congue eu nisl quis, tincidunt accumsan est. Nam id nulla ex. Cras pretium ante quis sagittis vestibulum. In eu vehicula massa. Duis dapibus consequat erat a eleifend.Nullam rutrum finibus magna vel viverra. Sed quis leo laoreet,elementum nunc bibendum, aliquam nulla.Integer eu nunc arcu.Vivamus dapibus sem leo.In ut turpis eu risus sollicitudin pellentesque eu sit amet lorem.Morbi suscipit sem sit amet vestibulum placerat. Phasellus sed ultricies odio, at facilisis est.Integer enim ex, malesuada vitae blandit et, tempus nec nulla.", "ff6cda08-29ce-4436-9298-bdd467d1210f_highres_491103347.png", "Lorem ipsum dolor sit amet elit.", "a946a04a-1b06-44f6-b72b-388f7e5c5773" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a946a04a-1b06-44f6-b72b-388f7e5c5773");

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
