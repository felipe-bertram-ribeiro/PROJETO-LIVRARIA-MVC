using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivrariaTeste.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelaEmprestimos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Funcionarios_FuncionarioId",
                table: "Emprestimos");

            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Livros_LivroId",
                table: "Emprestimos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Emprestimos",
                table: "Emprestimos");

            migrationBuilder.RenameTable(
                name: "Emprestimos",
                newName: "Emprestimo");

            migrationBuilder.RenameColumn(
                name: "DataDevolucaoReal",
                table: "Emprestimo",
                newName: "DataDevolucao");

            migrationBuilder.RenameColumn(
                name: "DataDevolucaoPrevista",
                table: "Emprestimo",
                newName: "DataPrevistaDevolucao");

            migrationBuilder.RenameIndex(
                name: "IX_Emprestimos_LivroId",
                table: "Emprestimo",
                newName: "IX_Emprestimo_LivroId");

            migrationBuilder.RenameIndex(
                name: "IX_Emprestimos_FuncionarioId",
                table: "Emprestimo",
                newName: "IX_Emprestimo_FuncionarioId");

            migrationBuilder.AlterColumn<string>(
                name: "SenhaHash",
                table: "Funcionarios",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Cargo",
                table: "Funcionarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Emprestimo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Emprestimo",
                table: "Emprestimo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimo_Funcionarios_FuncionarioId",
                table: "Emprestimo",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimo_Livros_LivroId",
                table: "Emprestimo",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimo_Funcionarios_FuncionarioId",
                table: "Emprestimo");

            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimo_Livros_LivroId",
                table: "Emprestimo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Emprestimo",
                table: "Emprestimo");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Emprestimo");

            migrationBuilder.RenameTable(
                name: "Emprestimo",
                newName: "Emprestimos");

            migrationBuilder.RenameColumn(
                name: "DataPrevistaDevolucao",
                table: "Emprestimos",
                newName: "DataDevolucaoPrevista");

            migrationBuilder.RenameColumn(
                name: "DataDevolucao",
                table: "Emprestimos",
                newName: "DataDevolucaoReal");

            migrationBuilder.RenameIndex(
                name: "IX_Emprestimo_LivroId",
                table: "Emprestimos",
                newName: "IX_Emprestimos_LivroId");

            migrationBuilder.RenameIndex(
                name: "IX_Emprestimo_FuncionarioId",
                table: "Emprestimos",
                newName: "IX_Emprestimos_FuncionarioId");

            migrationBuilder.AlterColumn<string>(
                name: "SenhaHash",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Cargo",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Emprestimos",
                table: "Emprestimos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Funcionarios_FuncionarioId",
                table: "Emprestimos",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Livros_LivroId",
                table: "Emprestimos",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
