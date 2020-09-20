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
    public partial class FormMoviment : Form
    {
        string tipoPagamento, receptor, recepCont, tipoTransporte, estado, dtEF, dtET;
        string varData = DateTime.Today.Date.Day + "/" + DateTime.Today.Date.Month + "/" + DateTime.Today.Date.Year;
        private void txtValorPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            
                e.Handled = false;
            
            else if (char.IsNumber(e.KeyChar))
            
                e.Handled = false;
            
            else if(char.IsSeparator(e.KeyChar))
            
                e.Handled = false;
            
            else
            
                e.Handled = true;
            
        }

        private void cboPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPaymentType.Text == "Sem pagamento")
            {
                txtValorPago.Enabled = false;
            }
            else
            {
                txtValorPago.Enabled = true;
            }
        }
        
        private void btnUpdateState_Click(object sender, EventArgs e)
        {
            //Update State

            //Actualizar Quantidades diponiveis
            if(cboFuncionarios.Text == "")
            {
                MessageBox.Show("Error :: \nSelecione o funcionario que realizou esta tarefa.  ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboFuncionarios.Focus();
            }
            else
            {
                FormLogin conn = new FormLogin();
                MySqlConnection connection = conn.methodConnect();
                try
                {
                    string queryUp = @"UPDATE tb_encomenda SET enc_estado='" + cboFiltro.Text + "', enc_saldo = '" + double.Parse(txtSaldo.Text) + "' WHERE enc_id='" + idEnc + "'";
                    MySqlCommand commandoUp = new MySqlCommand(queryUp, connection);
                    commandoUp.ExecuteNonQuery();

                    //Movimentos /pagamento
                    string queryMov = @"INSERT INTO tb_encomenta_movimento (enc_mov_estado, enc_mov_funcAtendente, enc_mov_data, enc_enc_mov_formPagamento, enc_mov_enc_id , enc_mov_montante) VALUES('" + cboFiltro.Text + "', '" + cboFuncionarios.Text + "','" + DateTime.Today.Date.ToShortDateString() + "','" + cboPaymentType.Text + "','" + idEnc + "', '" + double.Parse(txtValorPago.Text) + "');";
                    MySqlCommand commandoMov = new MySqlCommand(queryMov, connection);
                    commandoMov.ExecuteNonQuery();

                    MessageBox.Show("Estado actualizado com Sucesso!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                catch (Exception ex) { MessageBox.Show("Error :: Falha na actualização do estado da Encomenda  >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            
        }

        private void radUpDivida_CheckedChanged(object sender, EventArgs e)
        {
            cboFiltro.Text = estado;
        }

        private void radUpState_CheckedChanged(object sender, EventArgs e)
        {
            if (estado == "TRANSFERIDA")
            {
                disableMethod();
            }
            if (estado == "ENTRADA")
            {
                cboFiltro.Text = "ATENDIDA";
            }
            if (estado == "ATENDIDA")
            {
                cboFiltro.Text = "PRONTA";
            }
        }

        private void txtValorPago_TextChanged(object sender, EventArgs e)
        {
            if (txtSaldo.Text != "")
            {
                if (double.Parse(txtValorPago.Text.ToString()) > double.Parse(txtSaldoRest.Text.ToString()))
                {
                    txtValorPago.Text = double.Parse(txtSaldoRest.Text).ToString("N2");
                }

                if (txtValorPago.Text != string.Empty)
                {
                    saldo = double.Parse(txtSaldoRest.Text) - double.Parse(txtValorPago.Text);

                    txtSaldo.Text = saldo.ToString("N2");
                }
            }
        }

        void disableMethod()
        {
            btnUpdateState.Visible = false;
            Row1.Visible = false;
            Row2.Visible = false;

            panel4.Visible = false;
            lblSaldoDivida.Visible = false;
            lblValorPagar.Visible = false;

            cboPaymentType.Visible = false;
            txtValorPago.Visible = false;
            txtSaldo.Visible = false;
             
            bunifuSeparator1.Visible = false;
            bunifuSeparator13.Visible = false;
            bunifuSeparator14.Visible = false;
        }

        private void FormMoviment_Load(object sender, EventArgs e)
        {
            //Load
            carregarFun();

            txtReceptor.Text = receptor;
            txtRecpCont.Text = recepCont;
            txtdtET.Text = dtET;

            txtTipoPagamento.Text = tipoPagamento;
            txtMontantePago.Text = montate.ToString("N2");
            txtSaldoRest.Text = saldo.ToString("N2");
            txtDesconto.Text = desconto.ToString("N2");
            txtTransp.Text = transporte.ToString("N2");

            txtTotalGlobal.Text = global.ToString("N2");

            txtSaldo.Text = saldo.ToString("N2");
            txtValorPago.Text = "0";
            txtValorPago.Enabled = false;
            
            cboPaymentType.Text = "Sem pagamento";

            radUpState.Checked = true;

            if(estado == "TRANSFERIDA")
            {
                disableMethod();
            }
            if (estado == "ENTRADA")
            {
                cboFiltro.Text = "ATENDIDA";
            }
            if (estado == "ATENDIDA")
            {
                cboFiltro.Text = "PRONTA";
            }
            if (estado == "PRONTA")
            {
                btnUpdateState.Visible = false;
            }

            if (txtSaldoRest.Text == "0,00")
            {
                radUpDivida.Enabled = false;
                cboPaymentType.Enabled = false;
                radUpDivida.Visible = false;
            }
            else
            {
                radUpDivida.Enabled = true;
            }

            //
            FormLogin conn = new FormLogin();
            MySqlConnection connection = conn.methodConnect();
            try
            {
                dgvEncDetalhe.Rows.Clear();

                string query = "SELECT enc_mov_data, enc_mov_estado, enc_enc_mov_formPagamento, enc_mov_montante, enc_mov_funcAtendente FROM tb_encomenta_movimento WHERE enc_mov_enc_id = '" + idEnc + "' ORDER BY enc_mov_id";


                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader rd = command.ExecuteReader();

                while (rd.Read())
                {

                    dgvEncDetalhe.Rows.Add(

                        rd.GetString(0),
                        rd.GetString(1),
                        rd.GetString(2),
                        rd.GetDouble(3),
                        rd.GetString(4));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar dados " + ex.Message);
            }
        }

        double montate, desconto, transporte, total, global, saldo;
        int idEnc;

        public FormMoviment(int idEnc, string tipoPagamento, double montante, double desconto, double transporte, double total, double global, double saldo, string receptor, string recepCont, string tipoTransporte, string estado, string dtEF, string dtET)
        {

            this.idEnc = idEnc;
            this.tipoPagamento = tipoPagamento;
            this.montate = montante;
            this.desconto = desconto;
            this.transporte = transporte;
            this.total = total;
            this.global = global;
            this.saldo = saldo;
            this.receptor = receptor;
            this.recepCont = recepCont;
            this.tipoTransporte = tipoTransporte;
            this.estado = estado;
            this.dtEF = dtEF;
            this.dtET = dtET;

            InitializeComponent();
        }

        //============================================================
        public void carregarFun()
        {
            FormLogin conn = new FormLogin();
            MySqlConnection connection = conn.methodConnect();
            try
            {
                cboFuncionarios.Items.Clear();
                string query = "SELECT user_name, user_nickname FROM tb_user WHERE user_AccessLevel >='2'";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader rd = command.ExecuteReader();

                while (rd.Read())
                {
                    string nome = rd.GetString(0);
                    string apelido = rd.GetString(1);
                    cboFuncionarios.Items.Add(nome + " " + apelido);
                    cboFuncionarios.Text = "pelo funcionário";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar dados " + ex.Message);
            }
        }
    }
}
