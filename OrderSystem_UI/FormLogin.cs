using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//---------------------------
using System.IO;
using MySql.Data.MySqlClient;
using System.Net.NetworkInformation;

namespace OrderSystem_UI
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {

            InitializeComponent();
        }

        //=========================================================================
        private bool Drag;
        private int MouseX;
        private int MouseY;

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        private bool m_aeroEnabled;

        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]

        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW; return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0; DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        }; DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                default: break;
            }
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
        }
        private void PanelMove_MouseDown(object sender, MouseEventArgs e)
        {
            Drag = true;
            MouseX = Cursor.Position.X - this.Left;
            MouseY = Cursor.Position.Y - this.Top;
        }
        private void PanelMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                this.Top = Cursor.Position.Y - MouseY;
                this.Left = Cursor.Position.X - MouseX;
            }
        }
        private void PanelMove_MouseUp(object sender, MouseEventArgs e) { Drag = false; }
        //========================================================================
        Loader _loader = new Loader();
        alert.Formalert _alert;
        string msg1, msg2;

        string ficheiro = @"DBSystem.txt";

        private void internetConnection()
        {
            System.Uri Url = new System.Uri("http://www.google.com");
            System.Net.WebRequest WebRequest;
            WebRequest = System.Net.WebRequest.Create(Url);
            System.Net.WebResponse objResp;
            try
            {
                objResp = WebRequest.GetResponse();
                //Success Internet Detectada
            }
            catch (Exception) { WebRequest = null; 
                    if(WebRequest == null) {

                            if (MessageBox.Show("Internet não detectada, prosseguir mesmo assim ?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                            {
                                Application.Exit();
                            }
                        }
                   }

        }

        private void login_Load(object sender, EventArgs e)
        {
            //Load
            if (Directory.Exists(folder) == false)
            {
                Directory.CreateDirectory(folder);
                if (File.Exists(folder + "\\" + ficheiro) == false)
                {
                    ficheiroTextoSettingsConexao();
                }
            }
            if (Directory.Exists(folder) == true)
            {
                if (File.Exists(folder + "\\" + ficheiro) == true)
                {
                    carregarOffice();
                }
            }
            internetConnection();

        }

        public MySqlConnection methodConnect()
        {
            //string connString = "Server=App1.seudominio.co.mz;Port=3306;Database=appseudo_db_sidedesk;Uid=appseudo_jmac;Password=side*2019;";
            //string connString = "Server=abuild.mysql.database.azure.com;Port=3306;Database=abuild_SIDE;Uid=j7@abuild;Password=GloriaDeus19;";
            //string connString = "Server=localhost;Port=3306;Database=db_sidedesk;Uid=jmac;Password=12345root;";
            ficheiroTextoSettingsConexao();
            ficheiroTextoConexao();
            MySqlConnection conn = null;
            try
            {

                //conn = new MySqlConnection(@"Server="+nameServer.ToString()+ ";Port="+int.Parse(portnumber.ToString())+ ";Database="+nameDataBase+ ";Uid="+nameUser.ToString()+ ";Password="+passwordUser.ToString()+";");
                conn = new MySqlConnection(@"Server=abuild.mysql.database.azure.com;Port=3306;Database=db_sidedesk;Uid=j7@abuild;Password=GloriaDeus19;");
                //conn = new MySqlConnection(@"Server=database-1test.cu0qdjqhr0rz.us-east-2.rds.amazonaws.com;Port=3306;Database=db_sidedesk;Uid=admin;Password=12345root;");

                conn.Open();
                return conn;
            //Application.Restart();

            }catch (Exception) { 
                msg2 = "Falha ao estabelecer ligação com a Base de dados.";
                msg1 = "Error de conexão";
                _alert = new alert.Formalert("error", msg1, msg2);
                _alert.ShowInTaskbar = false;
                _alert.ShowDialog();
            }
            return conn;
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Sair da aplicação", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        string folder = "Config";


        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (cboOffice.Text == "")
            {

                msg2 = "Selecione o seu Escritório!.";
                msg1 = "Erro de leitura";
                _alert = new alert.Formalert("error", msg1, msg2);
                _alert.ShowInTaskbar = false;
                _alert.ShowDialog();

                cboOffice.Focus();
            } else
            {
               

                carregarOfficeID();

                _loader = new Loader();
                _loader.Show();

                //_loader.ShowInTaskbar = false;
               
                metodoLerUsuario();
                
            }
            

            
        }

        int idOff;
        string empresaOff, escritoriOff, escritoriOffSelected, cidadeOff, contactOff, emailOff, nuitOff, logOff, dtOff;
        //====================================
        MySqlCommand comando = null;
        MySqlDataReader ler = null;

        public MySqlConnection connection = null;
        public void carregarOffice()
        {
            connection = methodConnect();
            cboOffice.Items.Clear();
            try
            {

                comando = new MySqlCommand("SELECT off_escritorio FROM tb_office", connection);
                ler = comando.ExecuteReader();
                while (ler.Read())
                {
                    escritoriOffSelected = ler.GetString(0);

                    cboOffice.Items.Add(escritoriOffSelected);
                }

            }
            catch (Exception ex) {
                
                msg2 = "Não foi possivel carregar dados de Empresa!.";
                msg1 = "Carregamento não concebido";
                _alert = new alert.Formalert("error", msg1, msg2);
                _alert.ShowInTaskbar = false;
                _alert.ShowDialog();
                this._loader.Close(); }
        }

        //==================================

        public void carregarOfficeID()
        {
            
            comando.Parameters.Clear();
            ler.Close();

            try
            {
                
                comando = new MySqlCommand("SELECT * FROM tb_office WHERE off_escritorio='"+ cboOffice.Text +"'", connection);
                ler = comando.ExecuteReader();
                while (ler.Read())
                {
                    idOff = ler.GetInt32(0);
                    empresaOff = ler.GetString(1);
                    escritoriOff = ler.GetString(2);
                    cidadeOff = ler.GetString(3);
                    contactOff = ler.GetString(4);
                    emailOff = ler.GetString(5);
                    nuitOff = ler.GetString(6);
                    logOff = ler.GetString(7);
                    dtOff = ler.GetString(8);
                }

            }
            catch (Exception ex) {

                msg2 = "Não foi possivel carregar dados de Empresa";
                msg1 = "Carregamento não concebido";
                _alert = new alert.Formalert("error", msg1, msg2);
                _alert.ShowInTaskbar = false;
                _alert.ShowDialog();
                
            }
        }

        //===========================================================================

        //Metodo desencriptar
        string senhaDesencriptada;
        string desencriptar(string entrada)
        {
            string result = string.Empty;
            try
            {

                Byte[] desencriptar = Convert.FromBase64String(entrada);

                result = System.Text.Encoding.Unicode.GetString(desencriptar);

            }
            catch (Exception ex)
            {

            }

            return result;
        }
        string nameServer, nameDataBase, nameUser, passwordUser;

        String DBConn;

        string DBComm, DBDreader, DBDadapter, DBCommBuilder;
        int portnumber;

        public void ficheiroTextoSettingsConexao()

        {
            StreamReader sr;
            string ficheiro = @"DBSystem.txt";
            if (File.Exists(folder + "\\" + ficheiro) == true)
            {
                sr = File.OpenText(folder + "\\" + ficheiro);
                string linha = "";
                while ((linha = sr.ReadLine()) != null)
                {
                    string[] campos = new string[5];
                    campos = linha.Split(';');

                    DBConn = campos[0];
                    DBComm = campos[1];
                    DBDreader = campos[2];
                    DBDadapter = campos[3];
                    DBCommBuilder = campos[4];

                }
                sr.Close();
            }
            if (File.Exists(folder + "\\" + ficheiro) == false)
            {

                msg2 = "Contact your Administrator";
                msg1 = "Connection model not set!";
                _alert = new alert.Formalert("warning", msg1, msg2);
                _alert.ShowInTaskbar = false;
                _alert.ShowDialog();

                FormConnection con = new FormConnection(0);
                con.ShowDialog();
            }
        }

        private void cboOffice_SelectedIndexChanged(object sender, EventArgs e)
        {
            carregarOfficeID();
        }

        //==========================LoadFile
        public void ficheiroTextoConexao()
        {
            StreamReader sr;
            string ficheiro = @"connection.txt";
            if (File.Exists(folder + "\\" + ficheiro) == true)
            {
                sr = File.OpenText(folder + "\\" + ficheiro);
                string linha = "";
                while ((linha = sr.ReadLine()) != null)
                {
                    string[] campos = new string[5];
                    campos = linha.Split(';');

                    nameServer = campos[0];
                    portnumber = int.Parse(campos[1].ToString());
                    nameDataBase = campos[2];
                    nameUser = campos[3];
                    passwordUser = desencriptar(campos[4]);

                }
                sr.Close();
            }
            if (File.Exists(folder + "\\" + ficheiro) == false)
            {
                
                msg2 = "Ficheiro de conexao não encontrado";
                msg1 = "Error de conexão!";
                _alert = new alert.Formalert("error", msg1, msg2);
                _alert.ShowInTaskbar = false;
                _alert.ShowDialog();

                FormConnection con = new FormConnection(1);
                con.ShowDialog();
            }
        }
        //============================
        string[] separaNome;
        string name, nickname, email, office, pass, previlege, createDate, status;

        private void lblNewAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormNewAccount nt = new FormNewAccount();
            nt.ShowDialog();
        }

        int id, accessLevel;
        string nome, apelido;
        //============================
        public void metodoLerUsuario()
        {

            if (txtUsername.Text != "")
            {
                if (txtPassword.Text != "")
                {
                    try
                    {
                        comando.Parameters.Clear();
                        ler.Close();

                        separaNome = txtUsername.Text.ToUpper().Split('.');
                        nome = separaNome[0].ToString();
                        apelido = separaNome[1].ToString();

                        comando = new MySqlCommand("SELECT user_id, user_name, user_nickname, user_email, user_office, user_password, user_privilege, user_AccessLevel, user_createDate, user_status FROM tb_user WHERE user_name='" + nome.ToUpper() + "' AND user_nickname='" + apelido.ToUpper() + "'", connection);
                        ler = comando.ExecuteReader();
                        while (ler.Read())
                        {
                            id = ler.GetInt32(0);
                            name = ler.GetString(1);
                            nickname = ler.GetString(2);
                            email = ler.GetString(3);
                            office = ler.GetString(4);
                            pass = ler.GetString(5);
                            previlege = ler.GetString(6);
                            accessLevel = ler.GetInt32(7);
                            createDate = ler.GetString(8);
                            status = ler.GetString(9);

                        }
                        //Comparando Senhas e registando Login efectuado
                        this.metodoComparadorSenha();

                    }
                    catch (Exception)
                    {

                        msg2 = "Dados não validos";
                        msg1 = "Carregamento não concebido!";
                        _alert = new alert.Formalert("error", msg1, msg2);
                        _alert.ShowInTaskbar = false;
                        _alert.ShowDialog();

                        _loader.Close();
                    }
                }
                else
                {
                    msg2 = "Insira a password! ";
                    msg1 = "Erro de password!";
                    _alert = new alert.Formalert("error", msg1, msg2);
                    _alert.ShowInTaskbar = false;
                    _alert.ShowDialog();
                }

            }
            else { 
                msg2 = "Insira o username ou email!";
                msg1 = "Erro de dados!";
                _alert = new alert.Formalert("error", msg1, msg2);
                _alert.ShowInTaskbar = false;
                _alert.ShowDialog();
            }

        }
        //==============================
        int i = 3;
        string situacao;
        public void metodoComparadorSenha()
        {
            senhaDesencriptada = desencriptar(pass);
            if (status.ToString() == "active")
            {
                if (nome.ToUpper() == name.ToString() && apelido.ToUpper() == nickname.ToString())
                {

                    if (txtPassword.Text == senhaDesencriptada)
                    {
                        if (cboOffice.Text == office)
                        {
                            //Registar login
                            this.metodoRegistarLogin();
                            this.metodoLerIdControlador();

                            this._loader.Close();
                            this.Visible = false;
                            this.Dispose();


                            FormMain m = new FormMain(DBConn, DBComm, DBDreader, DBDadapter, DBCommBuilder, idOff, id, name, nickname, email, previlege, accessLevel, idControl, nameDataBase);
                            m.ShowDialog();


                        }
                        else
                        {

                            msg2 = "Usuário não registado neste escritório!";
                            msg1 = "Dados não acessíveis";
                            _alert = new alert.Formalert("warning", msg1, msg2);
                            _alert.ShowInTaskbar = false;
                            _alert.ShowDialog();

                            this._loader.Close();
                        }

                    }
                    else
                    {
                        i--;

                        if (i > 0) { this.metodoLoginAttempt(); situacao = "Tem apenas " + i.ToString() + " tentativa/s"; }
                        if (i <= 0) { this.blockingUser(); situacao = "Conta Suspendida, Contacte o Administrador."; i = 3; }

                        msg2 = "Password incorrecta! " + situacao.ToString();
                        msg1 = "Mesangem";
                        _alert = new alert.Formalert("error", msg1, msg2);
                        _alert.ShowInTaskbar = false;
                        _alert.ShowDialog();

                        this._loader.Dispose();
                    }
                }
                else
                {
                    msg2 = "Dados não validos! ";
                    msg1 = "Mesangem";
                    _alert = new alert.Formalert("error", msg1, msg2);
                    _alert.ShowInTaskbar = false;
                    _alert.ShowDialog();

                    this._loader.Close();
                }
            }
            else { 
                msg2 = "Usuario não permitido no Sistema";
                msg1 = "Contacte o Administrador";
                _alert = new alert.Formalert("error", msg1, msg2);
                _alert.ShowInTaskbar = false;
                _alert.ShowDialog();

                this._loader.Close();
            }


        }
        //----------------------------------------------------------------
        public void metodoRegistarLogin()
        {
            try
            {
                comando.Parameters.Clear();
                ler.Close();
                string estadoInicial = "online", estadoFinal = "nulo";
                comando = new MySqlCommand("INSERT INTO tb_user_login(login_user_id, login_user_name, login_initial_state, login_final_state, login_date) VALUES(" + id.ToString() + ",'" + txtUsername.Text + "','" + estadoInicial.ToUpper() + "/" + hora.Value.ToLocalTime() + "','" + estadoFinal + "','" + data.Value.ToLongDateString() + "')", connection);
                comando.ExecuteNonQuery();

                
            }
            catch (Exception ex) {
               
                msg2 = "Falhar ao efectuar o registo";
                msg1 = "Mesangem";
                _alert = new alert.Formalert("error", msg1, msg2);
                _alert.ShowInTaskbar = false;
                _alert.ShowDialog();

                this._loader.Dispose(); }
        }
        //===================================
        int idControl;
        public void metodoLerIdControlador()
        {
            comando.Parameters.Clear();
            ler.Close();
            try
            {
                comando = new MySqlCommand("SELECT login_id FROM tb_user_login WHERE login_user_id=" + id.ToString() + "", connection);
                ler = comando.ExecuteReader();
                int i = 0;
                while (ler.Read())
                {
                    idControl = ler.GetInt32(0);
                    i++;
                }
            }
            catch (Exception ex) {

                msg2 = "Falhar ao carregar ID";
                msg1 = "Erro de leitura";
                _alert = new alert.Formalert("error", msg1, msg2);
                _alert.ShowInTaskbar = false;
                _alert.ShowDialog();

                this._loader.Dispose();
            }
        }

        //------------------------------------
        //Metodo Registar tentativas de login
        public void metodoLoginAttempt()
        {
            try
            {
                comando.Parameters.Clear();
                ler.Close();

                comando = new MySqlCommand("INSERT INTO tb_login_attempt(attempt_name_used, attempt_password_used, attempt_date) VALUES('" + txtUsername.Text + "','" + txtPassword.Text + "','" + data.Value.ToLongDateString() + "')", connection);
                comando.ExecuteNonQuery();
            }
            catch (Exception ex) {

                msg2 = "Falhar ao efectuar o registo";
                msg1 = "Erro de gravação";
                _alert = new alert.Formalert("error", msg1, msg2);
                _alert.ShowInTaskbar = false;
                _alert.ShowDialog();

                this._loader.Dispose(); }
        }
        //----------------=--===----------------------
        public void blockingUser()
        {
            comando.Parameters.Clear();
            ler.Close();
            string status = "inactive";
            try
            {
                comando = new MySqlCommand("UPDATE tb_user SET user_status='" + status + "' WHERE user_id='" + id.ToString() + "'", connection);
                comando.ExecuteNonQuery();

                msg2 = "Usuario bloqueiado";
                msg1 = "Invasão detectada!";
                _alert = new alert.Formalert("info", msg1, msg2);
                _alert.ShowInTaskbar = false;
                _alert.ShowDialog();
          
            }
            catch (Exception ex) { MessageBox.Show("Falha no processo de bloqueio!" + ex.Message);
                msg2 = "Falha no efectuar bloqueio!";
                msg1 = "Erro de processameto";
                _alert = new alert.Formalert("error", msg1, msg2);
                _alert.ShowInTaskbar = false;
                _alert.ShowDialog();

                this._loader.Dispose(); }

        }
    }
}
