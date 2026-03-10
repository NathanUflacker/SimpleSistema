using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Cpoint
{
    public partial class MainWindow : Window
    {
        private Cadastro<Pessoa> cadastro;

        public MainWindow()
        {
            InitializeComponent();
            cadastro = new Cadastro<Pessoa>();
        }

        private void BotaoAdicionar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(EntradaID.Text))
                {
                    MessageBox.Show("Por favor, informe o ID.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(EntradaNome.Text))
                {
                    MessageBox.Show("Por favor, informe o Nome.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int id = int.Parse(EntradaID.Text);
                string nome = EntradaNome.Text;

                Pessoa pessoa = new Pessoa(nome);
                cadastro.Adicionar(id, pessoa);

                MessageBox.Show($"Pessoa adicionada com sucesso!\nID: {id}\nNome: {nome}", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                LimparCampos();
                AtualizarGrid();
            }
            catch (System.FormatException)
            {
                MessageBox.Show("ID deve ser um número válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BotaoListar_Click(object sender, RoutedEventArgs e)
        {
            AtualizarGrid();

            if (cadastro.Count() == 0)
            {
                MessageBox.Show("Nenhum registro cadastrado.", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Total de registros: {cadastro.Count()}", "Listagem", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BotaoBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(EntradaID.Text))
                {
                    MessageBox.Show("Por favor, informe o ID para buscar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int id = int.Parse(EntradaID.Text);
                Pessoa pessoa = cadastro.Buscar(id);

                MessageBox.Show($"Registro encontrado!\nID: {id}\nNome: {pessoa.Nome}", "Busca", MessageBoxButton.OK, MessageBoxImage.Information);

                EntradaNome.Text = pessoa.Nome;
            }
            catch (System.FormatException)
            {
                MessageBox.Show("ID deve ser um número válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Erro ao buscar: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BotaoBuscarInline_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(EntradaBuscaID.Text))
                {
                    MessageBox.Show("Por favor, informe o ID para buscar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int id = int.Parse(EntradaBuscaID.Text);
                Pessoa pessoa = cadastro.Buscar(id);

                MessageBox.Show($"Registro encontrado!\nID: {id}\nNome: {pessoa.Nome}", "Busca", MessageBoxButton.OK, MessageBoxImage.Information);

                EntradaID.Text = id.ToString();
                EntradaNome.Text = pessoa.Nome;
            }
            catch (System.FormatException)
            {
                MessageBox.Show("ID deve ser um número válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Erro ao buscar: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BotaoRemover_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(EntradaID.Text))
                {
                    MessageBox.Show("Por favor, informe o ID para remover.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int id = int.Parse(EntradaID.Text);

                var resultado = MessageBox.Show($"Deseja realmente remover o registro com ID {id}?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resultado == MessageBoxResult.Yes)
                {
                    cadastro.Remover(id);
                    MessageBox.Show("Registro removido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                    LimparCampos();
                    AtualizarGrid();
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("ID deve ser um número válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Erro ao remover: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BotaoLimpar_Click(object sender, RoutedEventArgs e)
        {
            LimparCampos();
            MessageBox.Show("Campos limpos!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LimparCampos()
        {
            EntradaID.Text = string.Empty;
            EntradaNome.Text = string.Empty;
            EntradaBuscaID.Text = string.Empty;
        }

        private void AtualizarGrid()
        {
            List<KeyValuePair<int, Pessoa>> lista = cadastro.Listar();
            TabelaDados.ItemsSource = null;
            TabelaDados.ItemsSource = lista;
        }
    }
}
