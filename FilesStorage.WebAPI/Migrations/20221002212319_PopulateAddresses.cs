using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilesStorage.WebAPI.Migrations
{
  public partial class PopulateAddresses : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql(
        "INSERT INTO Addresses(Street, Number, District, City, State, CEP, ClientId)" +
        "VALUES('Rua Monsenhor Hilton', 190, 'Jadir Marinho', 'Itaúna', 'MG', '35681232', 1)");

      migrationBuilder.Sql(
        "INSERT INTO Addresses(Street, Number, District, City, State, CEP, ClientId)" +
        "VALUES('Avenida Amazonas', 650, 'Barro Preto', 'Belo Horizonte', 'MG', '30180010', 2)");

      migrationBuilder.Sql(
        "INSERT INTO Addresses(Street, Number, District, City, State, CEP, ClientId)" +
        "VALUES('Avenida Nove de Julho', 1135, 'Bela Vista', 'São Paulo', 'SP', '01312001', 3)");

      migrationBuilder.Sql(
        "INSERT INTO Addresses(Street, Number, District, City, State, CEP, ClientId)" +
        "VALUES('Rua Doutor Eduardo Bahiana', 95, 'Pituba', 'Salvador', 'BA', '41810600', 4)");

      migrationBuilder.Sql(
        "INSERT INTO Addresses(Street, Number, District, City, State, CEP, ClientId)" +
        "VALUES('Rua Estrela do Amazonas', 118, 'Tancredo Neves', 'Manaus', 'AM', '69087520', 5)");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("DELETE FROM Addresses");
    }
  }
}
