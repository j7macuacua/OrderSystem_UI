using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//----------------------------
using MySql.Data.MySqlClient;

namespace OrderSystem_UI
{
    public partial class FormDashboard : Form
    {
        FormLogin conn = new FormLogin();
        MySqlConnection connection = null;

        string query;
        MySqlCommand command = null;
        MySqlDataReader rd = null;
        public FormDashboard()
        {
            InitializeComponent();
        }
        Loader _loader = new Loader();
        private void FormDashboard_Load(object sender, EventArgs e)
        {
            //Load
            _loader = new Loader();
            _loader.Show();
            cboFiltroGPH1.Text = "ENTRADA";

            
            methodDashBoardLoadData();

            this._loader.Close();
        }

        

        //-----------------------------------------------------
        public void methodDashBoardLoadData()
        {
            connection = conn.methodConnect();

            try
            {
                
                chart1.Series["Series1"].Points.Clear();
                query = @"SELECT enc_receptor,enc_total FROM tb_encomenda WHERE enc_estado='"+ cboFiltroGPH1.Text +"' ORDER BY enc_id DESC LIMIT 5";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {
                    chart1.Series["Series1"].Points.AddXY(rd.GetString(0), rd.GetDouble(1));
                    chart1.Series["Series1"].Points[i].Label=rd.GetDouble(1).ToString("N2");
                    i++;
                }
                //-----------
                query = null;
                command.Parameters.Clear();
                rd.Close();

                query = @"SELECT enc_dataEF, SUM(enc_total_global) FROM tb_encomenda WHERE enc_dataEF=enc_dataEF GROUP BY enc_dataEF ORDER BY enc_id DESC LIMIT 7";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                while (rd.Read())
                {
                    chart2.Series["Series2"].Points.AddXY(rd.GetString(0), rd.GetDouble(1).ToString("N2"));
                }
                //-------------
                query = null;
                command.Parameters.Clear();
                rd.Close();

                chart3.Series["Series3"].Points.Clear();
                query = @"SELECT stk_prod_nome, stk_qtd_disponivel FROM tb_stock LIMIT 30";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();

                int inc = 0;
                while (rd.Read())
                {
                    chart3.Series["Series3"].Points.AddXY(rd.GetString(0), rd.GetInt32(1));
                    if (rd.GetInt32(1) >= 50)
                    {
                        chart3.Series["Series3"].Points[inc].Color = Color.Green;     
                    }
                    if (rd.GetInt32(1) <= 30)
                    {
                        chart3.Series["Series3"].Points[inc].Color = Color.Yellow;
                    }
                    if (rd.GetInt32(1) <= 15)
                    {
                        chart3.Series["Series3"].Points[inc].Color = Color.Orange;
                    }
                    if (rd.GetInt32(1) <= 5)
                    {
                        chart3.Series["Series3"].Points[inc].Color = Color.Red;
                    }

                    inc++;
                }
                //--------------------

                query = null;
                command.Parameters.Clear();
                rd.Close();

                query = @"SELECT COUNT(serv_id) FROM tb_servicos";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                while (rd.Read())
                {
                    lblServicos.Text = rd.GetString(0);
                }
                //-------------------=========== GENERAL MODE ===========----------------------------

                query = null;
                command.Parameters.Clear();
                rd.Close();

                query = @"SELECT COUNT(prod_id) FROM tb_produto";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                while (rd.Read())
                {
                    lblProd.Text = rd.GetString(0);
                }

                //-------------====--------------------
                query = null;
                command.Parameters.Clear();
                rd.Close();

                query = @"SELECT COUNT(enc_id) FROM tb_encomenda WHERE enc_estado='ENTRADA'";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                while (rd.Read())
                {
                    lblPendentes.Text = rd.GetString(0);
                }


                //-------------====--------------------
                query = null;
                command.Parameters.Clear();
                rd.Close();

                query = @"SELECT COUNT(enc_id) FROM tb_encomenda WHERE enc_estado='PRONTA'";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                while (rd.Read())
                {
                    lblProntos.Text = rd.GetString(0);
                }

                //-------------====--------------------
                query = null;
                command.Parameters.Clear();
                rd.Close();

                query = @"SELECT COUNT(client_id) FROM tb_client";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                while (rd.Read())
                {
                    lblCliente.Text = rd.GetString(0);
                }

                //-------------======= END =========--------------------
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar dados " + ex.Message);
            }

        }


        //------------------------------------------------------------
        private void cboFiltroGPH1_SelectedIndexChanged(object sender, EventArgs e)
        {
            methodDashBoardLoadData();
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }
        //-------------------------------------------SHADOW
        private const int CS_DropShadown = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DropShadown;
                return cp;
            }
        }

        
    }
}
