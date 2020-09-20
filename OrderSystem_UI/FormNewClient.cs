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

namespace OrderSystem_UI
{
    public partial class FormNewClient : Form
    {
        string _target, _value;
        public FormNewClient(string target, string value)
        {
            this._target = target;
            this._value = value;
            InitializeComponent();
        }

        private void newClientForm_Load(object sender, EventArgs e)
        {
            txtCont1.MaxLength = 9;
            txtCont2.MaxLength = 9;
            if (_target=="Number")
            {
                txtCont1.Text = _value;
            }
            if (_target == "Text")
            {
                txtNomeCliente.Text = _value;
            }
        }

        //================================== INSERT ================================
        string resultCli;
        public void methodReadExistCategoria()
        {
            FormLogin conn = new FormLogin();
            MySqlConnection connection = conn.methodConnect();
            try
            {
                string query = "SELECT client_nome, client_contacto1 FROM tb_client WHERE client_contacto1='" + txtCont1.Text + "'";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader rd = command.ExecuteReader();

                while (rd.Read())
                {
                    resultCli = rd.GetString(0);
                }

                if (resultCli == null)
                {
                    try
                    {
                        if (txtCont2.Text == "")
                        {
                            txtCont2.Text = "0";
                        }

                        MySqlConnection connection1 = conn.methodConnect();
                        int nrr = 2;
                        string queryy = "INSERT INTO tb_client (client_office_id, client_user_id, client_nome, client_dtAniversario, client_contacto1, client_contacto2, client_provincia, client_obs, client_dt) VALUES('" + nrr + "','" + nrr + "','" + txtNomeCliente.Text.ToUpper() + "', '"+ dtAniversario.Value.Date +"' ," + txtCont1.Text + "," + txtCont2.Text + ",'" + cboProvincia.Text.ToUpper() + "','...','" + dateTimePicker1.Value.Date + "');";
                        MySqlCommand commando = new MySqlCommand(queryy, connection1);
                        commando.ExecuteNonQuery();
                        MessageBox.Show("Cliente " + txtNomeCliente.Text + " registado com Sucesso!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtNomeCliente.Text = string.Empty;
                        txtCont1.Text = string.Empty;
                        txtCont2.Text = string.Empty;
                        //txtObs.Text = string.Empty;

                        Dispose();
                    }
                    catch (Exception ex) { MessageBox.Show("Falha no registo de Cliente  >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); throw; }

                }
                else
                {
                    MessageBox.Show("Já existe Cliente com esse Contacto:  " + resultCli, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    resultCli = null;
                }

            }
            catch (Exception ex) {
            MessageBox.Show("Falha ao carregar Clientes >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); throw;
        }

        }

        private void btnSalvarClient_Click(object sender, EventArgs e)
        {
            if (txtNomeCliente.Text == "")
            {
                MessageBox.Show("Insira o nome do Cliente:: ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (txtCont1.Text == "")
                {
                    MessageBox.Show("Insira o contacto do Cliente:: ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (cboProvincia.Text == "")
                    {
                        MessageBox.Show("Selecione ou insira a provincia do Cliente:: ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //Metodo Salvar
                        methodReadExistCategoria();

                        // lstClienteEnc.Items[i].SubItems.Add(rd.GetString(2));
                    }
                }
            }
        }

        private void btnCancelEnc_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

       
    }
}
