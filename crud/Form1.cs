using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//acessando o pacote do mysql
using MySql.Data.MySqlClient;

namespace crud
{
    public partial class frmCadastro : Form
    {
        //Conexão com o banco de dados MySQL
        MySqlConnection Conexao;
        string data_source = "datasource=localhost; username=root; password=; database=db_cadastro";
        public frmCadastro()
        {
            InitializeComponent();
            
            //Configuração inciial do ListView para exibição dos dados dos clientes
            lstCliente.View = View.Details; //Define a visualização em "detalhes"
            lstCliente.LabelEdit = true; //Permite editar os títulos das colunas
            lstCliente.AllowColumnReorder = true; //PErmite reordenar as colunas
            lstCliente.FullRowSelect = true; //Seleciona a linha inteira ao clica
            lstCliente.GridLines = true; //Exibe a slinhas de grade no ListView

            //Definindo as colunas do ListView
            lstCliente.Columns.Add("Codigo", 79, HorizontalAlignment.Left); //Coluna do Código
            lstCliente.Columns.Add("Nome completo", 158, HorizontalAlignment.Left);//Coluna do Nome completo
            lstCliente.Columns.Add("Nome Social", 158, HorizontalAlignment.Left);//Coluna do Nome social
            lstCliente.Columns.Add("E-mail", 158, HorizontalAlignment.Left);//Coluna do E-mail
            lstCliente.Columns.Add("CPF", 158, HorizontalAlignment.Left);//Coluna do CPF

            //Carregar os dados dos clientes na interface
            carregar_clientes();
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                //Validando campos obrigatórios
                if(string.IsNullOrEmpty(txtNomeCompleto.Text.Trim()) || 
                    string.IsNullOrEmpty(txtEmail.Text.Trim()) ||
                    string.IsNullOrEmpty(txtCPF.Text.Trim()))
                {
                    MessageBox.Show(
                    "Todos os campos devem ser preenchidos.",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    return;  // Impede o prosseguimento se algum campo estiver vazio
                }

                //validação do CPF
                string cpf = txtCPF.Text.Trim();

                if (!isValidCPFLength(cpf))
                {
                    MessageBox.Show(
                        "Cpf Inválido. Certifique-se de que o CPF tenha 11 dígitos numéricos", 
                        "Validação de CPF", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
               
                    return;
                }

                //Cria a conexão com o banco de dados
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();

                //Comando SQL para inserir um novo cliente no banco de dados
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = Conexao
                };

                cmd.Prepare();
                cmd.CommandText = "INSERT INTO dadosdoclientes(nomecompleto, nomesocial, email, cpf) " + 
                    "VALUES(@nomecompleto, @nomesocial, @email, @cpf)";

                //Adiciona os parâmetros com os dados do formulário
                cmd.Parameters.AddWithValue("@nomecompleto", txtNomeCompleto.Text.Trim());
                cmd.Parameters.AddWithValue("@nomesocial", txtNomeSocial.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@cpf", txtCPF.Text.Trim());

                //Executa o comando de inserção no banco
                cmd.ExecuteNonQuery();

                //Mensagem de sucesso
                MessageBox.Show(
                    "Contato inserido com Sucesso: ",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );

                //Limpa os campos após o sucesso
                txtNomeCompleto.Clear();
                txtNomeSocial.Clear();
                txtEmail.Clear();
                txtCPF.Clear();

                //Recarrega os clientes na ListView
                carregar_clientes();

                //Muda para a aba de consulta
                tabControl.SelectedIndex = 1;
            }
            catch (MySqlException ex)
            {
                //Trata erros relacionados ao MySQL
                MessageBox.Show("Erro " + ex.Number + " ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                //Trata outros tipos de erro
                MessageBox.Show("Ocorreu: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                //Garante que a conexão com o banco será fechda, mesmo se ocorrer erro
                if(Conexao != null && Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
            }
        }

        private bool isValidCPFLength (string cpf)
        {
            // Remove todos os caracteres não númericos
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            // Verifica se o CPF tem exatamente 11 dígitos;
            return cpf.Length == 11;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM dadosdoclientes WHERE nomecompleto LIKE @q OR nomesocial LIKE @q ORDER BY codigo DESC";
            carregar_clientes_com_query(query);
        }

        private void carregar_clientes_com_query(string query)
        {
            try
            {
                //Cria a conexão ocm o banco de dados
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();

                //Executa a consulta SQL fornecida
                MySqlCommand cmd = new MySqlCommand(query, Conexao);

                //Se a consulta contém o parâmetro @q, adiciona o valor da caixa de pesquisa
                if(query.Contains("@q"))
                {
                    cmd.Parameters.AddWithValue("@q", "%" + txtBuscar.Text + "%");
                }

                //Executa o comando e obtém os resulttados
                MySqlDataReader reader = cmd.ExecuteReader();

                //Limpa os itens existentes no ListView antes de adiocnar novos
                lstCliente.Items.Clear();

                //Preenche o ListView com os dados dos cliente
                while (reader.Read())
                {
                    string[] row =
                    {
                        Convert.ToString(reader.GetInt32(0)), //Código
                        reader.GetString(1),                    //Nome Completo
                        reader.GetString(2),                    //Nome Social
                        reader.GetString(3),                    //E-mail
                        reader.GetString(4),                     //CPF
                    };

                    //Adiicona a linha ao ListView
                    lstCliente.Items.Add(new ListViewItem(row));
                }

            }
            catch (MySqlException ex)
            {
                //Trata erros relacionados ao MySQL
                MessageBox.Show("Erro " + ex.Number + " ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                //Trata outros tipos de erro
                MessageBox.Show("Ocorreu: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                //Garante que a conexão com o banco será fechda, mesmo se ocorrer erro
                if (Conexao != null && Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
            }
        }

        private void carregar_clientes()
        {
            string query = "Select * FROM dadosdoclientes ORDER BY codigo DESC";
            carregar_clientes_com_query(query);
        }

       
    }
}
