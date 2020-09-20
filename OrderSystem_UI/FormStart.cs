using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSystem_UI
{
    public partial class FormStart : Form
    {
        public FormStart()
        {
            InitializeComponent();
        }

        private void start_Load(object sender, EventArgs e)
        {
            //Loading
            //Local Connection
            //try
            //{
            //    System.Diagnostics.Process.Start(@"C:\wamp64\wampmanager.exe");
            //}
            //catch (Exception ex) { MessageBox.Show("Error: Servidor local não inicializado " + ex.Message, "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        //--------------------------------------------------------------------------------------------

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bunifuProgressBar1.Value < 100)
            {
                bunifuProgressBar1.Value = bunifuProgressBar1.Value + 2;
            }
            else
            {
                this.Visible = false;
                this.timer1.Enabled = false;

                FormLogin ml = new FormLogin();
                ml.ShowDialog();

                this.Visible = false;
                this.timer1.Enabled = false;
            }
        }
    }
}
