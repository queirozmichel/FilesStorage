using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilesStorage.WebAPI.Migrations
{
  public partial class PopulateClients : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql(
        "INSERT INTO Clients(Name, Age, Sex, CPF, PhoneNumber)" +
        "VALUES('Michel Antunes Queiroz', 34, 'M', '08840058605', '37999082978')");
      migrationBuilder.Sql(
        "INSERT INTO Clients(Name, Age, Sex, CPF, PhoneNumber)" +
        "VALUES('João da Silva', 17, 'M', '01189769305', '31991256513')");
      migrationBuilder.Sql(
        "INSERT INTO Clients(Name, Age, Sex, CPF, PhoneNumber)" +
        "VALUES('Juliana Cruz', 56, 'F', '05685112609', '11999018594')");
      migrationBuilder.Sql(
        "INSERT INTO Clients(Name, Age, Sex, CPF, PhoneNumber)" +
        "VALUES('Isabela Moreira', 25, 'F', '02359814763', '71991805632')");
      migrationBuilder.Sql(
        "INSERT INTO Clients(Name, Age, Sex, CPF, PhoneNumber)" +
        "VALUES('Ronaldo Ventura', 40, 'M', '08915698405', '92999069874')");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("DELETE FROM Clients");
    }
  }
}
