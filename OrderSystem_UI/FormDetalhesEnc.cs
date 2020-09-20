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
    public partial class FormDetalhesEnc : Form
    {
        string tipoPagamento, receptor, recepCont, tipoTransporte, estado, dtEF, dtET;
        double montate, desconto, transporte, total, global, saldo;
        int idEnc;

        public FormDetalhesEnc(int idEnc, string tipoPagamento, double montante, double desconto, double transporte, double total, double global, double saldo, string receptor, string recepCont, string tipoTransporte, string estado, string dtEF,  string dtET)
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
            this.dtET=dtET;

            InitializeComponent();
        }

        private void FormDetalhesEnc_Load(object sender, EventArgs e)
        {
            //Load
            txtReceptor.Text = receptor;
            txtRecpCont.Text = recepCont;
            txtdtET.Text = dtET;

            txtTipoPagamento.Text = tipoPagamento;
            txtMontantePago.Text = montate.ToString("N2");
            txtSaldoRest.Text = saldo.ToString("N2");
            txtDesconto.Text = desconto.ToString("N2");
            txtTransp.Text = transporte.ToString("N2");

            txtTotal.Text = total.ToString("N2");
            txtTotalGlobal.Text = global.ToString("N2");

            methodReadEncomendaDetalhes();

        }

        //-----------------------------------------------------
        public void methodReadEncomendaDetalhes()
        {
            FormLogin conn = new FormLogin();
            MySqlConnection connection = conn.methodConnect();
            try
            {
                dgvEncDetalhe.Rows.Clear();

                string query = "SELECT prod_nome, serv_nome, dt_quant, dt_preco FROM tb_produto RIGHT JOIN (tb_servicos RIGHT JOIN tb_encomenda_detalhe ON tb_servicos.serv_id=tb_encomenda_detalhe.dt_serv_id) ON tb_produto.prod_id=tb_encomenda_detalhe.dt_prod_id WHERE tb_encomenda_detalhe.dt_enc_id = '" + idEnc + "' ORDER BY tb_encomenda_detalhe.dt_id";


                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader rd = command.ExecuteReader();
                bool fg;
                string actProd= "";
                while (rd.Read())
                {
                    fg = bool.Parse(rd.IsDBNull(1).ToString());
                    if (fg == false)
                    {
                        

                        dgvEncDetalhe.Rows.Add(

                        rd.GetString(0),
                        rd.GetString(1),
                        rd.GetInt32(2),
                        rd.GetInt32(3));
                    }
                    else
                    {

                        dgvEncDetalhe.Rows.Add(

                        rd.GetString(0),
                        "",
                        rd.GetInt32(2),
                        rd.GetInt32(3));

                    }
                    foreach (DataGridViewRow row in dgvEncDetalhe.Rows)
                    {

                        if (row.Cells[1].Value.ToString() != "")
                        {
                            row.Cells[0].Value = "";

                        }

                    }

                }

                    

            }
            catch (Exception ex) {
                MessageBox.Show("Error :: ao Listar dados " + ex.Message); throw; }
            }

        }
    }

