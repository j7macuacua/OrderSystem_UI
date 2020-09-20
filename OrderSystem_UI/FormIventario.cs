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
    public partial class FormIventario : Form
    {
        int idOff, idUser, user_accessLevel;
        string _DBConn, _DBComm, _DBDreader, _DBDadapter, _DBCommBuilder;
        public FormIventario(string DBConn, string DBComm, string DBDreader, string DBDadapter, string DBCommBuilder, int idOff, int id, int user_accessLevel)
        {
            this._DBConn = DBConn;
            this._DBComm = DBComm;
            this._DBDreader = DBDreader;
            this._DBDadapter = DBDadapter;
            this._DBCommBuilder = DBCommBuilder;

            this.idOff = idOff;
            this.idUser = id;
            this.user_accessLevel = user_accessLevel;
            InitializeComponent();
        }

        private void iventarioForm_Load(object sender, EventArgs e)
        {
            //Load
            methodListTo();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {

            FormtabProduto tbp = new FormtabProduto(0, idOff, idUser);
            tbp.ShowDialog();
        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {

            FormtabProduto tbp = new FormtabProduto(1, idOff, idUser);
            tbp.ShowDialog();
        }

        private void btnSubcategoria_Click(object sender, EventArgs e)
        {

            FormtabProduto tbp = new FormtabProduto(2, idOff, idUser);
            tbp.ShowDialog();
        }
        int j = 1;

        private void txtSearch_OnValueChanged(object sender, EventArgs e)
        {
            MessageBox.Show(j++.ToString());
        }

        private void btnEntrada_Click(object sender, EventArgs e)
        {
            FormtabProduto tbp = new FormtabProduto(3, idOff, idUser);
            tbp.ShowDialog();
        }

        private void btnAjuste_Click(object sender, EventArgs e)
        {
            FormtabProduto tbp = new FormtabProduto(4, idOff, idUser);
            tbp.ShowDialog();
        }

        //-----------------------------------------------------------------------------------------------------------------------------

        public void methodListTo()
        {

            FormLogin conn = new FormLogin();
            MySqlConnection connection = conn.methodConnect();

            app.ConfigDB cfg = new app.ConfigDB();
           // _DBComm = cfg.methodComm();

            try
            {
                string query = "SELECT stk_prod_id, cat_nome, subcat_nome, stk_prod_nome, stk_qtd_disponivel, prod_precoCompra, prod_precoVenda FROM tb_stock INNER JOIN tb_produto ON stk_prod_id = prod_id INNER JOIN tb_categoria ON cat_id = stk_cat_id INNER JOIN tb_subcategoria ON subcat_id = stk_subcat_id ";
                
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader rd = command.ExecuteReader();
                int i = 0;
                while (rd.Read())
                {
                    
                    int id = rd.GetInt32(0);
                    lstIventario.Items.Add(id.ToString());
   
                    lstIventario.Items[i].SubItems.Add(rd.GetString(1));
                    lstIventario.Items[i].SubItems.Add(rd.GetString(2));
                    lstIventario.Items[i].SubItems.Add(rd.GetString(3));

                    int qt = rd.GetInt32(4);
                    lstIventario.Items[i].SubItems.Add(qt.ToString());

                    double prc = rd.GetDouble(5);
                    double prv = rd.GetDouble(6);
                    lstIventario.Items[i].SubItems.Add(prc.ToString("N2"));
                    lstIventario.Items[i].SubItems.Add(prv.ToString("N2"));

                    double tt = double.Parse(qt.ToString()) * prv;
                    lstIventario.Items[i].SubItems.Add(tt.ToString("N2"));
                    i++;
                }
                Cursor = Cursors.Default;

            }
            catch (Exception ex) { MessageBox.Show("Falha ao Dados Iventario:: " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); throw; }
        }
    }
}
