using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilesStorage.WebAPI.Migrations
{
  public partial class InitialTriggers : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql(
        "CREATE TRIGGER ClientsChangeDate ON Clients AFTER UPDATE, INSERT " +
        "AS " +
        "SET NOCOUNT ON " +
        "BEGIN " +
          "UPDATE Clients SET ChangeDate = CURRENT_TIMESTAMP FROM Clients " +
          "INNER JOIN Inserted ON Clients.ClientId = Inserted.ClientId " +
        "END");

      migrationBuilder.Sql(
        "CREATE TRIGGER AddressesChangeDate ON Addresses AFTER UPDATE, INSERT " +
        "AS " +
        "SET NOCOUNT ON " +
        "BEGIN " +
          "UPDATE Addresses SET ChangeDate = CURRENT_TIMESTAMP FROM Addresses " +
          "INNER JOIN Inserted ON Addresses.AddressId = Inserted.AddressId " +
        "END");

      migrationBuilder.Sql(
        "CREATE TRIGGER FilesChangeDate ON Files AFTER UPDATE, INSERT " +
        "AS " +
        "SET NOCOUNT ON " +
        "BEGIN " +
          "UPDATE Files SET ChangeDate = CURRENT_TIMESTAMP FROM Files " +
          "INNER JOIN Inserted ON Files.FileId = Inserted.FileId " +
        "END");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("DROP TRIGGER ClientsChangeDate");
      migrationBuilder.Sql("DROP TRIGGER AddressesChangeDate");
      migrationBuilder.Sql("DROP TRIGGER FilesChangeDate");
    }
  }
}
