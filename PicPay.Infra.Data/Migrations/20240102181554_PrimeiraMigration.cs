using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicPay.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lojistaModels",
                columns: table => new
                {
                    id_lojista = table.Column<Guid>(type: "uuid", nullable: false),
                    nomeCompleto = table.Column<string>(type: "text", nullable: true),
                    cpf = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    senha = table.Column<string>(type: "text", nullable: true),
                    saldo = table.Column<decimal>(type: "numeric", nullable: true),
                    cnpj = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lojistaModels", x => x.id_lojista);
                });

            migrationBuilder.CreateTable(
                name: "transferenciaModels",
                columns: table => new
                {
                    dd = table.Column<Guid>(type: "uuid", nullable: false),
                    info_remetente = table.Column<string>(type: "text", nullable: true),
                    info_destinatario = table.Column<string>(type: "text", nullable: true),
                    valor = table.Column<decimal>(type: "numeric", nullable: false),
                    autorizacaoExterna = table.Column<bool>(type: "boolean", nullable: false),
                    sucesso = table.Column<bool>(type: "boolean", nullable: false),
                    dataTransferencia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transferenciaModels", x => x.dd);
                });

            migrationBuilder.CreateTable(
                name: "usuarioModels",
                columns: table => new
                {
                    id_usuario = table.Column<Guid>(type: "uuid", nullable: false),
                    nomeCompleto = table.Column<string>(type: "text", nullable: true),
                    cpf = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    senha = table.Column<string>(type: "text", nullable: true),
                    saldo = table.Column<decimal>(type: "numeric", nullable: true),
                    telefone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarioModels", x => x.id_usuario);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lojistaModels");

            migrationBuilder.DropTable(
                name: "transferenciaModels");

            migrationBuilder.DropTable(
                name: "usuarioModels");
        }
    }
}
