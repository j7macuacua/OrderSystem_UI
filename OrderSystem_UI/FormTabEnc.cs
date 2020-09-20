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
    public partial class FormTabEnc : Form
    {
        int indice, idUser, idOff;
        string _DBConn, _DBComm, _DBDreader, _DBDadapter, _DBCommBuilder, user_name, user_nickname;
        public FormTabEnc(string DBConn, string DBComm, string DBDreader, string DBDadapter, string DBCommBuilder, int index, int idOff, int idUser, string user_name, string user_nickname)
        {
            this.indice = index;
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
        Loader _loader = new Loader();
        FormLogin conn = new FormLogin();

        public void methodReset()
        {
            //LOAD

            dgvClient.Rows.Clear();
            dgvProd.Rows.Clear();
            dgvServ.Rows.Clear();
            dgvEnc.Rows.Clear();

            txtQuantEnc.Value=0;

            tabPanelCliente.Visible = false;
            tabPanelNovaEnc.Visible = false;
            txtQuantEnc.Enabled = false;
            btnListar.Enabled = false;
            btnDevolver.Enabled = false;
            txtTransporte.Enabled = false;
            txtValorPago.Enabled = false;

            btnAddNewClient.Enabled = false;
            cboTransp.Text = "Sem transporte";
            cboPaymentType.Text = "Sem pagamento";

            txtSearchClient.Text = "";
            txtSearchProduct.Text = "";
            txtRecpt.Text = "";
            txtRecptCont.Text = "";
            lblCliente.Text = "CLIENTE";

            txtTotal.Text = "0.00";
            txtSaldo.Text = "0.00";
            txtDesconto.Text = "0.00";
            txtPrettpagar.Text = "0.00";
            txtGlobal.Text = "0.00";
            txtTotalServ.Text = "0.00";
            txtTransporte.Text = "0.00";
            //txtValorPago.Text = "0.00";

            txtSaldo.ForeColor = Color.FromArgb(0, 150, 136);
            txtGlobal.ForeColor = Color.OrangeRed;
            txtValorPago.ForeColor = Color.OrangeRed;

            txtCont1.MaxLength = 9;
            txtCont2.MaxLength = 9;

            if (indice == 0)
            {
                tabPanelCliente.Visible = false;
                tabPanelNovaEnc.Visible = true;

                tabControl1.SelectedIndex = 0;
                txtQuantEnc.Enabled = false;
                txtSearchClient.Focus();
                //LOAD USERS
                //this.methodReadClient();

                //LOAD PRODUCTS
                //this.methodReadProd();

            }
            if (indice == 1)
            {
                tabPanelCliente.Visible = true;
                tabPanelNovaEnc.Visible = false;

                tabControl1.SelectedIndex = 1;
                methodReadClientList();
                //this.Cursor = Cursors.Default;

            }
            
        }
        private void tabEncForm_Load(object sender, EventArgs e)
        {
            methodReset();
        }

        //================================== INSERT ================================
        string resultCli;
        public void methodReadExistCategoria()
        {

            MySqlConnection connection = conn.methodConnect();

            if (txtNomeCliente.Text == String.Empty || txtCont1.Text == String.Empty)
            {
                MessageBox.Show("Preencha o espaço em branco!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
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
                            string queryy = "INSERT INTO tb_client (client_office_id, client_user_id, client_nome, client_dtAniversario, client_contacto1, client_contacto2, client_provincia, client_obs, client_dt) VALUES('" + nrr + "','" + nrr + "','" + txtNomeCliente.Text.ToUpper() + "','"+ dtAniversario.Value.Date +"'," + txtCont1.Text + "," + txtCont2.Text + ",'" + cboProvincia.Text.ToUpper() + "','" + txtObs.Text + "','" + dtpEncTime.Value.Date + "');";
                            MySqlCommand commando = new MySqlCommand(queryy, connection1);
                            commando.ExecuteNonQuery();
                            MessageBox.Show("Cliente " + txtNomeCliente.Text + " registado com Sucesso!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            methodReadClientList();

                            txtNomeCliente.Text = string.Empty;
                            txtCont1.Text = string.Empty;
                            txtCont2.Text = string.Empty;
                            txtObs.Text = string.Empty;
                        }
                        catch (Exception ex) { MessageBox.Show("Falha no registo de Cliente  >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); throw; }

                    }
                    else
                    {
                        MessageBox.Show("Já existe Cliente com esse Contacto:  " + resultCli, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        resultCli = null;
                    }

                }
                catch (Exception ex) { MessageBox.Show("Falha ao carregar Clientes >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); throw; }

            }

        }

        //====================================== READ ================================
        public void methodReadClientList()
        {
            //loginForm conn = new loginForm();
            MySqlConnection connection = conn.methodConnect();
            try
            {
                lstClient.Items.Clear();


                string query = "SELECT client_id, client_nome, client_contacto1, client_contacto2, client_provincia, client_obs, client_dt FROM tb_client ORDER BY client_nome";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {

                    lstClient.Items.Add(rd.GetString(0));
                    lstClient.Items[i].SubItems.Add(rd.GetString(1));
                    lstClient.Items[i].SubItems.Add(rd.GetString(2));
                    lstClient.Items[i].SubItems.Add(rd.GetString(3));
                    lstClient.Items[i].SubItems.Add(rd.GetString(4));
                    lstClient.Items[i].SubItems.Add(rd.GetString(5));
                    lstClient.Items[i].SubItems.Add(rd.GetString(6));


                    i++;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar dados " + ex.Message);
            }

        }
        //----------------------------------------777
        public void methodReadClient()
        {
            //loginForm conn = new loginForm();
            MySqlConnection connection = conn.methodConnect();
            try
            {
                dgvClient.Rows.Clear();

                string query = "SELECT client_id, client_nome, client_contacto1, client_provincia FROM tb_client";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {
                    dgvClient.Rows.Add(rd.GetInt32(0), rd.GetString(1), rd.GetString(2), rd.GetString(3));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar dados " + ex.Message);
            }

        }
        //----------------------------------------
        public void methodSearchClient()
        {
            //Limpar
            dgvClient.Rows.Clear();

            if (txtSearchClient.Text=="")
            {
                return;
            }

            FormLogin conn = new FormLogin();
            MySqlConnection connection = conn.methodConnect();

            try
            {

                string query = @"SELECT client_id, client_nome, client_contacto1, client_provincia FROM tb_client WHERE client_nome LIKE '%" + txtSearchClient.Text.ToUpper() + "%' OR client_contacto1 LIKE '%" + txtSearchClient.Text.ToUpper() + "%' OR client_contacto2 LIKE '%" + txtSearchClient.Text.ToUpper() + "%'";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader rd = command.ExecuteReader();
                
                while (rd.Read())
                {
                    dgvClient.Rows.Add(rd.GetInt32(0), rd.GetString(1), rd.GetString(2), rd.GetString(3));

                }


            }
            catch (Exception ex) { MessageBox.Show("Falha na junção de dados:: " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); throw; }
        }
        //----------------------------------------777

        public void  methodReadProd()
        {
            FormLogin conn = new FormLogin();
            MySqlConnection connection = conn.methodConnect();
            try
            {

                dgvProd.Rows.Clear();
                string query = "SELECT stk_prod_id, subcat_nome, stk_prod_nome, stk_qtd_disponivel, prod_precoCompra FROM tb_stock INNER JOIN tb_produto ON stk_prod_id = prod_id INNER JOIN tb_categoria ON cat_id = stk_cat_id INNER JOIN tb_subcategoria ON subcat_id = stk_subcat_id WHERE stk_qtd_disponivel > 0";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader rd = command.ExecuteReader(CommandBehavior.CloseConnection);

                //cmd.ExecuteReader(CommandBehavior.CloseConnection)

                while (rd.Read()==true)
                {
                    //dgvProd.Rows.Add(rd(0), rd(1), rd(2));
                    dgvProd.Rows.Add(rd.GetInt32(0), rd.GetString(1), rd.GetString(2), rd.GetInt32(3), rd.GetDouble(4));


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar dados " + ex.Message);
            }

        }
        //----------------------------------------777
        public void methodSearchProduct()
        {
            dgvProd.Rows.Clear();

            if (txtSearchProduct.Text == "")
            {
                return;
            }

            FormLogin conn = new FormLogin();
            MySqlConnection connection = conn.methodConnect();
            try
            {
   
                string query = "SELECT stk_prod_id, subcat_nome, stk_prod_nome, stk_qtd_disponivel, prod_precoCompra FROM tb_stock INNER JOIN tb_produto ON stk_prod_id = prod_id INNER JOIN tb_categoria ON cat_id = stk_cat_id INNER JOIN tb_subcategoria ON subcat_id = stk_subcat_id WHERE stk_prod_nome LIKE '%" + txtSearchProduct.Text.ToUpper() + "%'";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {
                    dgvProd.Rows.Add(rd.GetInt32(0), rd.GetString(1), rd.GetString(2), rd.GetInt32(3), rd.GetDouble(4));
                }

                foreach (DataGridViewRow row in dgvProd.Rows)
                {

                    if (int.Parse(row.Cells[3].Value.ToString()) == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.IndianRed;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar dados " + ex.Message);throw;
            }

        }
        //============================================
        double dprecoTotal;
        public void methodReadServList()
        {
            FormLogin conn = new FormLogin();
            MySqlConnection connection = conn.methodConnect();
            try
            {
                dgvServ.Rows.Clear();
                txtPrettpagar.Text = string.Empty;

                string query = "SELECT serv_id, serv_nome, serv_preco FROM tb_servicos WHERE serv_prod_id = '" + dgvProd.CurrentRow.Cells[0].Value.ToString() + "' ORDER BY serv_nome ASC";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader rd = command.ExecuteReader();
                int i = 0;
                dprecoTotal = 0;
                while (rd.Read())
                {
                    dgvServ.Rows.Add(rd.GetInt32(0), rd.GetString(1), rd.GetDouble(2), true);

                }

                txtPrettpagar.Text = dprecoTotal.ToString("N2");
                txtTotalServ.Text = dprecoTotal.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar dados " + ex.Message);
            }

        }

        private void btnSalvarClient_Click(object sender, EventArgs e)
        {
            //Save Cliente
            methodReadExistCategoria();
        }

        private void txtCont1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCont2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        int actualQuant;

        //============================================================
        //===============================================================
        public static void methodCurrency(ref TextBox txt)
        {
            string n = string.Empty;
            double v = 0;
            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");
                if (n.Equals(""))
                    n = "";
                n = n.PadLeft(3, '0');
                if (n.Length > 3 & n.Substring(0, 1) == "0")
                    n = n.Substring(1, n.Length - 1);
                v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;

            }
            catch (Exception ex) { MessageBox.Show("Falha na Conversão de dados:: " + ex.Message); }
        }

        private void btnAddNewClient_Click(object sender, EventArgs e)
        {
            
            if (txtSearchClient.Text=="" && dgvClient.RowCount > 0)
            {
                FormNewClient ncl = new FormNewClient("","");
                ncl.ShowDialog();
                methodReadClient();
            }
            else
            {
                if (Char.IsNumber(txtSearchClient.Text, 0))
                {
                    if (txtSearchClient.Text.Length == 9 && dgvClient.RowCount == 0)
                    {
                        //MessageBox.Show("Number");
                        FormNewClient ncl = new FormNewClient("Number",txtSearchClient.Text);
                        ncl.ShowDialog();
                        methodReadClient();
                    }
                }
                if (Char.IsLetter(txtSearchClient.Text, 0) && dgvClient.RowCount == 0)
                {
                    //MessageBox.Show("Text");
                    FormNewClient ncl = new FormNewClient("Text", txtSearchClient.Text);
                    ncl.ShowDialog();
                    methodReadClient();
                }
            }
        }

        //*************************************************************************
        int idClient=0;
        private void dgvClient_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvClient.RowCount > 0)
            {
                idClient = int.Parse(dgvClient.CurrentRow.Cells[0].Value.ToString());
                lblCliente.Text = dgvClient.CurrentRow.Cells[1].Value.ToString();
                txtRecpt.Text = dgvClient.CurrentRow.Cells[1].Value.ToString();
                txtRecptCont.Text = dgvClient.CurrentRow.Cells[2].Value.ToString();

                txtSearchProduct.Focus();
            }
        }

        int idProd = 0;
        private void dgvProd_MouseClick(object sender, MouseEventArgs e)
        {
            //LoadServics Selected

            if (dgvProd.RowCount>0)
            {
                btnListar.Enabled = true;
                txtQuantEnc.Enabled = true;

                methodReadServList();
                TotalServicos();
                TotalServicosSelected();
                idProd = int.Parse(dgvProd.CurrentRow.Cells[0].Value.ToString());
                txtQuantEnc.Maximum = int.Parse(dgvProd.CurrentRow.Cells[3].Value.ToString());
                txtQuantEnc.Value.Equals(0);
                txtQuantEnc.Focus();
            }

        }

        private void btnDevolver_Click(object sender, EventArgs e)
        {

            dgvEnc.Rows.Clear();

        }

        private void dgvEnc_MouseClick(object sender, MouseEventArgs e)
        {
          if(dgvEnc.RowCount > 0)
            {
                btnDevolver.Enabled = true;
            }
        }

        public void methodReadPricesServs()
        {
            //lstAssServ.Items.Clear();
            FormLogin conn = new FormLogin();
            MySqlConnection connection = conn.methodConnect();
            try
            {
                string[] arrayID = new string[0];

                string query = "SELECT serv_id, serv_nome, serv_preco FROM tb_servicos WHERE serv_id='" + arrayID[1] +"'";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {
                    int serv_id = rd.GetInt32(0);
                    string ServNome = rd.GetString(1);
                    double servPreco = rd.GetDouble(2);

                    i++;    
                }   

            }
            catch (Exception)
            {
                MessageBox.Show("Foi detectada acção de remoção de seleções", "Message",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }

        }

        private void dgvServ_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void dgvServ_MouseClick(object sender, MouseEventArgs e)
        {
            getSelectedDGVChecked();
        }

        private void btnEE_Click(object sender, EventArgs e)
        {
            //Encomendar
            if (lblCliente.Text=="CLIENTE")
            {
                MessageBox.Show("Por favor!\nSelecione o respectivo Cliente, o qual irá\nefectuar a encomenda!.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (txtRecpt.Text == string.Empty)
                {
                    MessageBox.Show("Insira o Receptor da encomenda.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (txtDesconto.Text == "" || txtValorPago.Text == string.Empty || txtSaldo.Text == "")
                    {
                        MessageBox.Show("Campo vazio detectado\nPor favor preenche-os!.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (Char.IsNumber(txtRecpt.Text, 0))
                        {
                            MessageBox.Show("Nome de receptor não válido", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (Char.IsLetter(txtDesconto.Text, 0) || Char.IsLetter(txtSaldo.Text, 0) || Char.IsLetter(txtValorPago.Text, 0))
                            {
                                MessageBox.Show("Caracter não válido detectado pelo sistema!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {

                                if (txtValorPago.Text=="0.00" || txtValorPago.Text == "0" || txtValorPago.Text == "0,00" || txtValorPago.Text == "00" || txtValorPago.Text == "0,0" || txtValorPago.Text == "0.0")
                                {
                                    if (MessageBox.Show("Efectuar encomenda sem pagamento ?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                    {
                                        //Method
                                        this._loader.Show();
                                        this.methodReadExistAndEncomendar();
                                    }
                                }
                                else
                                {
                                    //Method
                                    this._loader.Show();
                                    this.methodReadExistAndEncomendar();
                                }
                            }
                        }

                    }
                }
            }
        }

        //-------------------------------------------------------------------------
        private void btnListar_Click(object sender, EventArgs e)
        {
            //Listar Product
            if (txtQuantEnc.Text != "")
            {
                
                if(txtQuantEnc.Value==0)
                {
                    txtQuantEnc.Focus();
                }
                else
                {

                    foreach (DataGridViewRow row in dgvEnc.Rows)
                    {

                        if (row.Cells[0].Value.ToString() == dgvProd.CurrentRow.Cells[0].Value.ToString())
                        {
                            MessageBox.Show("Produto já existe na lista!", "Duplicação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtSearchProduct.Focus();
                            return;
                        }

                    }

                    dgvEnc.Rows.Add(dgvProd.CurrentRow.Cells[0].Value,0, dgvProd.CurrentRow.Cells[2].Value, "" , txtQuantEnc.Value, dgvProd.CurrentRow.Cells[4].Value, int.Parse(txtQuantEnc.Value.ToString()) * (double.Parse(dgvProd.CurrentRow.Cells[4].Value.ToString())), int.Parse(dgvProd.CurrentRow.Cells[3].Value.ToString()) - txtQuantEnc.Value);
                    
                    
                    foreach (DataGridViewRow row in dgvEnc.Rows)
                    {
                        
                        if (row.Cells[3].Value.ToString() == string.Empty)
                        {
                            row.DefaultCellStyle.BackColor = Color.Crimson;
                            row.DefaultCellStyle.ForeColor = Color.White;
                        }
            
                    }
                    getSelectedDGV();

                    SumGeral();
                    SumGlobal();
                }

                dgvServ.Rows.Clear();
                dgvProd.Rows.Clear();

            }

        }
        //+-----------------+--------------------+
        
        public void TotalServicos()
        {
            double sum = 0;
            foreach(DataGridViewRow row in dgvServ.Rows)
            {
                sum = sum + double.Parse(row.Cells[2].Value.ToString());
            }
            txtPrettpagar.Text = sum.ToString("N2");
        }

        public void TotalServicosSelected()
        {
            double sum = 0;
            foreach (DataGridViewRow row in dgvServ.Rows)
            {
                if (Convert.ToBoolean(row.Cells[3].Value)==true)
                {
                    sum = sum + double.Parse(row.Cells[2].Value.ToString());
                }
            }
            txtTotalServ.Text = sum.ToString("N2");
        }
        //==================================================
        public void SumGeral()
        {
            double sum = 0;
            foreach (DataGridViewRow row in dgvEnc.Rows)
            {
                sum = sum + double.Parse(row.Cells[6].Value.ToString());
                
            }
            txtTotal.Text = sum.ToString("N2");
            txtSaldo.Text = sum.ToString("N2");
            //txtValorPago.Text = "0.00";
        }

        private void txtDesconto_TextChanged(object sender, EventArgs e)
        {
            if (txtDesconto.Text != "")
            {
                SumGlobal();
                SumSaldo();
            }
            moeda(ref txtDesconto);
            
        }

        private void txtTransporte_TextChanged(object sender, EventArgs e)
        {
            if (txtTransporte.Text!="")
            {
                SumGlobal();
                SumSaldo();
            }
            moeda(ref txtTransporte);
            
        }

        //====================+============================
        public void getSelectedDGV()
        {
            string message = string.Empty;
            int id = 0;
            double priceServ = 0;
            foreach (DataGridViewRow row in dgvServ.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells[3].Value);
                if (isSelected)
                {
                    id = int.Parse(row.Cells[0].Value.ToString());
                    priceServ = double.Parse(row.Cells[2].Value.ToString());
                    message = row.Cells[1].Value.ToString();

                    //=======================================

                    dgvEnc.Rows.Add(dgvProd.CurrentRow.Cells[0].Value,
                        id,
                        "",
                        message,
                        txtQuantEnc.Value,
                        priceServ.ToString(),
                        int.Parse(txtQuantEnc.Value.ToString()) * (double.Parse(priceServ.ToString())));
                }
                
                //MessageBox.Show("Selected Values" + message);
            }

            //MessageBox.Show("Selected Values" + message);
            
        }

        private void txtValorPago_TextChanged(object sender, EventArgs e)
        {

            if (txtSaldo.Text != "")
            {
                SumSaldo();
            }
            moeda(ref txtValorPago);

            if (txtValorPago.Text == txtGlobal.Text)
            {
                txtGlobal.ForeColor = Color.FromArgb(0, 150, 136);
                txtValorPago.ForeColor = Color.FromArgb(0, 150, 136);
            }
            else
            {
                txtGlobal.ForeColor = Color.OrangeRed;
                txtValorPago.ForeColor = Color.OrangeRed;
            }
        }

        //=================+==========================+=====================

        public static void moeda(ref TextBox txt)
        {
            string n = string.Empty;
            double v = 0;
            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");
                if (n.Equals(""))
                    n = "";

                n = n.PadLeft(3, '0');
                if (n.Length > 3 & n.Substring(0, 1) == "0")
                    n = n.Substring(1, n.Length - 1);
                    v = Convert.ToDouble(n) / 100;
                    txt.Text = string.Format("{0:N}",v);
                    txt.SelectionStart = txt.Text.Length;
                
            }
            catch (Exception)
            {
            }
        }

        //------------------- + ------------------------ + ---------------

        public void getSelectedDGVChecked()
        {
            string message = string.Empty;
            double  sumChk=0;
            foreach (DataGridViewRow row in dgvServ.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells[3].Value);
                if (isSelected)
                {
                    sumChk = sumChk + double.Parse(row.Cells[2].Value.ToString());   
                }
            
            }
            txtTotalServ.Text = sumChk.ToString("N2");
        }

        private void cboTransp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboTransp.Text!= "Sem transporte")
            {
                txtTransporte.Enabled = true;
            }
            else
            {
                txtTransporte.Enabled = false;
                txtTransporte.Text = "0.00";
            }
        }

        private void txtDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))

                e.Handled = false;

            else if (char.IsNumber(e.KeyChar))

                e.Handled = false;

            else if (char.IsSeparator(e.KeyChar))

                e.Handled = false;

            else

                e.Handled = true;
        }

        private void txtSaldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))

                e.Handled = false;

            else if (char.IsNumber(e.KeyChar))

                e.Handled = false;

            else if (char.IsSeparator(e.KeyChar))

                e.Handled = false;

            else

                e.Handled = true;
        }

        private void txtRecptCont_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))

                e.Handled = false;

            else if (char.IsNumber(e.KeyChar))

                e.Handled = false;

            else if (char.IsSeparator(e.KeyChar))

                e.Handled = false;

            else

                e.Handled = true;
        }

        private void txtValorPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))

                e.Handled = false;

            else if (char.IsNumber(e.KeyChar))

                e.Handled = false;

            else if (char.IsSeparator(e.KeyChar))

                e.Handled = false;

            else

                e.Handled = true;
        }

        private void txtTransporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))

                e.Handled = false;

            else if (char.IsNumber(e.KeyChar))

                e.Handled = false;

            else if (char.IsSeparator(e.KeyChar))

                e.Handled = false;

            else

                e.Handled = true;
        }

        private void btnProcurarClient_Click(object sender, EventArgs e)
        {
            if (txtSearchClient.Text != "")
            {
                btnAddNewClient.Enabled = true;
                if (Char.IsNumber(txtSearchClient.Text, 0))
                {
                    if (txtSearchClient.Text.Length == 9)
                    {
                        txtSearchClient.BorderColorFocused = Color.LimeGreen;
                    }
                    else
                    {
                        txtSearchClient.BorderColorFocused = Color.Blue;
                    }

                }
            }
            if (txtSearchClient.Text == "")
            {
                btnAddNewClient.Enabled = false;
            }

            //Metodo Pesquisar
            methodSearchClient();
        }

        private void btnProcurarProd_Click(object sender, EventArgs e)
        {
            //Metodo Pesquisar
            methodSearchProduct();
        }

        private void tabEncForm_Shown(object sender, EventArgs e)
        {
            txtSearchClient.Focus();
        }

        private void cboPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboPaymentType.Text=="Sem pagamento")
            {
                txtValorPago.Enabled = false;
            }
            else {
                txtValorPago.Enabled = true;
            }
        }

        //------------------- + ------------------------ + ---------------

        //==================================================
        double desconto, global, saldo;
        public void SumGlobal()
        {
            double sum = 0;
            foreach (DataGridViewRow row in dgvEnc.Rows)
            {
                sum = sum + double.Parse(row.Cells[6].Value.ToString());

            }
            desconto = sum - double.Parse(txtDesconto.Text);
            global = desconto + double.Parse(txtTransporte.Text);

            txtGlobal.Text = global.ToString("N2");

        }

        public void SumSaldo()
        {
            if (txtValorPago.Text != string.Empty)
            {
                SumGlobal();
                saldo = global - double.Parse(txtValorPago.Text);

                txtSaldo.Text = saldo.ToString("N2");
                if (txtSaldo.Text != "0.00")
                    txtSaldo.ForeColor = Color.FromArgb(0, 150, 136);
                else
                    txtSaldo.ForeColor = Color.OrangeRed;
            }
        }

        //===============================================================================================
        int currentID;
        public void methodReadExistAndEncomendar()
        {
            

            MySqlConnection connection = conn.methodConnect();
            try
            {
               
                string _dtEF = Convert.ToString(dtpEncTime.Value.Day) + "-"+Convert.ToString(dtpEncTime.Value.Month) + "-"+Convert.ToString(dtpEncTime.Value.Year);
                string _dtET = Convert.ToString(dtpEntregaTime.Value.Day) + "-" + Convert.ToString(dtpEntregaTime.Value.Month) + "-" + Convert.ToString(dtpEntregaTime.Value.Year);
                
                string queryy = @"INSERT INTO tb_encomenda (enc_office_id, enc_user_id, enc_client_id, enc_prod_id, enc_tipo_pagamento ,enc_montante, enc_desconto, enc_transporte, enc_total, enc_total_global, enc_saldo, enc_receptor, enc_recp_contacto, enc_tipo_transporte, enc_estado, enc_dataEF, enc_dataET) VALUES(
                    '"+ idOff +"','"+ idUser +"','"+ idClient +"', '"+ idProd + "','" + cboPaymentType.Text+"','" + double.Parse(txtValorPago.Text) + "','" + double.Parse(txtDesconto.Text) + "','" + double.Parse(txtTransporte.Text) + "','" + double.Parse(txtTotal.Text) + "', '"+ double.Parse(txtGlobal.Text)+"' ,'" + double.Parse(txtSaldo.Text) + "','" + txtRecpt.Text + "','" + txtRecptCont.Text + "','" + cboTransp.Text + "','ENTRADA', '"+ _dtEF + "' , '" + _dtET + "');";
                MySqlCommand commando = new MySqlCommand(queryy, connection);
                commando.ExecuteNonQuery();
                
                MySqlCommand comIdent = new MySqlCommand("Select @@IDENTITY", connection);
                MySqlDataReader dr = comIdent.ExecuteReader();

                if (dr.Read() == true)
                {
                    currentID = dr.GetInt32(0);
                }
                dr.Close();

                foreach (DataGridViewRow row in dgvEnc.Rows)
                {
                    if (row.Cells[7].Value == null)
                    {
                        //Instructions
                    }
                    else
                    {
                        //Actualizar Quantidades diponiveis
                        string queryUp = @"UPDATE tb_stock SET stk_qtd_disponivel='" + row.Cells[7].Value + "' WHERE stk_prod_id='" + row.Cells[0].Value + "'";
                        MySqlCommand commandoUp = new MySqlCommand(queryUp, connection);
                        commandoUp.ExecuteNonQuery();
                    }

                    //Salvar detalhes da encomenda
                    string query = @"INSERT INTO tb_encomenda_detalhe (dt_enc_id, dt_prod_id, dt_serv_id, dt_quant, dt_preco) VALUES('"+ currentID +"', '"+ row.Cells[0].Value +"', '"+ row.Cells[1].Value +"', '"+row.Cells[4].Value+"', '"+ row.Cells[5].Value +"');";
                    MySqlCommand commandoER = new MySqlCommand(query, connection);
                    commandoER.ExecuteNonQuery();

                }

                //Movimentos /pagamento
                string queryMov = @"INSERT INTO tb_encomenta_movimento (enc_mov_estado, enc_mov_funcAtendente, enc_mov_data, enc_enc_mov_formPagamento, enc_mov_enc_id , enc_mov_montante) VALUES('ENTRADA', '"+ user_name+" "+user_nickname +"','" + _dtEF +"','"+ cboPaymentType.Text +"','" + currentID + "', '" + double.Parse(txtValorPago.Text) + "');";
                MySqlCommand commandoMov = new MySqlCommand(queryMov, connection);
                commandoMov.ExecuteNonQuery();

                MessageBox.Show("Registado com Sucesso!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                methodReset();
                this._loader.Close();
                this.Dispose();
            }
            catch (Exception ex) { MessageBox.Show("Falha no registo da Encomenda  >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); throw; }

        }
    }
}
