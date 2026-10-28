using ERP_Mecanica_CSharp.Data;
using ERP_Mecanica_CSharp;

namespace ERP_Mecanica_CSharp;

internal static class Program
{
    [STAThread]

    static void Main()
    {
        ApplicationConfiguration.Initialize();

        try 
        {
            BancoDados.Inicializar();

            Application.Run(new Form1());        
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Não foi possível iniciar o ERP:\n{ex.Message}",
                "Erro de inicialização",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );
        }
    }
}