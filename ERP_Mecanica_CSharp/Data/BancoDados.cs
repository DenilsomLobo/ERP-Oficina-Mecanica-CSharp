using Microsoft.Data.Sqlite;

namespace ERP_Mecanica_CSharp.Data;

public static class BancoDados
{
    public static string CaminhoBanco =>
        Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
            "AutoGestERP",
            "autogest.db"
        );

    public static SqliteConnection AbrirConexao()
    {
        var conexao = new SqliteConnection(
            $"Data Source={CaminhoBanco};Foreign Keys=True"
        );

        conexao.Open();

        return conexao;
    }

    public static void Inicializar()
    {
        // Cria a pasta do banco, se necessário.
        Directory.CreateDirectory(
            Path.GetDirectoryName(CaminhoBanco)!
        );

        using var conexao = AbrirConexao();
        using var comando = conexao.CreateCommand();

        comando.CommandText = """
            CREATE TABLE IF NOT EXISTS Clientes
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL,
                Telefone TEXT,
                Documento TEXT,
                DataCadastro TEXT NOT NULL
                    DEFAULT CURRENT_TIMESTAMP
            );
            """;

        comando.ExecuteNonQuery();
    }
}