namespace ADown
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.grpLogin = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.grpAlbum = new System.Windows.Forms.GroupBox();
            this.btnADown = new System.Windows.Forms.Button();
            this.stProg = new System.Windows.Forms.Label();
            this.stStat = new System.Windows.Forms.Label();
            this.lblProg = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnChange = new System.Windows.Forms.Button();
            this.txtSv = new System.Windows.Forms.TextBox();
            this.lblSv = new System.Windows.Forms.Label();
            this.txtAl = new System.Windows.Forms.TextBox();
            this.lblAl = new System.Windows.Forms.Label();
            this.lblDev = new System.Windows.Forms.Label();
            this.fbdSv = new System.Windows.Forms.FolderBrowserDialog();
            this.lnkDonate = new System.Windows.Forms.LinkLabel();
            this.grpLogin.SuspendLayout();
            this.grpAlbum.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLogin
            // 
            this.grpLogin.Controls.Add(this.txtPassword);
            this.grpLogin.Controls.Add(this.txtEmail);
            this.grpLogin.Controls.Add(this.lblPassword);
            this.grpLogin.Controls.Add(this.lblEmail);
            this.grpLogin.Location = new System.Drawing.Point(12, 12);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Size = new System.Drawing.Size(453, 106);
            this.grpLogin.TabIndex = 0;
            this.grpLogin.TabStop = false;
            this.grpLogin.Text = "Account Information";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(112, 62);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(276, 23);
            this.txtPassword.TabIndex = 1;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(112, 30);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(276, 23);
            this.txtEmail.TabIndex = 0;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(46, 65);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(60, 15);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "Password:";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(67, 33);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 15);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "Email:";
            // 
            // grpAlbum
            // 
            this.grpAlbum.Controls.Add(this.btnADown);
            this.grpAlbum.Controls.Add(this.stProg);
            this.grpAlbum.Controls.Add(this.stStat);
            this.grpAlbum.Controls.Add(this.lblProg);
            this.grpAlbum.Controls.Add(this.lblStatus);
            this.grpAlbum.Controls.Add(this.btnChange);
            this.grpAlbum.Controls.Add(this.txtSv);
            this.grpAlbum.Controls.Add(this.lblSv);
            this.grpAlbum.Controls.Add(this.txtAl);
            this.grpAlbum.Controls.Add(this.lblAl);
            this.grpAlbum.Location = new System.Drawing.Point(12, 124);
            this.grpAlbum.Name = "grpAlbum";
            this.grpAlbum.Size = new System.Drawing.Size(453, 218);
            this.grpAlbum.TabIndex = 1;
            this.grpAlbum.TabStop = false;
            this.grpAlbum.Text = "Album Download";
            // 
            // btnADown
            // 
            this.btnADown.Location = new System.Drawing.Point(191, 174);
            this.btnADown.Name = "btnADown";
            this.btnADown.Size = new System.Drawing.Size(75, 27);
            this.btnADown.TabIndex = 5;
            this.btnADown.Text = "Download";
            this.btnADown.UseVisualStyleBackColor = true;
            this.btnADown.Click += new System.EventHandler(this.btnADown_Click);
            // 
            // stProg
            // 
            this.stProg.AutoSize = true;
            this.stProg.Location = new System.Drawing.Point(240, 136);
            this.stProg.Name = "stProg";
            this.stProg.Size = new System.Drawing.Size(36, 15);
            this.stProg.TabIndex = 4;
            this.stProg.Text = "0 of 0";
            // 
            // stStat
            // 
            this.stStat.AutoSize = true;
            this.stStat.Location = new System.Drawing.Point(240, 112);
            this.stStat.Name = "stStat";
            this.stStat.Size = new System.Drawing.Size(26, 15);
            this.stStat.TabIndex = 4;
            this.stStat.Text = "Idle";
            // 
            // lblProg
            // 
            this.lblProg.AutoSize = true;
            this.lblProg.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProg.Location = new System.Drawing.Point(179, 136);
            this.lblProg.Name = "lblProg";
            this.lblProg.Size = new System.Drawing.Size(58, 15);
            this.lblProg.TabIndex = 3;
            this.lblProg.Text = "Progress:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(192, 112);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(45, 15);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status:";
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(333, 68);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(70, 23);
            this.btnChange.TabIndex = 4;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // txtSv
            // 
            this.txtSv.Location = new System.Drawing.Point(112, 69);
            this.txtSv.Name = "txtSv";
            this.txtSv.Size = new System.Drawing.Size(215, 23);
            this.txtSv.TabIndex = 3;
            // 
            // lblSv
            // 
            this.lblSv.AutoSize = true;
            this.lblSv.Location = new System.Drawing.Point(55, 72);
            this.lblSv.Name = "lblSv";
            this.lblSv.Size = new System.Drawing.Size(51, 15);
            this.lblSv.TabIndex = 0;
            this.lblSv.Text = "Save To:";
            // 
            // txtAl
            // 
            this.txtAl.Location = new System.Drawing.Point(112, 34);
            this.txtAl.Name = "txtAl";
            this.txtAl.Size = new System.Drawing.Size(291, 23);
            this.txtAl.TabIndex = 2;
            // 
            // lblAl
            // 
            this.lblAl.AutoSize = true;
            this.lblAl.Location = new System.Drawing.Point(36, 37);
            this.lblAl.Name = "lblAl";
            this.lblAl.Size = new System.Drawing.Size(70, 15);
            this.lblAl.TabIndex = 0;
            this.lblAl.Text = "Album URL:";
            // 
            // lblDev
            // 
            this.lblDev.AutoSize = true;
            this.lblDev.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDev.ForeColor = System.Drawing.Color.Gray;
            this.lblDev.Location = new System.Drawing.Point(362, 350);
            this.lblDev.Name = "lblDev";
            this.lblDev.Size = new System.Drawing.Size(103, 13);
            this.lblDev.TabIndex = 2;
            this.lblDev.Text = "Developed by Ruel";
            this.lblDev.Click += new System.EventHandler(this.lblDev_Click);
            // 
            // lnkDonate
            // 
            this.lnkDonate.AutoSize = true;
            this.lnkDonate.LinkColor = System.Drawing.Color.DimGray;
            this.lnkDonate.Location = new System.Drawing.Point(12, 349);
            this.lnkDonate.Name = "lnkDonate";
            this.lnkDonate.Size = new System.Drawing.Size(45, 15);
            this.lnkDonate.TabIndex = 3;
            this.lnkDonate.TabStop = true;
            this.lnkDonate.Text = "Donate";
            this.lnkDonate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDonate_LinkClicked);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 371);
            this.Controls.Add(this.lnkDonate);
            this.Controls.Add(this.lblDev);
            this.Controls.Add(this.grpAlbum);
            this.Controls.Add(this.grpLogin);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "ADown - Facebook Album Downloader";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grpLogin.ResumeLayout(false);
            this.grpLogin.PerformLayout();
            this.grpAlbum.ResumeLayout(false);
            this.grpAlbum.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.GroupBox grpAlbum;
        private System.Windows.Forms.Label lblDev;
        private System.Windows.Forms.TextBox txtAl;
        private System.Windows.Forms.Label lblAl;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.TextBox txtSv;
        private System.Windows.Forms.Label lblSv;
        private System.Windows.Forms.Button btnADown;
        private System.Windows.Forms.Label stProg;
        private System.Windows.Forms.Label stStat;
        private System.Windows.Forms.Label lblProg;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.FolderBrowserDialog fbdSv;
        private System.Windows.Forms.LinkLabel lnkDonate;
    }
}

