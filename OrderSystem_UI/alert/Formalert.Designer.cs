namespace OrderSystem_UI.alert
{
    partial class Formalert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Formalert));
            this.panel1 = new System.Windows.Forms.Panel();
            this.bunifuSeparator1 = new Bunifu.Framework.UI.BunifuSeparator();
            this.btnOk = new System.Windows.Forms.Button();
            this.ptBoxWarning = new System.Windows.Forms.PictureBox();
            this.ptBoxSuccess = new System.Windows.Forms.PictureBox();
            this.ptBoxError = new System.Windows.Forms.PictureBox();
            this.ptBoxInfo = new System.Windows.Forms.PictureBox();
            this.lblMsg2 = new System.Windows.Forms.Label();
            this.lblMsg1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptBoxWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptBoxSuccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptBoxError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptBoxInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.bunifuSeparator1);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.ptBoxWarning);
            this.panel1.Controls.Add(this.ptBoxSuccess);
            this.panel1.Controls.Add(this.ptBoxError);
            this.panel1.Controls.Add(this.ptBoxInfo);
            this.panel1.Controls.Add(this.lblMsg2);
            this.panel1.Controls.Add(this.lblMsg1);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(523, 141);
            this.panel1.TabIndex = 0;
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.bunifuSeparator1.LineThickness = 1;
            this.bunifuSeparator1.Location = new System.Drawing.Point(11, 95);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Size = new System.Drawing.Size(499, 1);
            this.bunifuSeparator1.TabIndex = 4;
            this.bunifuSeparator1.Transparency = 255;
            this.bunifuSeparator1.Vertical = false;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(207, 104);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(99, 27);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // ptBoxWarning
            // 
            this.ptBoxWarning.Image = ((System.Drawing.Image)(resources.GetObject("ptBoxWarning.Image")));
            this.ptBoxWarning.Location = new System.Drawing.Point(27, 30);
            this.ptBoxWarning.Name = "ptBoxWarning";
            this.ptBoxWarning.Size = new System.Drawing.Size(52, 46);
            this.ptBoxWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptBoxWarning.TabIndex = 2;
            this.ptBoxWarning.TabStop = false;
            // 
            // ptBoxSuccess
            // 
            this.ptBoxSuccess.Image = ((System.Drawing.Image)(resources.GetObject("ptBoxSuccess.Image")));
            this.ptBoxSuccess.Location = new System.Drawing.Point(27, 30);
            this.ptBoxSuccess.Name = "ptBoxSuccess";
            this.ptBoxSuccess.Size = new System.Drawing.Size(52, 46);
            this.ptBoxSuccess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptBoxSuccess.TabIndex = 2;
            this.ptBoxSuccess.TabStop = false;
            // 
            // ptBoxError
            // 
            this.ptBoxError.Image = ((System.Drawing.Image)(resources.GetObject("ptBoxError.Image")));
            this.ptBoxError.Location = new System.Drawing.Point(27, 30);
            this.ptBoxError.Name = "ptBoxError";
            this.ptBoxError.Size = new System.Drawing.Size(52, 46);
            this.ptBoxError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptBoxError.TabIndex = 2;
            this.ptBoxError.TabStop = false;
            // 
            // ptBoxInfo
            // 
            this.ptBoxInfo.Image = ((System.Drawing.Image)(resources.GetObject("ptBoxInfo.Image")));
            this.ptBoxInfo.Location = new System.Drawing.Point(27, 30);
            this.ptBoxInfo.Name = "ptBoxInfo";
            this.ptBoxInfo.Size = new System.Drawing.Size(52, 46);
            this.ptBoxInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptBoxInfo.TabIndex = 2;
            this.ptBoxInfo.TabStop = false;
            // 
            // lblMsg2
            // 
            this.lblMsg2.AutoSize = true;
            this.lblMsg2.Font = new System.Drawing.Font("Microsoft New Tai Lue", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg2.Location = new System.Drawing.Point(102, 59);
            this.lblMsg2.Name = "lblMsg2";
            this.lblMsg2.Size = new System.Drawing.Size(390, 17);
            this.lblMsg2.TabIndex = 1;
            this.lblMsg2.Text = "Lorem Ipsum ist ein einfacher Demo-Text für die  Schriftindustrie. ";
            // 
            // lblMsg1
            // 
            this.lblMsg1.AutoSize = true;
            this.lblMsg1.Font = new System.Drawing.Font("Microsoft New Tai Lue", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg1.Location = new System.Drawing.Point(102, 42);
            this.lblMsg1.Name = "lblMsg1";
            this.lblMsg1.Size = new System.Drawing.Size(247, 17);
            this.lblMsg1.TabIndex = 1;
            this.lblMsg1.Text = "Lorem Ipsum ist ein einfacher Demo-Text";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft New Tai Lue", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(101, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(47, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Alert";
            // 
            // Formalert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 141);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Formalert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formalert";
            this.Load += new System.EventHandler(this.Formalert_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptBoxWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptBoxSuccess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptBoxError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptBoxInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMsg1;
        private System.Windows.Forms.Label lblTitle;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PictureBox ptBoxInfo;
        private System.Windows.Forms.Label lblMsg2;
        private System.Windows.Forms.PictureBox ptBoxWarning;
        private System.Windows.Forms.PictureBox ptBoxSuccess;
        private System.Windows.Forms.PictureBox ptBoxError;
    }
}