using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//-------------------------
using System.IO;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace OrderSystem_UI
{
    public partial class FormtabProduto : Form
    {
        int indice;
        int idOff, idUser;
        public FormtabProduto(int index, int idOff, int idUser)
        {
            this.indice = index;

            this.idOff = idOff;
            this.idUser = idUser;

            InitializeComponent();
        }
        FormLogin conn = new FormLogin();
        MySqlConnection connection = null;
        string query;
        MySqlCommand command = null;
        MySqlDataReader rd = null;

        //---------------------------------------

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
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;

            }
            catch (Exception)
            {
            }
        }
        private void tabProduto_Load(object sender, EventArgs e)
        {
            //lOAD
            

            btnActalizarCat.Enabled = false;
            btnActualizarSubcat.Enabled = false;
            btnActualizarProd.Enabled = false;

            btnApagarSubcat.Enabled = false;
            btnApagarCat.Enabled = false;
            btnApagarProduct.Enabled = false;

            btnSalvarCategoria.Enabled = false;
            btnSalvarSubcat.Enabled = false;
            btnSalvarProd.Enabled = false;

            btnAddStock.Enabled = false;
            btnDevStock.Enabled = false;
            btnAtribuirQuant.Enabled = false;

            gpbQuantDani.Visible = false;
            methodReadCategoria();

            if (indice == 0)
            {
                tabControl1.SelectedIndex = 0;
                methodReadProdList();

                btnSalvarProd.Enabled = true;

            }
            if (indice == 1)
            {
                tabControl1.SelectedIndex = 1;
                

                btnSalvarCategoria.Enabled = true;
            }
            if (indice == 2)
            {
                tabControl1.SelectedIndex = 2;
                methodReadSubcategoriaList();

                btnSalvarSubcat.Enabled = true;
            }
            if (indice == 3)
            {
                tabControl1.SelectedIndex = 3;
                methodReadProdExist();
                methodReadProdExistStock();

            }
            if (indice == 4)
            {
                tabControl1.SelectedIndex = 4;

                methodReadServList();
            }
            
        }

        //===========================INSERT====================================
        string resultCat, resultSubcat, resultProd;
        public void methodReadExistCategoria()
        {
            query = null;
            command.Parameters.Clear();
            rd.Close();

            if (txtCategoria.Text == String.Empty)
            {
                MessageBox.Show("Insira o Nome do produto!","Message",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            else
            {
                try
                {
                    query = "SELECT cat_nome FROM tb_categoria WHERE cat_nome='" + txtCategoria.Text.ToUpper()+"'";
                    command = new MySqlCommand(query, connection);
                    rd = command.ExecuteReader();

                    while (rd.Read())
                    {
                       resultCat = rd.GetString(0);
                    }

                    if (resultCat == null)
                    {
                        try
                        {
                            query = null;
                            command.Parameters.Clear();
                            rd.Close();

                            query = "INSERT INTO tb_categoria(cat_nome) VALUES('" + txtCategoria.Text.ToUpper() + "')";
                            command = new MySqlCommand(query, connection);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Categoria " + txtCategoria.Text + " registada com Sucesso!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.methodReadCategoria();
                            txtCategoria.Text = string.Empty;
                        }
                        catch (Exception ex) { MessageBox.Show("Falha no registo de categoria!  >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);}

                    }
                    else
                    {
                        MessageBox.Show("Categoria existente!  >> ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        resultCat = null;
                    }

                }
                catch (Exception ex) { MessageBox.Show("Falha ao carregar categorias >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);  }
                
            }

        }
        //-----------------------------INSERT----
        public void methodReadExistSubCategoria()
        {

            query = null;
            command.Parameters.Clear();
            rd.Close();

            if (txtSubcategoria.Text == String.Empty)
            {
                MessageBox.Show("Insira o Nome da Subcategoria", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                try
                {
                    query = "SELECT subcat_nome FROM tb_subcategoria WHERE subcat_nome='" + txtSubcategoria.Text.ToUpper() + "'";
                    command = new MySqlCommand(query, connection);
                    rd = command.ExecuteReader();

                    while (rd.Read())
                    {
                        resultSubcat = rd.GetString(0);
                    }

                    if (resultSubcat == null)
                    {
                        try
                        {
                            query = null;
                            command.Parameters.Clear();
                            rd.Close();

                            string queryy = "INSERT INTO tb_subcategoria(subcat_cat_id, subcat_nome) VALUES('"+ idCategoriaOnly +"','" + txtSubcategoria.Text.ToUpper() + "')";
                            command = new MySqlCommand(query, connection);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Subcategoria " + txtCategoria.Text + " registada com Sucesso!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            methodReadCategoria();
                            txtSubcategoria.Text = string.Empty;
                        }
                        catch (Exception ex) { MessageBox.Show("Falha no registo de Subcategoria!  >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);  }

                    }
                    else
                    {
                        MessageBox.Show("Subcategoria existente!  >> ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        resultSubcat = null;
                    }

                }
                catch (Exception ex) { MessageBox.Show("Falha ao carregar registo de Subcategorias >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            }

        }

        string varData = DateTime.Today.Date.Day + "/" + DateTime.Today.Date.Month + "/" + DateTime.Today.Date.Year;

        //============================== INSERT==========================
        public void methodReadExistProduct()
        {

            query = null;
            command.Parameters.Clear();
            rd.Close();

            if (idCategoriaOnly==0 || idSubCategoria==0 || txtProduct.Text == String.Empty || txtPrecoCompra.Text == String.Empty || txtPrecoVenda.Text == String.Empty)
            {
                MessageBox.Show("Operação não valida ::\nPreecha o espaço em branco", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                try
                {
                    query = "SELECT prod_nome FROM tb_produto WHERE prod_cat_id='" + idCategoriaOnly + "' AND prod_subcat_id='"+idSubCategoria+"' AND prod_nome='"+txtProduct.Text.ToUpper()+"'";
                    command = new MySqlCommand(query, connection);
                    rd = command.ExecuteReader();

                    while (rd.Read())
                    {
                        resultProd = rd.GetString(0);
                    }

                    if (resultProd == null)
                    {
                        try
                        {
                            

                            query = null;
                            command.Parameters.Clear();
                            rd.Close();

                            query = "INSERT INTO tb_produto(prod_cat_id, prod_subcat_id, prod_nome, prod_precoCompra, prod_precoVenda, prod_data, prod_office_id, prod_user_id) VALUES('" + idCategoriaOnly + "','" + idSubCategoria + "','"+txtProduct.Text.ToUpper()+"','"+Convert.ToDecimal(txtPrecoCompra.Text)+ "','" + Convert.ToDecimal(txtPrecoVenda.Text) + "','" + varData + "','"+ idOff +"','"+ idUser +"')";
                            command = new MySqlCommand(query, connection);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Subcategoria " + txtProduct.Text + " com preço "+txtPrecoCompra.Text+" registada com Sucesso!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //methodReadCategoria();
                            txtProduct.Text = string.Empty;
                            txtPrecoCompra.Text = string.Empty;
                            txtPrecoVenda.Text = string.Empty;
                        }
                        catch (Exception ex) { MessageBox.Show("Falha no registo do Produto!  >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); throw; }

                    }
                    else
                    {
                        MessageBox.Show("Produto existente!  >> ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        resultProd = null;
                    }

                }
                catch (Exception ex) { MessageBox.Show("Falha ao carregar registo de Subcategorias >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            }

        }

        //============================== INSERT==========================
        public void methodReadExistService()
        {

            
            if (idCategoriaOnly == 0 || idSubCategoria == 0 || lblObjctAtb.Text == "notSet" || lblObjetTargetName.Text == "notGet" || txtTypeServ.Text==string.Empty || txtPrecoServ.Text == string.Empty)
            {
                MessageBox.Show("Operação não valida ::\nPreecha o espaço em branco", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                try
                {
                    query = null;
                    command.Parameters.Clear();
                    rd.Close();

                    query = "SELECT serv_nome FROM tb_servicos WHERE serv_nome='"+ txtTypeServ.Text.ToUpper() + "' AND serv_cat_id='"+lblObjctAtb.Text+"'";
                    command = new MySqlCommand(query, connection);
                    rd = command.ExecuteReader();

                    while (rd.Read())
                    {
                        resultProd = rd.GetString(0);
                    }

                    if (resultProd == null)
                    {
                        try
                        {
                            query = null;
                            command.Parameters.Clear();
                            rd.Close();

                            query = "INSERT INTO tb_servicos(serv_cat_id, serv_subcat_id, serv_prod_id, serv_nome, serv_preco, serv_data, serv_office_id, serv_user_id) VALUES('" + idCategoriaOnly + "','" + idSubCategoria + "','" + lblObjctAtb.Text + "','" + txtTypeServ.Text.ToUpper() + "','" + Convert.ToDecimal(txtPrecoServ.Text)+ "','"+ dateTimePicker1.Value.Date+"','"+ idOff +"','" + idUser+ "')";
                            command = new MySqlCommand(query, connection);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Registado com sucesso","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            methodReadCurrentServList();

                            txtPrecoServ.Text = string.Empty;
                            txtTypeServ.Text = string.Empty;
                            lblObjctAtb.Text = "notSet";
                            lblObjetTargetName.Text = "notGet";
                        }
                        catch (Exception ex) { MessageBox.Show("Falha no registo de Serviço!  >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                    }
                    else
                    {
                        MessageBox.Show("Serviço existente!  >> ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        resultProd = null;
                    }

                }
                catch (Exception ex) { MessageBox.Show("Falha ao carregar registo de Serviço >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); throw; }

            }

        }
        //===============================================================
        
        public void methodReadCategoria()
        {
            connection = conn.methodConnect();

            try
            {
                lstCategoria.Items.Clear();
                cboCatToServ.Items.Clear();
                cboCategoriaSub.Items.Clear();
                cboCategoriaProd.Items.Clear();

                

                query = "SELECT * FROM tb_categoria";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {
                   
                    int idCat = rd.GetInt32(0);
                    string nomeCat = rd.GetString(1);

                    lstCategoria.Items.Add(idCat.ToString());
                    lstCategoria.Items[i].SubItems.Add(nomeCat);

                    cboCatToServ.Items.Add(nomeCat);

                    cboCategoriaProd.Items.Add(nomeCat);
                    cboCategoriaSub.Items.Add(nomeCat);
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao Carregar Dados de Prduto  >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); throw;
            }

        }
        //===============================================================

        public void methodReadProductList()
        {
            query = null;
            command.Parameters.Clear();
            rd.Close();

            try
            {
                lstProdut.Items.Clear();

                query = "SELECT cat_nome, subcat_id, subcat_nome FROM tb_categoria INNER JOIN tb_subcategoria ON cat_id = subcat_cat_id ORDER BY cat_nome";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {

                    string catNome = rd.GetString(0);
                    int subcat_id = rd.GetInt32(1);
                    string subcatNome = rd.GetString(2);

                    lstProdut.Items.Add(subcat_id.ToString());
                    lstProdut.Items[i].SubItems.Add(catNome);
                    lstProdut.Items[i].SubItems.Add(subcatNome);

                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar de Produtos " + ex.Message);
            }

        }
        //===============================================================
        public void methodReadSubcategoriaList()
        {
            query = null;
            command.Parameters.Clear();
            rd.Close();

            try
            {
                lstSubcategoria.Items.Clear();

                query = "SELECT cat_nome, subcat_id, subcat_nome FROM tb_categoria INNER JOIN tb_subcategoria ON cat_id = subcat_cat_id ORDER BY cat_nome";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {

                    string catNome = rd.GetString(0);
                    int subcat_id = rd.GetInt32(1);
                    string subcatNome = rd.GetString(2);

                    lstSubcategoria.Items.Add(subcat_id.ToString());
                    lstSubcategoria.Items[i].SubItems.Add(catNome);
                    lstSubcategoria.Items[i].SubItems.Add(subcatNome);

                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar dados "+ex.Message);
            }

        }
        //===============================================================
        int stockQuant = 0, stockQtDamaged=0;
        public void methodReadExistProdStock()
        {
            query = null;
            command.Parameters.Clear();
            rd.Close();

            try
                {
                    query = "SELECT stk_prod_nome FROM tb_stock WHERE stk_prod_id='" + lstProdExist.SelectedItems[0].Text + "' AND stk_prod_nome='" + lstProdExist.SelectedItems[0].SubItems[1].Text + "'";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader rd = command.ExecuteReader();

                    while (rd.Read())
                    {
                        resultProd = rd.GetString(0);
                    }

                    if (resultProd == null)
                    {
                        try
                        {
                        //----------------------
                        query = null;
                        command.Parameters.Clear();
                        rd.Close();
                        //------------------------

                        query = "INSERT INTO tb_stock(stk_prod_id, stk_cat_id, stk_subcat_id, stk_prod_nome, stk_office_id, stk_qtd_disponivel, stk_qtd_danificada) VALUES('" + toStockProdID + "' ,'" + stkIDCat + "' ,'" + stkIDSubcat + "' ,'" + toStockProdNome + "','"+ idOff +"','" + stockQuant + "','"+ stockQtDamaged + "')";
                        command = new MySqlCommand(query, connection);
                        command.ExecuteNonQuery();

                            methodReadProdExistStock();
                            toStockProdID =0;
                            toStockProdNome = null;
                        }
                        catch (Exception ex) { MessageBox.Show("Falha ao Adicionar no Stock >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                    }
                    else
                    {
                        MessageBox.Show("Produto já Existente na lista de Stock  >> ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        resultProd = null;
                    }

                }
                catch (Exception ex) { MessageBox.Show("Falha no processo de analise no Stock >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            

        }
        public void methodReadProdList()
        {
            //----------------------
            query = null;
            command.Parameters.Clear();
            rd.Close();
            //------------------------

            try
            {
                lstProdut.Items.Clear();


                query = "SELECT cat_nome, subcat_nome, prod_id, prod_nome, prod_precoCompra, prod_precoVenda, prod_data FROM tb_categoria INNER JOIN tb_subcategoria ON cat_id = subcat_cat_id INNER JOIN tb_produto ON subcat_id = prod_subcat_id";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {

                    string catNome = rd.GetString(0);
                    string subcatNome = rd.GetString(1);
                    int prod_id = rd.GetInt32(2);
                    string prodNome = rd.GetString(3);
                    string prodPrecoC = rd.GetString(4);
                    string prodPrecoV = rd.GetString(5);
                    string prodData = rd.GetString(6);
                    double d; 

                    lstProdut.Items.Add(prod_id.ToString());
                    lstProdut.Items[i].SubItems.Add(catNome);
                    lstProdut.Items[i].SubItems.Add(subcatNome);
                    lstProdut.Items[i].SubItems.Add(prodNome);
                    lstProdut.Items[i].SubItems.Add(prodPrecoC);
                    lstProdut.Items[i].SubItems.Add(prodPrecoV);
                    lstProdut.Items[i].SubItems.Add(prodData);

                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar dados " + ex.Message);
            }

        }
        //===============================================================
        int idCategoriaOnly=0;
        public void methodReadIDCat()
        {
            //----------------------
            query = null;
            command.Parameters.Clear();
            rd.Close();
            //------------------------
            try
            {
                query = "SELECT cat_id FROM tb_categoria WHERE cat_nome='"+cboCategoriaSub.Text.ToUpper()+ "' OR cat_nome='" + cboCategoriaProd.Text.ToUpper() + "' OR cat_nome='" + cboCatToServ.Text.ToUpper() + "'";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {
                   idCategoriaOnly = int.Parse(rd.GetString(0));
                  // MessageBox.Show(idCategoriaOnly.ToString());

                    i++;
                }

            }
            catch (Exception ex) { MessageBox.Show("Falha ao Carregar ID-categoria  >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); throw; }
        }
        int idSubCategoria=0;
        //===============================================================
        public void methodReadIDSub()
        {
            //----------------------
            query = null;
            command.Parameters.Clear();
            rd.Close();
            //------------------------

            try
            {
                query = "SELECT subcat_id FROM tb_subcategoria WHERE subcat_nome='" + cboSubcatProd.Text.ToUpper() + "' OR subcat_nome='" + cboSubcatToServ.Text.ToUpper() + "'";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {
                    idSubCategoria = int.Parse(rd.GetString(0));
                    //MessageBox.Show(idSubCategoria.ToString());

                    i++;
                }

            }
            catch (Exception ex) { MessageBox.Show("Falha ao Carregar ID-categoria  >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); throw; }
        }
        //===============================================================
        public static void methodCurrency(ref TextBox txt)
        {
            string n = string.Empty;
            double v = 0;
            try
            {
                n=txt.Text.Replace(",", "").Replace(".","");
                if (n.Equals(""))
                    n = "";
                n = n.PadLeft(3, '0');
                if (n.Length > 3 & n.Substring(0, 1) == "0")
                    n = n.Substring(1, n.Length-1);
                v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
                
            }
            catch (Exception ex) { MessageBox.Show("Falha na Conversão de dados:: " + ex.Message); }
        }
        //---------------------------------------------------------------
        int stkIDCat = 0 , stkIDSubcat = 0;
        public void methodReadProdExist()
        {
            //----------------------
            query = null;
            command.Parameters.Clear();
            rd.Close();
            //------------------------
            try
            {
                lstProdExist.Items.Clear();
                

                query = "SELECT prod_id, prod_cat_id, prod_subcat_id, prod_nome, prod_precoVenda FROM tb_produto";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {

                    int prod_id = rd.GetInt32(0);
                    stkIDCat = rd.GetInt32(1);
                    stkIDSubcat = rd.GetInt32(2);
                    string prodNome = rd.GetString(3);
                    string prodPreco = rd.GetString(4);

                    lstProdExist.Items.Add(prod_id.ToString());
                    lstProdExist.Items[i].SubItems.Add(prodNome);
                    lstProdExist.Items[i].SubItems.Add(prodPreco);
                    lstProdExist.Items[i].SubItems.Add(stkIDCat.ToString());
                    lstProdExist.Items[i].SubItems.Add(stkIDSubcat.ToString());

                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar dados " + ex.Message);
            }

        }
        //»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»
        public void methodReadProdExistStock()
        {
            //----------------------
            query = null;
            command.Parameters.Clear();
            rd.Close();
            //------------------------
            try
            {
                lstStockExist.Items.Clear();

                query = "SELECT stk_prod_id, stk_prod_nome, stk_qtd_disponivel FROM tb_stock";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {

                    int stock_prod_id = rd.GetInt32(0);
                    string strock_prodNome = rd.GetString(1);
                    int stock_Quant = rd.GetInt32(2);

                    lstStockExist.Items.Add(stock_prod_id.ToString());
                    lstStockExist.Items[i].SubItems.Add(strock_prodNome);
                    lstStockExist.Items[i].SubItems.Add(stock_Quant.ToString());

                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar dados " + ex.Message);
            }

        }

        //»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»
        public void methodReadProdExistServ()
        {
            //----------------------
            query = null;
            command.Parameters.Clear();
            rd.Close();
            //------------------------
            try
            {
                lstProdToServ.Items.Clear();

                query = "SELECT prod_id, prod_nome FROM tb_produto WHERE prod_subcat_id='"+ idSubCategoria + "'";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {

                    int prod_id = rd.GetInt32(0);
                    string prodNome = rd.GetString(1);

                    lstProdToServ.Items.Add(prod_id.ToString());
                    lstProdToServ.Items[i].SubItems.Add(prodNome);

                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar dados " + ex.Message);
            }

        }
        //===============================================================
        public void methodReadCurrentServList()
        {
            //----------------------
            query = null;
            command.Parameters.Clear();
            rd.Close();
            //------------------------
            try
            {
                lstCurrentServProd.Items.Clear();


                query = "SELECT serv_id, serv_nome, serv_preco FROM tb_servicos WHERE serv_prod_id="+ int.Parse(lblObjctAtb.Text) + "";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {
                    int serv_id = rd.GetInt32(0);
                    string ServNome = rd.GetString(1);
                    string servPreco = rd.GetString(2);
                    

                    lstCurrentServProd.Items.Add(serv_id.ToString());
                    lstCurrentServProd.Items[i].SubItems.Add(ServNome);
                    lstCurrentServProd.Items[i].SubItems.Add(double.Parse(servPreco).ToString("N2"));

                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar dados " + ex.Message);
            }

        }

        public void methodReadServList()
        {
            //----------------------
            query = null;
            command.Parameters.Clear();
            rd.Close();
            //------------------------
            try
            {
                lstServsTotal.Items.Clear();

                query = "SELECT serv_id, prod_nome, serv_nome, serv_preco FROM tb_servicos INNER JOIN tb_produto ON prod_id = serv_prod_id";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {
                    int serv_id = rd.GetInt32(0);
                    string ProdNome = rd.GetString(1);
                    string ServNome = rd.GetString(2);
                    string servPreco = rd.GetString(3);

                    lstServsTotal.Items.Add(serv_id.ToString());
                    lstServsTotal.Items[i].SubItems.Add(ProdNome);
                    lstServsTotal.Items[i].SubItems.Add(ServNome);
                    lstServsTotal.Items[i].SubItems.Add(double.Parse(servPreco).ToString("N2"));

                    i++;
                    lblServTotais.Text = "Serviços totais: " + i.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :: ao Listar dados " + ex.Message);
            }

        }
        //»»»»»»»»»»»»»»»»»»»»»»»»»»»  UPDATE  »»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»
        public void methodUpdateQuantStock()
        {
            //----------------------
            query = null;
            command.Parameters.Clear();
            rd.Close();
            //------------------------
            try
            {
                int totalQuant = int.Parse(txtQuantActual.Text) + int.Parse(txtQuanty.Text);
                query = "UPDATE tb_stock SET stk_qtd_disponivel='" + totalQuant + "' WHERE stk_prod_id ='" + toStockProdID + "'";
                command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Quantidade actualizada com Sucesso!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                methodReadProdExistStock();

                toStockProdID = 0;
                txtQuantActual.Text = string.Empty;
                txtQuanty.Text = string.Empty;


                btnAtribuirQuant.Enabled = false;
                gpbQuantDani.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: ::" + ex.Message);
            }
        }

        //»»»»»»»»»»»»»»»»»»»»»»»»»»»  UPDATE  »»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»
        public void methodUpdateQuantStockDamaged()
        {
            //----------------------
            query = null;
            command.Parameters.Clear();
            rd.Close();
            //------------------------
            try
            {
                int totalQuant = int.Parse(txtDaniTotal.Text) - int.Parse(txtDaniActual.Value.ToString());
                query = "UPDATE tb_stock SET stk_qtd_disponivel='"+ totalQuant + "', stk_qtd_danificada='" + txtDaniActual.Value.ToString() + "' WHERE stk_prod_id ='" + toStockProdID + "'";
                command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Quantidade Danificada actualizada com Sucesso!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                methodReadProdExistStock();

                toStockProdID = 0;
                txtQuantActual.Text = string.Empty;
                txtDaniTotal.Text = string.Empty;
                txtDaniActual.Value = 0;


                btnAtribuirQuant.Enabled = false;
                gpbQuantDani.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: ::" + ex.Message);
            }
        }
        //»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»  DELETE »»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»
        public void methodDeleteProdStrock()
        {
            
            try
            {
                query = "DELETE FROM tb_stock WHERE stk_prod_id ='" + lstStockExist.SelectedItems[0].Text + "'";
                command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                btnDevStock.Enabled = false;
                methodReadProdExistStock();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error na devolução do Stock::" + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        public void methodReadSubcat()
        {
            //----------------------
            query = null;
            command.Parameters.Clear();
            rd.Close();
            //------------------------
            try
            {
                cboSubcatProd.Items.Clear();
                cboSubcatToServ.Items.Clear();
                query = "SELECT subcat_nome FROM tb_subcategoria WHERE subcat_cat_id='" + idCategoriaOnly + "'";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                
                while (rd.Read())
                {
                    cboSubcatProd.Items.Add(rd.GetString(0));
                    cboSubcatToServ.Items.Add(rd.GetString(0));
                    //MessageBox.Show(rd.GetString(0));

                }

            }
            catch (Exception ex) { MessageBox.Show("Falha ao Carregar ID-categoria  >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); throw; }


        }
        private void btnSalvarCategoria_Click(object sender, EventArgs e)
        {
            //Salvar Categoria
            methodReadExistCategoria();
            //-------------------------------
        }

        int idCategoria, idProdutoOnly=0;
        private void lstCategoria_MouseClick(object sender, MouseEventArgs e)
        {
            idCategoria = int.Parse(lstCategoria.SelectedItems[0].Text);
            txtCategoria.Text = lstCategoria.SelectedItems[0].SubItems[1].Text;

            btnActalizarCat.Enabled = true;
            btnApagarCat.Enabled = true;
        }

        private void cboCategoriaSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Load ID from categoria
            methodReadIDCat();

            txtSubcategoria.Text = string.Empty;
        }

        private void btnSalvarSubcat_Click(object sender, EventArgs e)
        {
            //Save Subcategoria
            methodReadExistSubCategoria();
            //Carregar com JOIN Cat and Subcat
            methodReadSubcategoriaList();
        }

        int idSubcategoriaSelected;
        private void lstSubcategoria_MouseClick(object sender, MouseEventArgs e)
        {
            idSubcategoriaSelected = 0;
            cboCategoriaSub.Text = string.Empty;
            txtSubcategoria.Text = string.Empty;

            idSubcategoriaSelected = int.Parse(lstSubcategoria.Items[0].Text);
            cboCategoriaSub.Text = lstSubcategoria.SelectedItems[0].SubItems[1].Text;
            txtSubcategoria.Text = lstSubcategoria.SelectedItems[0].SubItems[2].Text;
        }

        private void btnCancelSub_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancelarCategoria_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancelProd_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnActalizarCat_Click(object sender, EventArgs e)
        {
            //LoadActualizar categoria
            
            if (txtCategoria.Text != string.Empty)
            {
                try
                {
                    //----------------------
                    query = null;
                    command.Parameters.Clear();
                    rd.Close();
                    //------------------------
                    query = "UPDATE tb_categoria SET cat_nome='" + txtCategoria.Text.ToUpper() + "' WHERE cat_id ='"+ idCategoria + "'";
                    command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Categoria " + txtCategoria.Text + " Actualizada com Sucesso!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCategoria.Text = string.Empty;
                    this.methodReadCategoria();

                    btnActalizarCat.Enabled = false;
                    btnApagarCat.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: ::"+ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Error: Insira o nome da Categoria ","Message",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }

        }

        private void btnApagarCat_Click(object sender, EventArgs e)
        {
            //Delete
            if (MessageBox.Show("Deseja mesmo Apagar Categoria :: "+ lstCategoria.SelectedItems[0].SubItems[1].Text +" ?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                //----------------------
                query = null;
                command.Parameters.Clear();
                rd.Close();
                //------------------------

                try
                {
                    query = "DELETE FROM tb_categoria WHERE cat_id ='" + idCategoria + "'";
                    command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Categoria " + txtCategoria.Text + " Removida com Sucesso!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCategoria.Text = string.Empty;
                    this.methodReadCategoria();

                    btnActalizarCat.Enabled = false;
                    btnApagarCat.Enabled = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error na tentativa de Apagar Categoria! ::" + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            
        }

        private void cboSubcatProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Select ID
            if (cboSubcatProd.Text == "")
            {
                MessageBox.Show("Error :: Seleccione a respectiva Categoria!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Select ID
                methodReadIDSub();

                txtProduct.Text = string.Empty;
                txtPrecoCompra.Text = string.Empty;

            }

        }

        private void btnApagarProduct_Click(object sender, EventArgs e)
        {
            //Delete
            if (MessageBox.Show("Deseja mesmo Apagar Produto :: " + lstProdut.SelectedItems[0].SubItems[3].Text + " ?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    //----------------------
                    query = null;
                    command.Parameters.Clear();
                    rd.Close();
                    //------------------------

                    query = "DELETE FROM tb_produto WHERE prod_id ='" + idProdutoOnly + "'";
                    command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Produto " + txtProduct.Text + " Removido com Sucesso!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtProduct.Text = string.Empty;
                    txtPrecoCompra.Text = string.Empty;
                    this.methodReadProdList();

                    btnActalizarCat.Enabled = false;
                    btnApagarCat.Enabled = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error na tentativa de Apagar Produto! ::" + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void txtPreco_TextChanged(object sender, EventArgs e)
        {
            moeda(ref txtPrecoCompra);
        }

        private void btnActualizarProd_Click(object sender, EventArgs e)
        {
            if (txtProduct.Text != string.Empty && txtPrecoCompra.Text!=string.Empty && idProdutoOnly!=0)
            {
                try
                {
                    //----------------------
                    query = null;
                    command.Parameters.Clear();
                    rd.Close();
                    //------------------------


                    query = "UPDATE tb_produto SET prod_nome='" + txtProduct.Text.ToUpper() + "', prod_preco='"+Double.Parse(txtPrecoCompra.Text)+ "' WHERE prod_id ='" + idProdutoOnly + "'";
                    command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Produto " + txtProduct.Text + " com o preço: "+txtPrecoCompra.Text+" Actualizado com Sucesso!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtProduct.Text = string.Empty;
                    txtPrecoCompra.Text= string.Empty;
                    this.methodReadProdList();

                    btnActualizarProd.Enabled = false;
                    btnApagarProduct.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: ::" + ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Error: \nInsira o nome do Produto ou Preço ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnCancelarEntrada_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        int toStockProdID = 0;
        string toStockProdNome = null;
        private void lstProdExist_MouseClick(object sender, MouseEventArgs e)
        {
            toStockProdID = int.Parse(lstProdExist.SelectedItems[0].Text);
            toStockProdNome = lstProdExist.SelectedItems[0].SubItems[1].Text;
            stkIDCat = int.Parse(lstProdExist.SelectedItems[0].SubItems[3].Text);
            stkIDSubcat = int.Parse(lstProdExist.SelectedItems[0].SubItems[4].Text);

            btnAddStock.Enabled = true;
            btnDevStock.Enabled = false;
        }

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            //
            methodReadExistProdStock();
        }

        private void lstStockExist_MouseClick(object sender, MouseEventArgs e)
        {
            //
            toStockProdID = int.Parse(lstStockExist.SelectedItems[0].Text);
            txtQuantActual.Text = lstStockExist.SelectedItems[0].SubItems[2].Text;

            btnAtribuirQuant.Enabled = true;
            btnDevStock.Enabled = true;

            //Quantidade danificadas
            gpbQuantDani.Visible = true;
            txtDaniTotal.Text = lstStockExist.SelectedItems[0].SubItems[2].Text;
            txtDaniActual.Maximum = int.Parse(lstStockExist.SelectedItems[0].SubItems[2].Text);
        }

        private void btnAtribuirQuant_Click(object sender, EventArgs e)
        {
            if (txtQuanty.Text != "")
            {
                if (toStockProdID!=0)
                {
                    //Metodo Update
                    methodUpdateQuantStock();
                }
                else
                {
                    MessageBox.Show("Erro:: Variavel já inicializada! ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Insira a Quantidade >> ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDevStock_Click(object sender, EventArgs e)
        {
            btnAddStock.Enabled = false;
            methodDeleteProdStrock();

            txtQuantActual.Text = "";
            txtQuanty.Text = "";
            btnAtribuirQuant.Enabled = false;

        }

        private void txtPrecoVenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPrecoServ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cboCatToServ_SelectedIndexChanged(object sender, EventArgs e)
        {
            methodReadIDCat();

            cboSubcatProd.Text = string.Empty;
            txtSearchProdToServ.Text = string.Empty;
            txtTypeServ.Text = string.Empty;
            txtPrecoServ.Text = string.Empty;

            lblObjctAtb.Text = "notSet";
            lblObjetTargetName.Text = "notGet";

            lstProdToServ.Items.Clear();
            lstCurrentServProd.Items.Clear();

            methodReadSubcat();
        }

        private void cboSubcatToServ_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Select ID
            if (cboCatToServ.Text == "")
            {
                MessageBox.Show("Error :: Seleccione a respectiva Categoria!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Select ID
                methodReadIDSub();

                //SELECT ALL PRODUCTS LINKED BY THIS ID-SUB
                methodReadProdExistServ();

                txtPrecoServ.Text = "";
                txtTypeServ.Text = string.Empty;
                txtSearchProdToServ.Text = string.Empty;
                

                lblObjctAtb.Text = "notSet";
                lblObjetTargetName.Text = "notGet";

                lstCurrentServProd.Items.Clear();

            }
        }
        //===================================================SEARCH============================

        public void methodSearchProdServ()
        {
            //----------------------
            query = null;
            command.Parameters.Clear();
            rd.Close();
            //------------------------

            try
            {
                lstProdToServ.Items.Clear();

                query = "SELECT prod_id, prod_nome FROM tb_produto WHERE prod_subcat_id='" + idCategoriaOnly + "' AND prod_nome LIKE '%" + txtSearchProdToServ.Text.ToUpper() + "%'";
                command = new MySqlCommand(query, connection);
                rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {
                    int prod_id = rd.GetInt32(0);
                    string prodNome = rd.GetString(1);

                    lstProdToServ.Items.Add(prod_id.ToString());
                    lstProdToServ.Items[i].SubItems.Add(prodNome);

                    i++;

                }

            }
            catch (Exception ex) { MessageBox.Show("Falha ao Pesquisar Produto  >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); throw; }


        }

        private void txtSearchProdToServ_OnValueChanged(object sender, EventArgs e)
        {
            if (txtSearchProdToServ.Text != "")
            {
                //SEARCH PRODUCT 
                methodSearchProdServ();
            }
            else
            {
                methodReadProdExistServ();
            }
        }

        private void lstProdToServ_MouseClick(object sender, MouseEventArgs e)
        {
            //
            lblObjctAtb.Text = lstProdToServ.SelectedItems[0].Text;
            lblObjetTargetName.Text = lstProdToServ.SelectedItems[0].SubItems[1].Text;

            methodReadCurrentServList();
        }

        private void txtSalvarServ_Click(object sender, EventArgs e)
        {
            if (lblObjctAtb.Text == "notSet")
            {
                MessageBox.Show("Error: \nSelecione o produto alvo... ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (txtTypeServ.Text == "")
                {
                    MessageBox.Show("Error: \nInsira o tipo on nome do serviço ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    if (txtPrecoServ.Text == "")
                    {
                        MessageBox.Show("Error: \nInsira o Preço do serviço", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        methodReadExistService();
                        //MessageBox.Show(dateTimePicker1.Value.DayOfYear +" of World");

                        methodReadServList();

                    }
                }
            }
        }

        private void txtPrecoServ_TextChanged(object sender, EventArgs e)
        {
            moeda(ref txtPrecoServ);
        }

        private void lstServsTotal_MouseClick(object sender, MouseEventArgs e)
        {
            txtTypeServ.Text = lstServsTotal.SelectedItems[0].SubItems[1].Text;
            txtPrecoServ.Text = lstServsTotal.SelectedItems[0].SubItems[3].Text;
        }

        private void btnRetirarDamaged_Click(object sender, EventArgs e)
        {
            if (txtDaniTotal.Text != "")
            {
                if (toStockProdID != 0)
                {
                    //Metodo Update
                    methodUpdateQuantStockDamaged();
                }
                else
                {
                    MessageBox.Show("Erro:: Variavel já inicializada! ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Insira a Quantidade >> ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPrecoVenda_TextChanged(object sender, EventArgs e)
        {
            moeda(ref txtPrecoVenda);
        }

        private void lstProdut_MouseClick_1(object sender, MouseEventArgs e)
        {
            //--***
            btnActualizarProd.Enabled = true;
            btnApagarProduct.Enabled = true;

            idProdutoOnly = int.Parse(lstProdut.Items[0].Text);
            cboCategoriaProd.Text = lstProdut.SelectedItems[0].SubItems[1].Text;
            cboSubcatProd.Text = lstProdut.SelectedItems[0].SubItems[2].Text;
            txtProduct.Text = lstProdut.SelectedItems[0].SubItems[3].Text;
            txtPrecoCompra.Text = lstProdut.SelectedItems[0].SubItems[4].Text;
            txtPrecoVenda.Text = lstProdut.SelectedItems[0].SubItems[5].Text;
        }

        private void txtPreco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void btnSalvarProd_Click(object sender, EventArgs e)
        {
            //Salvar Produto
            methodReadExistProduct();
            methodReadProdList();
        }

        private void cboCategoriaProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Load ID categoria
            methodReadIDCat();

            cboSubcatProd.Text = string.Empty;
            txtProduct.Text = string.Empty;
            txtPrecoCompra.Text = string.Empty;

            methodReadSubcat();

        }
    }
}
