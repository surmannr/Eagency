using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eagency.Dal.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address_Id = table.Column<int>(type: "int", nullable: true),
                    Address_Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_ZipCode = table.Column<int>(type: "int", nullable: true),
                    NumberOfBedrooms = table.Column<int>(type: "int", nullable: false),
                    NumberOfBathrooms = table.Column<int>(type: "int", nullable: false),
                    NumberOfGarages = table.Column<int>(type: "int", nullable: false),
                    NumberOfParkingSpaces = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    HouseType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFurnished = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_AspNetUsers_AgentId",
                        column: x => x.AgentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeePercentage = table.Column<double>(type: "float", nullable: false),
                    PaymentFrequency = table.Column<int>(type: "int", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsSigned = table.Column<bool>(type: "bit", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "Agent", "c41917e3-ba41-444d-8ead-703f9167c5c6", "Agent", "AGENT" },
                    { "Customer", "6ee21c8b-6202-4538-8014-660abcf4d7fc", "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "seedone", 0, "f3908db0-891f-413e-980f-5d1b7e5c738e", "tesztelek@gmail.com", true, "Teszt", "Elek", false, null, "TESZTELEK@GMAIL.COM", "TESZTELEK", "AQAAAAEAACcQAAAAEEec8SmSRa6XfWsmj+K6J0Zchsk0Kl2Bs0xF1BWPHzTIwJK47q+Xq/nxTGA0vg8pdg==", "06/30-152-5123", false, "421601e6-8fa0-43d0-a5bc-079cd3efd547", false, "tesztelek" },
                    { "seedtwo", 0, "68665a5e-5e9a-4e6a-b5db-767a708ecce2", "wincheszter@gmail.com", true, "Winch", "Eszter", false, null, "WINCHESZTER@GMAIL.COM", "WINCHESZTER", "AQAAAAEAACcQAAAAEAOfK4QL73EQk5EXKRt08LvOvFkSyF6KNE9lRisyNZI/VIl21Qhq0BNXFW4T/WmhYg==", "06/30-152-5123", false, "3a5a7329-91a1-45f6-9c36-d38049439f86", false, "wincheszter" },
                    { "seedthree", 0, "09c8f835-1ca4-4333-8c39-c2557e4c00a2", "kbela@gmail.com", true, "Kis", "Béla", false, null, "KBELA@GMAIL.COM", "KBELA", "AQAAAAEAACcQAAAAECOx0lOR3fq95DtrPRhPo4XopMSKJl0vs6/q8ce31vXK9HDqSDy1f+h+bY1eX+jCGw==", "06/30-152-5123", false, "3d2fef9a-e7d0-4593-a242-03e49ccbfa13", false, "kbela" },
                    { "admin", 0, "eb865abc-e495-4cad-9378-d009f78358cd", "admin@eagency.com", true, "Admin", "Admin", false, null, "ADMIN@EAGENCY.COM", "ADMIN", "AQAAAAEAACcQAAAAEPKRK+3IMa5rksNj+H3fmF873jJ655E7xM9x+WD5SHtpEnpI38EtI88o4TwydWUqGw==", "06/30-152-5123", false, "d40b8cb5-f773-4043-a716-3c666484a61e", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "Agent", "admin" },
                    { "Customer", "seedone" },
                    { "Customer", "seedtwo" },
                    { "Customer", "seedthree" }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "AgentId", "Area", "Description", "HouseType", "ImageName", "IsFurnished", "NumberOfBathrooms", "NumberOfBedrooms", "NumberOfGarages", "NumberOfParkingSpaces", "Price", "Address_City", "Address_Country", "Address_Id", "Address_Street", "Address_ZipCode" },
                values: new object[,]
                {
                    { 4, "admin", 135, "A three bedroom mid-terrace property within the popular Swansea suburb of Brynhyfryd. Benefiting from being within close proximity to local amenities and approx. 4 miles from Swansea City Centre. The accommodation, benefiting from updating comprises:- Entrance hallway, three reception rooms, kitchen and shower room to the ground floor. To the first floor there are three bedrooms. Externally there is an enclosed rear garden.", "Cottage", "alap4.jpg", false, 2, 4, 0, 0, 15000, "Honolulu", "USA", 1, "Upalu St street 3", 96705 },
                    { 3, "admin", 35, "This very popular and pretty, coastal village of Horton is nestled between the glorious Oxwich Bay and Port Eynon Bay along the South coast of the Gower Peninsula. A popular beach side spot, Horton is in an area of outstanding natural beauty which is blessed with many award-winning sandy beaches and amazing coastal walks.", "Mediterranean", "alap3.jpg", true, 1, 2, 2, 2, 17400, "Peking", "China", 2, "Chicaego street 45", 11004 },
                    { 2, "admin", 320, "Built around a traditional Gower cottage and situated within the welcoming Gower village of Horton you will find the spectacular family home that is Castle Hill Cottage. Sitting at the top of its private drive, this detached family home enjoys incredible views over the most southerly point of Gower with its rugged gorse covered cliffs and dunes; over Port Eynon Bay and its rocky point; over the Bristol Channel across to Ilfracombe in North Devon and, on a clear day, as far to the West as Lundy. In its elevated position it is just paces away from the sandy beach of Horton and within a short walk along the boardwalk to Port Eynon. This well-presented home sits in readiness for its new owners to create new memories.", "Apartment", "alap2.jpg", false, 2, 4, 2, 1, 35500, "Madrid", "Spain", 3, "Bueno street 45", 3424 },
                    { 1, "admin", 110, "Situated on the first floor we offer a two bedroom apartment offering partial Marina Views. The apartment is within walking distance to Swansea Marina and all the local restaurants and coffee shops, close to the new music arena and town centre. The apartment offers a L shaped hallway leading to lounge with french doors and balcony, master bedroom, bathroom, kitchen and second bedroom and benifits from lift access, recently fitted UPVC windows. and allocated secure underground parking.", "Detached", "alap1.jpg", true, 1, 3, 1, 2, 22000, "New York", "USA", 4, "Pearl street 72", 5504 },
                    { 5, "admin", 64, "emi detached property comprising entrance hallway, lounge and fitted kitchen to the ground floor. To the first floor there are two bedrooms and a bathroom. Externally there is generously sized garden wrapping around the side and rear. Benefits include uPVC double glazing throughout and newly fitted Hive gas central heating with newly fitted radiators. The property is situated close to local amenities and transport links with easy access to the M4. Viewing is highly recommended to appreciate this well presented property. ", "Colonial", "alap5.jpg", false, 1, 3, 2, 0, 35000, "Budapest", "Hungary", 5, "Havanna St street 3", 1181 },
                    { 6, "admin", 275, "Light, airy and very nicely presented modern semi detached property situated in a quiet family friendly cul de sac location of Cockett. This well proportioned family home comprises: lounge/ dining room, fitted kitchen and cloakroom to ground floor with three bedrooms and family bathroom to first floor. Benefits: uPVC double glazing, gas central heating, built in storage facilities, two parking spaces and a pleasant enclosed rear garden offering decked seating area, astro turf grass, patio area and side access. Easy access to local amenities, Fforestfach Retail Park, the M4 and Swansea City Centre.", "Craftsman", "alap6.jpg", true, 3, 6, 2, 1, 45000, "Tokyo", "Japan", 6, "Kaumu street 410", 5620 },
                    { 7, "admin", 75, "A lovely light and airy three bedroom flat within walking distance of local amenities and great school catchments. Briefly comprises: lounge/ dining room, fitted kitchen, three bedrooms, bathroom with separate w/c and communal entrance hallway. This property benefits: uPVC double glazing, gas central heating, small balcony and ample storage facilities. It would make a great home with easy access to Sketty Park, Tycoch Square, Sketty Cross, Singleton Hospital & Park, Killay including Olchfa & Parklands School catchments as well. ", "Contemporary", "alap7.jpg", true, 1, 2, 2, 1, 32700, "Delhi", "India", 7, "Temple street 80", 41023 },
                    { 8, "admin", 110, "Extremely spacious and very well extended detached family home situated in a quiet cul-de-sac at the heart of Cockett. This beautiful property briefly comprises of sitting room, cloakroom/ utility, large open plan L shaped fitted kitchen, dining room and lounge great for modern family living to ground floor, four bedrooms, en-suite, family bathroom to the first floor plus an attic room. The many benefits to this property are uPVC double glazing, gas central heating, off road parking, single garage, enclosed and very well maintained tiered rear garden laid to lawn and patio area. ", "Ranch", "alap8.jpg", false, 1, 2, 1, 1, 22600, "Shanghai", "China", 8, "Ruraro street 91", 632302 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Answer", "Date", "PropertyId", "Question", "UserId" },
                values: new object[,]
                {
                    { 4, "It's one year old.", new DateTimeOffset(new DateTime(2021, 5, 30, 16, 26, 48, 626, DateTimeKind.Unspecified).AddTicks(5436), new TimeSpan(0, 2, 0, 0, 0)), 4, "How old is the roof?", "seedone" },
                    { 3, "He has a new job in another town.", new DateTimeOffset(new DateTime(2021, 5, 30, 16, 26, 48, 626, DateTimeKind.Unspecified).AddTicks(5430), new TimeSpan(0, 2, 0, 0, 0)), 3, "Why is the seller leaving?", "seedthree" },
                    { 11, null, new DateTimeOffset(new DateTime(2021, 5, 30, 16, 26, 48, 626, DateTimeKind.Unspecified).AddTicks(5463), new TimeSpan(0, 2, 0, 0, 0)), 3, "Is the home in a flood zone or prone to other natural disasters?", "seedtwo" },
                    { 2, null, new DateTimeOffset(new DateTime(2021, 5, 30, 16, 26, 48, 626, DateTimeKind.Unspecified).AddTicks(5399), new TimeSpan(0, 2, 0, 0, 0)), 2, "Is the home in a flood zone or prone to other natural disasters?", "seedtwo" },
                    { 10, "It's one year old.", new DateTimeOffset(new DateTime(2021, 5, 30, 16, 26, 48, 626, DateTimeKind.Unspecified).AddTicks(5460), new TimeSpan(0, 2, 0, 0, 0)), 2, "How old is the roof?", "seedone" },
                    { 1, null, new DateTimeOffset(new DateTime(2021, 5, 30, 16, 26, 48, 624, DateTimeKind.Unspecified).AddTicks(3340), new TimeSpan(0, 2, 0, 0, 0)), 1, "Are there any shops nearby?", "seedone" },
                    { 9, "He has a new job in another town.", new DateTimeOffset(new DateTime(2021, 5, 30, 16, 26, 48, 626, DateTimeKind.Unspecified).AddTicks(5456), new TimeSpan(0, 2, 0, 0, 0)), 1, "Why is the seller leaving?", "seedthree" },
                    { 5, null, new DateTimeOffset(new DateTime(2021, 5, 30, 16, 26, 48, 626, DateTimeKind.Unspecified).AddTicks(5440), new TimeSpan(0, 2, 0, 0, 0)), 5, "Is the home in a flood zone or prone to other natural disasters?", "seedtwo" },
                    { 6, null, new DateTimeOffset(new DateTime(2021, 5, 30, 16, 26, 48, 626, DateTimeKind.Unspecified).AddTicks(5444), new TimeSpan(0, 2, 0, 0, 0)), 6, "How is the neighborhood?", "seedthree" },
                    { 12, null, new DateTimeOffset(new DateTime(2021, 5, 30, 16, 26, 48, 626, DateTimeKind.Unspecified).AddTicks(5467), new TimeSpan(0, 2, 0, 0, 0)), 6, "How is the neighborhood?", "seedthree" },
                    { 7, null, new DateTimeOffset(new DateTime(2021, 5, 30, 16, 26, 48, 626, DateTimeKind.Unspecified).AddTicks(5448), new TimeSpan(0, 2, 0, 0, 0)), 7, "Have there been previous problems with the house, or repairs which have been necessary?", "seedone" },
                    { 8, null, new DateTimeOffset(new DateTime(2021, 5, 30, 16, 26, 48, 626, DateTimeKind.Unspecified).AddTicks(5452), new TimeSpan(0, 2, 0, 0, 0)), 8, "What’s included with the sale?", "seedtwo" }
                });

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "Id", "ClientId", "Date", "FeePercentage", "IsPaid", "IsSigned", "PaymentFrequency", "PaymentMethod", "PropertyId" },
                values: new object[,]
                {
                    { 1, "seedone", new DateTimeOffset(new DateTime(2021, 5, 20, 16, 26, 48, 626, DateTimeKind.Unspecified).AddTicks(9008), new TimeSpan(0, 2, 0, 0, 0)), 0.28000000000000003, false, false, 2, "BankTransfer", 3 },
                    { 3, "seedthree", new DateTimeOffset(new DateTime(2021, 6, 3, 16, 26, 48, 627, DateTimeKind.Unspecified).AddTicks(822), new TimeSpan(0, 2, 0, 0, 0)), 0.39000000000000001, true, false, 5, "Creditcard", 1 },
                    { 2, "seedtwo", new DateTimeOffset(new DateTime(2021, 5, 12, 16, 26, 48, 627, DateTimeKind.Unspecified).AddTicks(808), new TimeSpan(0, 2, 0, 0, 0)), 0.57999999999999996, true, true, 3, "Cash", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PropertyId",
                table: "Comments",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ClientId",
                table: "Contracts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_PropertyId",
                table: "Contracts",
                column: "PropertyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AgentId",
                table: "Properties",
                column: "AgentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
