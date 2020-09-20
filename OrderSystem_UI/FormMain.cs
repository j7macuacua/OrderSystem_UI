using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//-----------------------------
using MySql.Data.MySqlClient;
using System.IO;

namespace OrderSystem_UI
{
    public partial class FormMain : Form
    {
        string _DBConn, _DBComm, _DBDreader, _DBDadapter, _DBCommBuilder, nameDatabase;

        int idOff, user_id, login_idControler, user_accessLevel;
        string user_name, user_nickname, user_email, user_previlege;

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
        public FormMain(String DBConn, string DBComm, string DBDreader, string DBDadapter, string DBCommBuilder, int idOff, int id, string name, string user_nickname, string email, string previlege, int accessLevel, int idControler, string nameDatabase)
        {
            this._DBConn = DBConn;
            this._DBComm = DBComm;
            this._DBDreader = DBDreader;
            this._DBDadapter = DBDadapter;
            this._DBCommBuilder = DBCommBuilder;


            this.idOff = idOff;
            this.user_id = id;
            this.user_name = name;
            this.user_nickname = user_nickname;
            this.user_email = email;
            this.user_previlege = previlege;
            this.user_accessLevel = accessLevel;
            this.login_idControler = idControler;


            this.nameDatabase = nameDatabase;

            InitializeComponent();
        }

        //public FormMain(string dBConn, string dBComm, string dBDreader, string dBDadapter, string dBCommBuilder, int idOff, int id, string name, string nickname, string email, string previlege, int accessLevel, int idControl, string nameDataBase)
        //{
        //    this.dBConn = dBConn;
        //    this.dBComm = dBComm;
        //    this.dBDreader = dBDreader;
        //    this.dBDadapter = dBDadapter;
        //    this.dBCommBuilder = dBCommBuilder;

        //    this.idOff = idOff;
        //    this.id = id;

        //    Name = name;
        //    this.nickname = nickname;
        //    this.email = email;
        //    this.previlege = previlege;
        //    this.accessLevel = accessLevel;
        //    this.idControl = idControl;
        //    nameDatabase = nameDataBase;
        //}

        string mailCComp, mailYear;
        string folder = "Config";
        //private string dBConn;
        //private string dBComm;
        //private string dBDreader;
        //private string dBDadapter;
        //private string dBCommBuilder;
        //private int id;
        //private string nickname;
        //private string email;
        //private string previlege;
        //private int accessLevel;
        //private int idControl;

        //-----------------------------------
        public void corporation()
        {
            StreamReader sr;
            string ficheiro = @"License.txt";
            if (File.Exists(folder + "\\" + ficheiro) == true)
            {
                sr = File.OpenText(folder + "\\" + ficheiro);
                string linha = "";
                while ((linha = sr.ReadLine()) != null)
                {
                    string[] campos = new string[2];
                    campos = linha.Split(';');

                    mailCComp = campos[0];
                    mailYear = campos[1];

                }
                sr.Close();
            }
            if (File.Exists(folder + "\\" + ficheiro) == false)
            {
                MessageBox.Show("Error :: License :: \nSistema não Licenciado!", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        //--------------------------------------------------------------------------------------------
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
                conectionToolStripMenuItem.Text = " Connected";
            }
            catch(Exception) { WebRequest = null; conectionToolStripMenuItem.Text = " Disconnected"; }
            
        }
        //-----------------------------------------------------------------------------------------------
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
        //---------------------------------------------------------------------------------------------

        //------------------------------------------------------
        private void mainForm_Load(object sender, EventArgs e)
        {
            //Load
            lblTime.Text = DateTime.Now.ToLongTimeString();
            this.WindowState = FormWindowState.Maximized;

            _loader.Show();
            internetConnection();
            databaseToolStripMenuItem.Text = " " + nameDatabase;
            corporation();
            this._loader.Close();

            string year = DateTime.Now.Year.ToString();
            string yearDesc = desencriptar(mailYear);
            if (year != yearDesc)
            {
                MessageBox.Show("Error :: License :: \nLicenciado expirado\n\nContacte o Administrador!", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                licençaToolStripMenuItem.Text = "Licença: " + desencriptar(mailCComp) + " " + year;
            }

            openForm(new FormDashboard());

        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (panelMenuText.Width == 236)
            {
                panelMenuText.Width = 10;
            }
            else
            {
                panelMenuText.Width = 236;
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo Sair do Sistema ?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnMax_Click_1(object sender, EventArgs e)
        {
            
            if (WindowState.Equals(FormWindowState.Maximized))
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }


        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void openForm(object enc)
        {
            if (this.pnlMenu.Controls.Count > 0)
                this.pnlMenu.Controls.RemoveAt(0);
            Form fh = enc as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.pnlMenu.Controls.Add(fh);
            this.pnlMenu.Tag = fh;
            fh.Show();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dateTimePicker1.Value.Date.ToString());
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            
            openForm( new FormDashboard());
        }

        private void btnIconDashboard_Click(object sender, EventArgs e)
        {
            
            openForm(new FormDashboard());
        }

        private void btnIconEnc_Click(object sender, EventArgs e)
        {
            openForm(new FormEncomendas(_DBConn, _DBComm, _DBDreader, _DBDadapter, _DBCommBuilder, idOff, user_id, user_name, user_nickname));
        }

        private void btnIconIventario_Click(object sender, EventArgs e)
        {
            openForm(new FormIventario(_DBConn, _DBComm, _DBDreader, _DBDadapter, _DBCommBuilder, idOff, user_id, user_accessLevel));
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            //
            openForm(new FormIventario(_DBConn, _DBComm, _DBDreader, _DBDadapter, _DBCommBuilder, idOff, user_id, user_accessLevel));
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo Sair do Sistema ?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }   
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void btnEncomends_Click(object sender, EventArgs e)
        {
            openForm(new FormEncomendas(_DBConn, _DBComm, _DBDreader, _DBDadapter, _DBCommBuilder, idOff, user_id, user_name, user_nickname));
        }
        //-----------------------------------------------------


        ////-------------------------------------------SHADOW
        //private const int CS_DropShadown = 0x00020000;
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ClassStyle |= CS_DropShadown;
        //        return cp;
        //    }
        //}
    }
}
