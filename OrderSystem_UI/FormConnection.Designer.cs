namespace OrderSystem_UI
{
    partial class FormConnection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConnection));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rdMySQL = new System.Windows.Forms.RadioButton();
            this.rdSQLServer = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.bunifuSeparator1 = new Bunifu.Framework.UI.BunifuSeparator();
            this.bunifuSeparator3 = new Bunifu.Framework.UI.BunifuSeparator();
            this.bunifuSeparator2 = new Bunifu.Framework.UI.BunifuSeparator();
            this.btnSaveTypeConn = new System.Windows.Forms.Button();
            this.cboIntMod = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblLAM = new System.Windows.Forms.Label();
            this.butCon = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtPassword = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.txtUser = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.txtDB = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.txtPort = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.txtServer = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblControleAcesso = new System.Windows.Forms.Label();
            this.lblDataConnection = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.CircularPercGrande = new Bunifu.Framework.UI.BunifuCircleProgressbar();
            this.butClose = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butCon)).BeginInit();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butClose)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panelHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(447, 394);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 105);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(447, 286);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.rdMySQL);
            this.tabPage1.Controls.Add(this.rdSQLServer);
            this.tabPage1.Controls.Add(this.panel4);
            this.tabPage1.Controls.Add(this.bunifuSeparator1);
            this.tabPage1.Controls.Add(this.bunifuSeparator3);
            this.tabPage1.Controls.Add(this.bunifuSeparator2);
            this.tabPage1.Controls.Add(this.btnSaveTypeConn);
            this.tabPage1.Controls.Add(this.cboIntMod);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(439, 260);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Integration";
            // 
            // rdMySQL
            // 
            this.rdMySQL.AutoSize = true;
            this.rdMySQL.Font = new System.Drawing.Font("Microsoft PhagsPa", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdMySQL.Location = new System.Drawing.Point(37, 123);
            this.rdMySQL.Name = "rdMySQL";
            this.rdMySQL.Size = new System.Drawing.Size(79, 25);
            this.rdMySQL.TabIndex = 37;
            this.rdMySQL.TabStop = true;
            this.rdMySQL.Text = "MySQL";
            this.rdMySQL.UseVisualStyleBackColor = true;
            // 
            // rdSQLServer
            // 
            this.rdSQLServer.AutoSize = true;
            this.rdSQLServer.Font = new System.Drawing.Font("Microsoft PhagsPa", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSQLServer.Location = new System.Drawing.Point(37, 101);
            this.rdSQLServer.Name = "rdSQLServer";
            this.rdSQLServer.Size = new System.Drawing.Size(106, 25);
            this.rdSQLServer.TabIndex = 36;
            this.rdSQLServer.TabStop = true;
            this.rdSQLServer.Text = "SQL Server";
            this.rdSQLServer.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Gold;
            this.panel4.Location = new System.Drawing.Point(16, 96);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(15, 58);
            this.panel4.TabIndex = 35;
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.bunifuSeparator1.LineThickness = 1;
            this.bunifuSeparator1.Location = new System.Drawing.Point(16, 95);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Size = new System.Drawing.Size(196, 2);
            this.bunifuSeparator1.TabIndex = 34;
            this.bunifuSeparator1.Transparency = 255;
            this.bunifuSeparator1.Vertical = false;
            // 
            // bunifuSeparator3
            // 
            this.bunifuSeparator3.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator3.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.bunifuSeparator3.LineThickness = 1;
            this.bunifuSeparator3.Location = new System.Drawing.Point(17, 193);
            this.bunifuSeparator3.Name = "bunifuSeparator3";
            this.bunifuSeparator3.Size = new System.Drawing.Size(412, 10);
            this.bunifuSeparator3.TabIndex = 34;
            this.bunifuSeparator3.Transparency = 255;
            this.bunifuSeparator3.Vertical = false;
            // 
            // bunifuSeparator2
            // 
            this.bunifuSeparator2.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.bunifuSeparator2.LineThickness = 1;
            this.bunifuSeparator2.Location = new System.Drawing.Point(17, 152);
            this.bunifuSeparator2.Name = "bunifuSeparator2";
            this.bunifuSeparator2.Size = new System.Drawing.Size(196, 2);
            this.bunifuSeparator2.TabIndex = 34;
            this.bunifuSeparator2.Transparency = 255;
            this.bunifuSeparator2.Vertical = false;
            // 
            // btnSaveTypeConn
            // 
            this.btnSaveTypeConn.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSaveTypeConn.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnSaveTypeConn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveTypeConn.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveTypeConn.Location = new System.Drawing.Point(316, 212);
            this.btnSaveTypeConn.Name = "btnSaveTypeConn";
            this.btnSaveTypeConn.Size = new System.Drawing.Size(113, 29);
            this.btnSaveTypeConn.TabIndex = 5;
            this.btnSaveTypeConn.Text = "Save";
            this.btnSaveTypeConn.UseVisualStyleBackColor = false;
            this.btnSaveTypeConn.Click += new System.EventHandler(this.btnSaveTypeConn_Click);
            // 
            // cboIntMod
            // 
            this.cboIntMod.BackColor = System.Drawing.Color.LightGray;
            this.cboIntMod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIntMod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboIntMod.Font = new System.Drawing.Font("Microsoft PhagsPa", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboIntMod.FormattingEnabled = true;
            this.cboIntMod.Items.AddRange(new object[] {
            "Database"});
            this.cboIntMod.Location = new System.Drawing.Point(10, 34);
            this.cboIntMod.Name = "cboIntMod";
            this.cboIntMod.Size = new System.Drawing.Size(201, 28);
            this.cboIntMod.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "Database Connection:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Integration Mode:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.lblLAM);
            this.tabPage2.Controls.Add(this.butCon);
            this.tabPage2.Controls.Add(this.btnSave);
            this.tabPage2.Controls.Add(this.txtPassword);
            this.tabPage2.Controls.Add(this.txtUser);
            this.tabPage2.Controls.Add(this.txtDB);
            this.tabPage2.Controls.Add(this.txtPort);
            this.tabPage2.Controls.Add(this.txtServer);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.lblControleAcesso);
            this.tabPage2.Controls.Add(this.lblDataConnection);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(439, 260);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Connection";
            // 
            // lblLAM
            // 
            this.lblLAM.AutoSize = true;
            this.lblLAM.BackColor = System.Drawing.Color.Transparent;
            this.lblLAM.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLAM.ForeColor = System.Drawing.Color.DimGray;
            this.lblLAM.Location = new System.Drawing.Point(10, 241);
            this.lblLAM.Name = "lblLAM";
            this.lblLAM.Size = new System.Drawing.Size(105, 16);
            this.lblLAM.TabIndex = 12;
            this.lblLAM.Text = "2019 AbstractBuild";
            // 
            // butCon
            // 
            this.butCon.BackColor = System.Drawing.Color.Transparent;
            this.butCon.Image = ((System.Drawing.Image)(resources.GetObject("butCon.Image")));
            this.butCon.ImageActive = null;
            this.butCon.Location = new System.Drawing.Point(279, 211);
            this.butCon.Name = "butCon";
            this.butCon.Size = new System.Drawing.Size(31, 30);
            this.butCon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.butCon.TabIndex = 24;
            this.butCon.TabStop = false;
            this.butCon.Zoom = 10;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft New Tai Lue", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(316, 212);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(113, 29);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.BorderColorFocused = System.Drawing.Color.Blue;
            this.txtPassword.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPassword.BorderColorMouseHover = System.Drawing.Color.Blue;
            this.txtPassword.BorderThickness = 1;
            this.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPassword.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(25)))), ((int)(((byte)(75)))));
            this.txtPassword.isPassword = true;
            this.txtPassword.Location = new System.Drawing.Point(277, 160);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(152, 33);
            this.txtPassword.TabIndex = 10;
            this.txtPassword.Text = "root";
            this.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtUser
            // 
            this.txtUser.BorderColorFocused = System.Drawing.Color.Blue;
            this.txtUser.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtUser.BorderColorMouseHover = System.Drawing.Color.Blue;
            this.txtUser.BorderThickness = 1;
            this.txtUser.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUser.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtUser.isPassword = false;
            this.txtUser.Location = new System.Drawing.Point(10, 160);
            this.txtUser.Margin = new System.Windows.Forms.Padding(4);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(256, 33);
            this.txtUser.TabIndex = 6;
            this.txtUser.Text = "root";
            this.txtUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtDB
            // 
            this.txtDB.BorderColorFocused = System.Drawing.Color.Blue;
            this.txtDB.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDB.BorderColorMouseHover = System.Drawing.Color.Blue;
            this.txtDB.BorderThickness = 1;
            this.txtDB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDB.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtDB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDB.isPassword = false;
            this.txtDB.Location = new System.Drawing.Point(10, 94);
            this.txtDB.Margin = new System.Windows.Forms.Padding(4);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(256, 31);
            this.txtDB.TabIndex = 4;
            this.txtDB.Text = "::";
            this.txtDB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtPort
            // 
            this.txtPort.BorderColorFocused = System.Drawing.Color.Blue;
            this.txtPort.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPort.BorderColorMouseHover = System.Drawing.Color.Blue;
            this.txtPort.BorderThickness = 1;
            this.txtPort.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPort.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPort.isPassword = false;
            this.txtPort.Location = new System.Drawing.Point(279, 94);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(152, 31);
            this.txtPort.TabIndex = 8;
            this.txtPort.Text = "3306";
            this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtServer
            // 
            this.txtServer.BorderColorFocused = System.Drawing.Color.Blue;
            this.txtServer.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtServer.BorderColorMouseHover = System.Drawing.Color.Blue;
            this.txtServer.BorderThickness = 1;
            this.txtServer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtServer.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtServer.isPassword = false;
            this.txtServer.Location = new System.Drawing.Point(10, 32);
            this.txtServer.Margin = new System.Windows.Forms.Padding(4);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(256, 31);
            this.txtServer.TabIndex = 2;
            this.txtServer.Text = "172.0.0.1";
            this.txtServer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(274, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "User:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Database:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(276, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Port:";
            // 
            // lblControleAcesso
            // 
            this.lblControleAcesso.AutoSize = true;
            this.lblControleAcesso.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControleAcesso.Location = new System.Drawing.Point(7, 11);
            this.lblControleAcesso.Name = "lblControleAcesso";
            this.lblControleAcesso.Size = new System.Drawing.Size(48, 17);
            this.lblControleAcesso.TabIndex = 0;
            this.lblControleAcesso.Text = "Server:";
            // 
            // lblDataConnection
            // 
            this.lblDataConnection.AutoSize = true;
            this.lblDataConnection.Font = new System.Drawing.Font("Microsoft New Tai Lue", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataConnection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(33)))), ((int)(((byte)(122)))));
            this.lblDataConnection.Location = new System.Drawing.Point(282, 11);
            this.lblDataConnection.Name = "lblDataConnection";
            this.lblDataConnection.Size = new System.Drawing.Size(151, 20);
            this.lblDataConnection.TabIndex = 1;
            this.lblDataConnection.Text = "DataBase Connection";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(51)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 391);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(447, 3);
            this.panel2.TabIndex = 19;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(51)))));
            this.panelHeader.Controls.Add(this.CircularPercGrande);
            this.panelHeader.Controls.Add(this.butClose);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(447, 99);
            this.panelHeader.TabIndex = 0;
            // 
            // CircularPercGrande
            // 
            this.CircularPercGrande.animated = true;
            this.CircularPercGrande.animationIterval = 1;
            this.CircularPercGrande.animationSpeed = 50;
            this.CircularPercGrande.BackColor = System.Drawing.Color.Transparent;
            this.CircularPercGrande.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CircularPercGrande.BackgroundImage")));
            this.CircularPercGrande.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F);
            this.CircularPercGrande.ForeColor = System.Drawing.Color.Transparent;
            this.CircularPercGrande.LabelVisible = false;
            this.CircularPercGrande.LineProgressThickness = 7;
            this.CircularPercGrande.LineThickness = 1;
            this.CircularPercGrande.Location = new System.Drawing.Point(4, 43);
            this.CircularPercGrande.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.CircularPercGrande.MaxValue = 100;
            this.CircularPercGrande.Name = "CircularPercGrande";
            this.CircularPercGrande.ProgressBackColor = System.Drawing.Color.Gray;
            this.CircularPercGrande.ProgressColor = System.Drawing.Color.LightGray;
            this.CircularPercGrande.Size = new System.Drawing.Size(56, 56);
            this.CircularPercGrande.TabIndex = 0;
            this.CircularPercGrande.Value = 20;
            // 
            // butClose
            // 
            this.butClose.BackColor = System.Drawing.Color.Transparent;
            this.butClose.Image = ((System.Drawing.Image)(resources.GetObject("butClose.Image")));
            this.butClose.ImageActive = null;
            this.butClose.Location = new System.Drawing.Point(422, 4);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(20, 18);
            this.butClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.butClose.TabIndex = 1;
            this.butClose.TabStop = false;
            this.butClose.Zoom = 10;
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.panelHeader;
            this.bunifuDragControl1.Vertical = true;
            // 
            // connectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 394);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "connectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "connectionForm";
            this.Load += new System.EventHandler(this.connectionForm_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butCon)).EndInit();
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.butClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelHeader;
        private Bunifu.Framework.UI.BunifuImageButton butClose;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuCircleProgressbar CircularPercGrande;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblLAM;
        private Bunifu.Framework.UI.BunifuImageButton butCon;
        private System.Windows.Forms.Button btnSave;
        private Bunifu.Framework.UI.BunifuMetroTextbox txtPassword;
        private Bunifu.Framework.UI.BunifuMetroTextbox txtUser;
        private Bunifu.Framework.UI.BunifuMetroTextbox txtDB;
        private Bunifu.Framework.UI.BunifuMetroTextbox txtPort;
        private Bunifu.Framework.UI.BunifuMetroTextbox txtServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblControleAcesso;
        private System.Windows.Forms.Label lblDataConnection;
        private System.Windows.Forms.Button btnSaveTypeConn;
        private System.Windows.Forms.ComboBox cboIntMod;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rdMySQL;
        private System.Windows.Forms.RadioButton rdSQLServer;
        private System.Windows.Forms.Panel panel4;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator1;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator3;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator2;
    }
}