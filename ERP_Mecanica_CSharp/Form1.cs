using System.Data;
using ERP_Mecanica_CSharp.Data;

namespace ERP_Mecanica_CSharp;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        Text = "AutoGest ERP - Clientes";

        btnSalvar.Click += BtnSalvar_Click;

        Load += (_, _) => CarregarClientes();
    }

    private void BtnSalvar_Click(
        object? sender,
        EventArgs e)
    {
        string nome = txtNome.Text.Trim();
        string telefone = txtTelefone.Text.Trim();

        if (string.IsNullOrWhiteSpace(nome))
        {
            MessageBox.Show("Informe o nome do cliente.");
            return;
        }

        try
        {
            using var conexao = BancoDados.AbrirConexao();
            using var comando = conexao.CreateCommand();

            comando.CommandText = """
                INSERT INTO Clientes (Nome, Telefone)
                VALUES ($nome, $telefone);
                """;

            comando.Parameters.AddWithValue("$nome", nome);
            comando.Parameters.AddWithValue(
                "$telefone", telefone);

            comando.ExecuteNonQuery();

            MessageBox.Show("Cliente cadastrado!");

            txtNome.Clear();
            txtTelefone.Clear();

            CarregarClientes();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Erro ao salvar cliente: {ex.Message}");
        }
    }

    private void CarregarClientes()
    {
        using var conexao = BancoDados.AbrirConexao();
        using var comando = conexao.CreateCommand();

        comando.CommandText = """
            SELECT Id, Nome, Telefone
            FROM Clientes
            ORDER BY Nome;
            """;

        using var leitor = comando.ExecuteReader();

        DataTable tabela = new DataTable();

        tabela.Load(leitor);

        dgvClientes.DataSource = tabela;
    }
}