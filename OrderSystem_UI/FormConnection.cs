using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//--------------------------
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.IO;

namespace OrderSystem_UI
{
    public partial class FormConnection : Form
    {
        int indice;
        public FormConnection(int index)
        {
            this.indice = index;
            InitializeComponent();
        }
        string folder = "Config";
        private void connectionForm_Load(object sender, EventArgs e)
        {
            //Load

            if (indice == 0)
            {
                tabControl1.SelectedIndex = 0;
            }
            if (indice == 1)
            {
                tabControl1.SelectedIndex = 1;
            }

        }

        private void butClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Fechar aplicação", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------
        //Metodo para Encriptar
        string senhaEncriptada;
        string encriptar(string entrada)
        {
            string result = string.Empty;
            Byte[] encriptar = System.Text.Encoding.Unicode.GetBytes(entrada);
            result = Convert.ToBase64String(encriptar);

            return result;
        }

        //---------------------------------------------------------------------------------------------------------

        //Metodo para criar ficheiro de texto que ira armazenar o nome do servidor assim como o nome da  base de dados
        StreamWriter sw;
        public void metodoCriarFicheiro()
        {
            string ficheiro = @"connection.txt";

            if (File.Exists(folder + "\\" + ficheiro) == false)
            {
                sw = File.CreateText(folder + "\\" + ficheiro);
            }
            string linha = txtServer.Text + ";" + txtPort.Text + ";" + txtDB.Text + ";" + txtUser.Text + ";" + encriptar(txtPassword.Text);
            sw.WriteLine(linha);

            MessageBox.Show("Ficheiro de conexao criado com sucesso", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            sw.Close();

            this.Close();

        }
        int logicConn=0;

        public void connectionTest()
        {
            string connectionString = @"Server=" + txtServer.Text + ";Port=" + int.Parse(txtPort.Text) + ";Database=" + txtDB.Text + ";Uid=" + txtUser.Text + ";Password=" + txtPassword.Text + ";";
            try
            {
                MySqlHelper helper = new MySqlHelper(connectionString);
                if (helper.isConnection)
                    MessageBox.Show("Connectado com sucesso", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);logicConn = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexão:: >>"+ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtServer.Text == "" || txtPort.Text == "" || txtDB.Text == "" || txtUser.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Campo vazio detectado, Preencha-o.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.connectionTest();
                if (logicConn == 1)
                {
                    metodoCriarFicheiro();
                }
                else
                {
                    MessageBox.Show("Falha de conexão::");
                }
            }
        }

        private void btnSaveTypeConn_Click(object sender, EventArgs e)
        {
            //Load
            if (cboIntMod.Text != "")
            {
                app.ConfigDataConnection cdc = new app.ConfigDataConnection();
                if (rdMySQL.Checked)
                {
                    cdc.mySqlDataConnection();
                }
                if (rdSQLServer.Checked)
                {
                    cdc.sqlDataConnection();
                }
                if (rdMySQL.Checked == false && rdSQLServer.Checked==false)
                {
                    MessageBox.Show("Select Database to connect!.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Select integration mode!.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
