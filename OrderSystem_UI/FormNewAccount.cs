using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//--------------------------------
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
//--------------------------------



namespace OrderSystem_UI
{
    public partial class FormNewAccount : Form
    {
        public FormNewAccount()
        {
            InitializeComponent();
        }
        Loader _loader = new Loader();

        string varData = DateTime.Today.Date.Day + "/" + DateTime.Today.Date.Month + "/" + DateTime.Today.Date.Year;
        string mailTo = "joazinho.macuacua@gmail.com",
            mailSub="New Account[SIDE] autentication Code",
        mailMsg;
        string senha = "nula.nula", macADDRESS;

        private void FormNewAccount_Load(object sender, EventArgs e)
        {
            //Load
            cboOffice.Focus();
            gbConfirm.Visible = false;

            carregarOffice();

            
        }
        int idOff;
        string empresaOff, escritoriOff, cidadeOff, contactOff, emailOff, nuitOff, logOff, dtOff;

        //================================================================
        public void carregarOffice()
        {
            FormLogin conn = new FormLogin();
            MySqlConnection connection = conn.methodConnect();
            cboOffice.Items.Clear();
            try
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM tb_office", connection);
                MySqlDataReader ler = comando.ExecuteReader();
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

                    cboOffice.Items.Add(escritoriOff);
                }

            }
            catch (Exception ex) { MessageBox.Show("Não foi possivel carregar dados de Empresa!\nCarregamento não concebido.\n\n " + ex.Message); }
        }
        //================================================================

        private void butClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Cancelar Registo?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Dispose();
            }
            
        }
        Random ran = new Random();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            senha = ran.Next(100, 999).ToString() + "-" + ran.Next(100, 999).ToString();


            if (cboOffice.Text == "")
            {
                MessageBox.Show("Por favor selecione o Escritório!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboOffice.Focus();
            }
            else
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Error: Preencha o Nome. ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtName.Focus();
                }
                else
                {
                    if (txtApelido.Text == "")
                    {
                        MessageBox.Show("Error: Preencha o Apelido. ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtApelido.Focus();
                    }
                    else
                    {
                        if(txtPassword1.Text == "")
                        {
                            MessageBox.Show("Error: Preencha a Senha. ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtPassword1.Focus();
                        }
                        else
                        {
                            if (txtPassword2.Text=="")
                            {
                                MessageBox.Show("Error: Confirme a Senha antes...", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                txtPassword2.Focus();
                            }
                            else
                            {
                                if(txtPassword1.Text.Length >= 8 || txtPassword2.Text.Length >= 8)
                                {
                                    if(txtPassword1.Text == txtPassword2.Text)
                                    {
                                        if (txtEmail.Text != "")
                                        {
                                            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                                            if (Regex.IsMatch(txtEmail.Text, pattern))
                                            {
                                                errorProvider1.Clear();
                                                if (txtTelemovel.Text != "")
                                                {
                                                    if(txtTelemovel.Text.Length == 9)
                                                    {
                                                        this._loader.Show();

                                                        string result = string.Empty;
                                                        System.Uri Url = new System.Uri("http://www.google.com");
                                                        System.Net.WebRequest WebRequest;
                                                        WebRequest = System.Net.WebRequest.Create(Url);
                                                        System.Net.WebResponse objResp;
                                                        try
                                                        {
                                                            objResp = WebRequest.GetResponse();
                                                            //Success Internet Detectada

                                                            try
                                                            {
                                                                getMAC();

                                                                mailMsg = "<h3>CONFIRMATION[Abuild]</h3><h4>Administração[LRJM]</h4><div style='background-color:#53A93F; width:500px; height:2px'></div>Foi solicitada a criação de uma nova Conta Funcionário<br>Conta referente SIDEDESK Sistema de Gestão de Incomendas<br>Codigo de Confirmação: <strong style = 'color:#293955; font-size:12pt'>" + senha.ToUpper() + "</strong> válido até a exclusão da janela referente.<br><strong>Office: </strong>" + cboOffice.Text + "<br><strong style='color:#E8756D'>Nome: </strong>" + txtName.Text + " " + txtApelido.Text + "<br><strong style='color:#E8756D'>Email: </strong>" + txtEmail.Text + "<br> <strong style='color:#E8756D'>Telemóvel: </strong>" + txtTelemovel.Text + "<br> <strong>Data: </strong>" + DateTime.Today.Date.Day + "/" + DateTime.Today.Date.Month + "/" + DateTime.Today.Date.Year + "<br><br>Endereço MAC: " + macADDRESS;

                                                                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();
                                                                mmsg.To.Add(mailTo+","+txtEmail.Text);
                                                                mmsg.Subject = mailSub;
                                                                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;
                                                                mmsg.Bcc.Add("jmacuacua@mahs.co.mz");

                                                                mmsg.Body = mailMsg;
                                                                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                                                                mmsg.IsBodyHtml = true;
                                                                mmsg.From = new System.Net.Mail.MailAddress("j7macuacua@gmail.com");

                                                                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                                                cliente.Credentials = new System.Net.NetworkCredential("j7macuacua@gmail.com", "Aloverlu130");
                                                                cliente.Port = 587;
                                                                cliente.EnableSsl = true;
                                                                cliente.Host = "smtp.gmail.com"; //mail.dominio.com

                                                                cliente.Send(mmsg);

                                                                this._loader.Close();
                                                                gbConfirm.Visible = true;
                                                                btnLogin.Enabled = false;
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                MessageBox.Show("Falha no envio de email: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                        }
                                                        catch (Exception exc)
                                                        {
                                                            MessageBox.Show("Error: \nInternet não disponivel ","Erro de Internet",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                                            WebRequest = null;
                                                            
                                                        }
                                                        
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Error: Número do telefone não válido!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                        txtTelemovel.Focus();
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Error: Insira o número do telefone associado a esta conta!","Alerta",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                                                    txtTelemovel.Focus();
                                                }

                                            }
                                            else
                                            {
                                                errorProvider1.SetError(this.txtEmail,"Insira um email válido, por favor");
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Error: Insira o endereço de Email!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                            txtEmail.Focus();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error: As senhas não coincidem!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                        txtPassword1.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Error: o numero de caracteres de ser maior ou igual a oito(8)","Alerta", MessageBoxButtons.OK,MessageBoxIcon.Stop);
                                    txtPassword1.Focus();
                                }
                            }
                        }
                    }
                }
            }

            
        }

        private void txtConfirmCod_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTelemovel_KeyPress(object sender, KeyPressEventArgs e)
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
        //---------------------------------------------------
        string senhaEncriptada;
        string encriptar(string entrada)
        {
            string result = string.Empty;
            Byte[] encriptar = System.Text.Encoding.Unicode.GetBytes(entrada);
            result = Convert.ToBase64String(encriptar);

            return result;
        }
        //--------------------------------------------------------------------------------------------------------------------------------
        public void getMAC()
        {

            string mac = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {

                if (nic.OperationalStatus == OperationalStatus.Up && (!nic.Description.Contains("Virtual") && !nic.Description.Contains("Pseudo")))
                {
                    if (nic.GetPhysicalAddress().ToString() != "")
                    {
                        mac = nic.GetPhysicalAddress().ToString();
                    }
                }
            }
            string h1 = mac.Substring(0, 2),
                h2 = mac.Substring(2, 2),
                h3 = mac.Substring(4, 2),
                h4 = mac.Substring(6, 2),
                h5 = mac.Substring(8, 2),
                h6 = mac.Substring(10, 2);

            macADDRESS = h1 + "-" + h2 + "-" + h3 + "-" + h4 + "-" + h5 + "-" + h6;
        }

        //============================================================

        string privilege = "client";
        string accountStatus = "active";
        int accessLevel = 3;
        string resultUser;

        public void methodReadExistProduct()
        {

            FormLogin conn = new FormLogin();
            MySqlConnection connection = conn.methodConnect();
            if (cboOffice.Text == String.Empty || txtName.Text == String.Empty || txtApelido.Text == String.Empty || txtPassword1.Text == String.Empty || txtPassword2.Text == String.Empty || txtEmail.Text == String.Empty || txtTelemovel.Text == String.Empty)
            {
                MessageBox.Show("Operação não valida ::\nPreecha o espaço em branco", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                try
                {
                    string query = "SELECT user_name FROM tb_user WHERE user_name='" + txtName.Text.ToUpper() + "' AND user_nickname='" + txtApelido.Text.ToUpper() + "' OR user_email='" + txtEmail.Text + "'";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader rd = command.ExecuteReader();

                    while (rd.Read())
                    {
                        resultUser = rd.GetString(0);
                    }

                    if (resultUser == null)
                    {
                        try
                        {
                            //metodo encriptarSenha
                            senhaEncriptada = encriptar(this.txtPassword1.Text);

                            FormLogin connn = new FormLogin();
                            MySqlConnection connectionn = connn.methodConnect();
                            string queryy = "INSERT INTO tb_user(user_name, user_nickname, user_email, user_office, user_contacto, user_password, user_privilege, user_AccessLevel, user_createDate, user_status) VALUES('" + txtName.Text.ToUpper() + "','" + txtApelido.Text.ToUpper() + "','" + txtEmail.Text + "','" +cboOffice.Text + "','" + txtTelemovel.Text + "','" + senhaEncriptada + "','"+ privilege +"','"+ accessLevel.ToString() + "','"+ varData + "','"+ accountStatus + "')";
                            MySqlCommand commando = new MySqlCommand(queryy, connectionn);
                            commando.ExecuteNonQuery();
                            MessageBox.Show("Usuário " + txtName.Text + " " + txtApelido.Text + " registado com Sucesso!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (MessageBox.Show("Deseja terminar registo?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                Dispose();
                            }
                            txtName.Text = string.Empty;
                            txtApelido.Text = string.Empty;
                            txtPassword1.Text = string.Empty;
                            txtPassword2.Text = string.Empty;
                            txtEmail.Text = string.Empty;
                            txtTelemovel.Text = string.Empty;

                            gbConfirm.Visible = false;
                            btnLogin.Enabled = true;
                        }
                        catch (Exception ex) { MessageBox.Show("Falha no registo do Produto!  >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); throw; }

                    }
                    else
                    {
                        MessageBox.Show("Produto existente!  >> ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        resultUser = null;
                    }

                }
                catch (Exception ex) { MessageBox.Show("Falha ao carregar registo de Subcategorias >> " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); throw; }

            }

        }

        //============================================================

        private void button1_Click(object sender, EventArgs e)
        {
            txtConfirmCod.MaxLength = 7;

            if(txtConfirmCod.Text != "")
            {
                
                if (senha == txtConfirmCod.Text)
                {
                    this._loader.Show();
                    methodReadExistProduct();
                    this._loader.Close();
                }
                else
                {
                    MessageBox.Show("Error: Código não valido!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPassword1.Focus();
                }
                
            }
        }
        //=================================================================

    }
}
