using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//+-------------+-----------------+
using MySql.Data.MySqlClient;

namespace OrderSystem_UI
{
    public partial class FormEncomendas : Form
    {
        Loader _loader = new Loader();

        FormLogin conn = new FormLogin();
        MySqlConnection connection = null;

        string query=null;
        MySqlCommand command = null;
        MySqlDataReader rd = null;

        int idOff, idUser;
        string _DBConn, _DBComm, _DBDreader, _DBDadapter, _DBCommBuilder, user_name, user_nickname;
        public FormEncomendas(string DBConn, string DBComm, string DBDreader, string DBDadapter, string DBCommBuilder, int idOff, int idUser, string user_name, string user_nickname)
        {
            this._DBConn = DBConn;
            this._DBComm = DBComm;
            this._DBDreader = DBDreader;
            this._DBDadapter = DBDadapter;
            this._DBCommBuilder = DBCommBuilder;

            this.idOff = idOff;
            this.idUser = idUser;
            this.user_name = user_name;
            this.user_nickname = user_nickname;

            InitializeComponent();
        }

        private void encomendas_Load(object sender, EventArgs e)
        {
            //Load
            cboFiltro.Text = "ENTRADA";
            txtSearch.Focus();

            btnDetalhes.Enabled = false;
            btnMovimento.Enabled = false;

            

            methodReadEncomenda();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            FormTabEnc tbEnc = new FormTabEnc(_DBConn, _DBComm, _DBDreader, _DBDadapter, _DBCommBuilder, 1, idOff ,idUser, user_name, user_nickname);
            tbEnc.ShowDialog();
        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDetalhes_Click(object sender, EventArgs e)
        {
            FormDetalhesEnc fde = new FormDetalhesEnc(int.Parse(dgvEncView.CurrentRow.Cells[0].Value.ToString()), dgvEncView.CurrentRow.Cells[4].Value.ToString(), double.Parse(dgvEncView.CurrentRow.Cells[5].Value.ToString()), double.Parse(dgvEncView.CurrentRow.Cells[6].Value.ToString()), double.Parse(dgvEncView.CurrentRow.Cells[7].Value.ToString()), double.Parse(dgvEncView.CurrentRow.Cells[8].Value.ToString()), double.Parse(dgvEncView.CurrentRow.Cells[9].Value.ToString()), double.Parse(dgvEncView.CurrentRow.Cells[10].Value.ToString()), dgvEncView.CurrentRow.Cells[11].Value.ToString(), dgvEncView.CurrentRow.Cells[12].Value.ToString(), dgvEncView.CurrentRow.Cells[13].Value.ToString(), dgvEncView.CurrentRow.Cells[14].Value.ToString(), dgvEncView.CurrentRow.Cells[15].Value.ToString(), dgvEncView.CurrentRow.Cells[16].Value.ToString());
            fde.ShowDialog();

            btnDetalhes.Enabled = false;
            btnMovimento.Enabled = false;
        }

        private void dgvEncView_MouseClick(object sender, MouseEventArgs e)
        {
            if(dgvEncView.RowCount > 0)
                btnDetalhes.Enabled = true;
                btnMovimento.Enabled = true;
            
        }

        private void btnMovimento_Click(object sender, EventArgs e)
        {
            FormMoviment fde = new FormMoviment(int.Parse(dgvEncView.CurrentRow.Cells[0].Value.ToString()), dgvEncView.CurrentRow.Cells[4].Value.ToString(), double.Parse(dgvEncView.CurrentRow.Cells[5].Value.ToString()), double.Parse(dgvEncView.CurrentRow.Cells[6].Value.ToString()), double.Parse(dgvEncView.CurrentRow.Cells[7].Value.ToString()), double.Parse(dgvEncView.CurrentRow.Cells[8].Value.ToString()), double.Parse(dgvEncView.CurrentRow.Cells[9].Value.ToString()), double.Parse(dgvEncView.CurrentRow.Cells[10].Value.ToString()), dgvEncView.CurrentRow.Cells[11].Value.ToString(), dgvEncView.CurrentRow.Cells[12].Value.ToString(), dgvEncView.CurrentRow.Cells[13].Value.ToString(), dgvEncView.CurrentRow.Cells[14].Value.ToString(), dgvEncView.CurrentRow.Cells[15].Value.ToString(), dgvEncView.CurrentRow.Cells[16].Value.ToString());
            fde.ShowDialog();

            btnDetalhes.Enabled = false;
            btnMovimento.Enabled = false;

            methodReadEncomenda();

        }
        string lastStateMoviment;
        private void cboFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LOAD Estado
            methodReadEncomenda();
            lastStateMoviment = cboFiltro.Text;
        }

        private void btnNovaEnc_Click(object sender, EventArgs e)
        {
            FormTabEnc tbEnc = new FormTabEnc(_DBConn, _DBComm, _DBDreader, _DBDadapter, _DBCommBuilder, 0, idOff ,idUser, user_name, user_nickname);
            tbEnc.ShowDialog();

            methodReadEncomenda();
        }

        private void txtSearch_OnValueChanged(object sender, EventArgs e)
        {
            if(txtSearch.Text == string.Empty)
            {
                cboFiltro.Text = lastStateMoviment;
                methodReadEncomenda();
                
            }
            else
            {
                methodSearchEncomenda();
            }
            if(txtSearch.Text != "")
            {
                cboFiltro.Text = "Tudo";
            }
            
        }

        private void btnIconNovaEnc_Click(object sender, EventArgs e)
        {
            FormTabEnc tbEnc = new FormTabEnc(_DBConn, _DBComm, _DBDreader, _DBDadapter, _DBCommBuilder, 0, idOff, idUser, user_name, user_nickname);
            tbEnc.ShowDialog();

            methodReadEncomenda();
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            FormEncomendas enc = new FormEncomendas(_DBConn, _DBComm, _DBDreader, _DBDadapter, _DBCommBuilder, idOff, idUser,user_name, user_nickname);
            enc.Dispose();
        }


        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Correct");
        }

        //-----------------------------------------------------
        public void methodReadEncomenda()
        {
            _loader = new Loader();
            _loader.Show();

            connection = conn.methodConnect();
            try
            {

                dgvEncView.Rows.Clear();

                query = @"SELECT enc_id, enc_office_id, enc_user_id, enc_client_id, enc_tipo_pagamento ,enc_montante, enc_desconto, enc_transporte, enc_total, enc_total_global, enc_saldo, enc_receptor, enc_recp_contacto, enc_tipo_transporte, enc_estado, enc_dataEF, enc_dataEF FROM tb_encomenda WHERE enc_estado='" + cboFiltro.Text+"' ORDER BY enc_id DESC";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                
                while (rd.Read())
                {
                    dgvEncView.Rows.Add(
                        rd.GetInt32(0),
                        rd.GetInt32(1),
                        rd.GetInt32(2),
                        rd.GetInt32(3),
                        rd.GetString(4),
                        rd.GetDouble(5).ToString("N2"),
                        rd.GetDouble(6).ToString("N2"),
                        rd.GetDouble(7).ToString("N2"),
                        rd.GetDouble(8).ToString("N2"),
                        rd.GetDouble(9).ToString("N2"),
                        rd.GetDouble(10).ToString("N2"),
                        rd.GetString(11),
                        rd.GetString(12),
                        rd.GetString(13),
                        rd.GetString(14),
                        rd.GetString(15),
                        rd.GetString(16));
                }
                _loader.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar dados " + ex.Message);throw;
            }

        }

        //-----------------------------------------------------
        public void methodSearchEncomenda()
        {
            query = null;
            command.Parameters.Clear();
            rd.Close();

            try
            {

                dgvEncView.Rows.Clear();

                string query = @"SELECT enc_id, enc_office_id, enc_user_id, enc_client_id, enc_tipo_pagamento ,enc_montante, enc_desconto, enc_transporte, enc_total, enc_total_global, enc_saldo, enc_receptor, enc_recp_contacto, enc_tipo_transporte, enc_estado, enc_dataEF, enc_dataEF FROM tb_encomenda WHERE enc_receptor LIKE '%" + txtSearch.Text + "%' or enc_recp_contacto LIKE '%" + txtSearch.Text + "%' or enc_tipo_pagamento LIKE '%" + txtSearch.Text + "%' or enc_total_global LIKE '%" + txtSearch.Text + "%'";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();

                while (rd.Read())
                {
                    dgvEncView.Rows.Add(
                        rd.GetInt32(0),
                        rd.GetInt32(1),
                        rd.GetInt32(2),
                        rd.GetInt32(3),
                        rd.GetString(4),
                        rd.GetDouble(5).ToString("N2"),
                        rd.GetDouble(6).ToString("N2"),
                        rd.GetDouble(7).ToString("N2"),
                        rd.GetDouble(8).ToString("N2"),
                        rd.GetDouble(9).ToString("N2"),
                        rd.GetDouble(10).ToString("N2"),
                        rd.GetString(11),
                        rd.GetString(12),
                        rd.GetString(13),
                        rd.GetString(14),
                        rd.GetString(15),
                        rd.GetString(16));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar dados " + ex.Message);throw;
            }

        }
    }
}
