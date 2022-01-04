namespace Fujitsu.Tools.KLPlanUpdate
{
  partial class FormConfig
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
      this.groupBoxDatabase = new System.Windows.Forms.GroupBox();
      this.radioButtonSQLAuth = new System.Windows.Forms.RadioButton();
      this.radioButtonWinAuth = new System.Windows.Forms.RadioButton();
      this.comboBoxDBDatabase = new System.Windows.Forms.ComboBox();
      this.label7 = new System.Windows.Forms.Label();
      this.textBoxDBPassword = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.textBoxDBUser = new System.Windows.Forms.TextBox();
      this.textBoxDBServer = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.groupBoxXmlFiles = new System.Windows.Forms.GroupBox();
      this.buttonBrowseStikord = new System.Windows.Forms.Button();
      this.buttonBrowseFacetter = new System.Windows.Forms.Button();
      this.buttonBrowseEmneplan = new System.Windows.Forms.Button();
      this.textBoxKLStikord = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.textBoxKLFacetter = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.textBoxKLEmneplan = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.openFileDialogXmlFiles = new System.Windows.Forms.OpenFileDialog();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.buttonOK = new System.Windows.Forms.Button();
      this.groupBoxDatabase.SuspendLayout();
      this.groupBoxXmlFiles.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBoxDatabase
      // 
      this.groupBoxDatabase.Controls.Add(this.radioButtonSQLAuth);
      this.groupBoxDatabase.Controls.Add(this.radioButtonWinAuth);
      this.groupBoxDatabase.Controls.Add(this.comboBoxDBDatabase);
      this.groupBoxDatabase.Controls.Add(this.label7);
      this.groupBoxDatabase.Controls.Add(this.textBoxDBPassword);
      this.groupBoxDatabase.Controls.Add(this.label6);
      this.groupBoxDatabase.Controls.Add(this.textBoxDBUser);
      this.groupBoxDatabase.Controls.Add(this.textBoxDBServer);
      this.groupBoxDatabase.Controls.Add(this.label5);
      this.groupBoxDatabase.Controls.Add(this.label4);
      this.groupBoxDatabase.Location = new System.Drawing.Point(12, 129);
      this.groupBoxDatabase.Name = "groupBoxDatabase";
      this.groupBoxDatabase.Size = new System.Drawing.Size(592, 195);
      this.groupBoxDatabase.TabIndex = 0;
      this.groupBoxDatabase.TabStop = false;
      this.groupBoxDatabase.Text = "eDoc database";
      // 
      // radioButtonSQLAuth
      // 
      this.radioButtonSQLAuth.AutoSize = true;
      this.radioButtonSQLAuth.Location = new System.Drawing.Point(11, 72);
      this.radioButtonSQLAuth.Name = "radioButtonSQLAuth";
      this.radioButtonSQLAuth.Size = new System.Drawing.Size(116, 17);
      this.radioButtonSQLAuth.TabIndex = 9;
      this.radioButtonSQLAuth.Text = "SQL authentication";
      this.radioButtonSQLAuth.UseVisualStyleBackColor = true;
      this.radioButtonSQLAuth.CheckedChanged += new System.EventHandler(this.radioButtonAuth_CheckedChanged);
      // 
      // radioButtonWinAuth
      // 
      this.radioButtonWinAuth.AutoSize = true;
      this.radioButtonWinAuth.Checked = true;
      this.radioButtonWinAuth.Location = new System.Drawing.Point(11, 49);
      this.radioButtonWinAuth.Name = "radioButtonWinAuth";
      this.radioButtonWinAuth.Size = new System.Drawing.Size(139, 17);
      this.radioButtonWinAuth.TabIndex = 8;
      this.radioButtonWinAuth.TabStop = true;
      this.radioButtonWinAuth.Text = "Windows authentication";
      this.radioButtonWinAuth.UseVisualStyleBackColor = true;
      this.radioButtonWinAuth.CheckedChanged += new System.EventHandler(this.radioButtonAuth_CheckedChanged);
      // 
      // comboBoxDBDatabase
      // 
      this.comboBoxDBDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBoxDBDatabase.FormattingEnabled = true;
      this.comboBoxDBDatabase.Location = new System.Drawing.Point(111, 153);
      this.comboBoxDBDatabase.Name = "comboBoxDBDatabase";
      this.comboBoxDBDatabase.Size = new System.Drawing.Size(217, 21);
      this.comboBoxDBDatabase.TabIndex = 7;
      this.comboBoxDBDatabase.DropDown += new System.EventHandler(this.comboBoxDBDatabase_DropDown);
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(6, 156);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(56, 13);
      this.label7.TabIndex = 6;
      this.label7.Text = "Database:";
      // 
      // textBoxDBPassword
      // 
      this.textBoxDBPassword.Enabled = false;
      this.textBoxDBPassword.Location = new System.Drawing.Point(111, 127);
      this.textBoxDBPassword.Name = "textBoxDBPassword";
      this.textBoxDBPassword.PasswordChar = '*';
      this.textBoxDBPassword.Size = new System.Drawing.Size(217, 20);
      this.textBoxDBPassword.TabIndex = 5;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(6, 130);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(56, 13);
      this.label6.TabIndex = 4;
      this.label6.Text = "Password:";
      // 
      // textBoxDBUser
      // 
      this.textBoxDBUser.Enabled = false;
      this.textBoxDBUser.Location = new System.Drawing.Point(111, 101);
      this.textBoxDBUser.Name = "textBoxDBUser";
      this.textBoxDBUser.Size = new System.Drawing.Size(217, 20);
      this.textBoxDBUser.TabIndex = 3;
      // 
      // textBoxDBServer
      // 
      this.textBoxDBServer.Location = new System.Drawing.Point(111, 24);
      this.textBoxDBServer.Name = "textBoxDBServer";
      this.textBoxDBServer.Size = new System.Drawing.Size(217, 20);
      this.textBoxDBServer.TabIndex = 2;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(6, 104);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(65, 13);
      this.label5.TabIndex = 1;
      this.label5.Text = "Brugernavn:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 27);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(90, 13);
      this.label4.TabIndex = 0;
      this.label4.Text = "Database Server:";
      // 
      // groupBoxXmlFiles
      // 
      this.groupBoxXmlFiles.Controls.Add(this.buttonBrowseStikord);
      this.groupBoxXmlFiles.Controls.Add(this.buttonBrowseFacetter);
      this.groupBoxXmlFiles.Controls.Add(this.buttonBrowseEmneplan);
      this.groupBoxXmlFiles.Controls.Add(this.textBoxKLStikord);
      this.groupBoxXmlFiles.Controls.Add(this.label3);
      this.groupBoxXmlFiles.Controls.Add(this.textBoxKLFacetter);
      this.groupBoxXmlFiles.Controls.Add(this.label2);
      this.groupBoxXmlFiles.Controls.Add(this.textBoxKLEmneplan);
      this.groupBoxXmlFiles.Controls.Add(this.label1);
      this.groupBoxXmlFiles.Location = new System.Drawing.Point(12, 12);
      this.groupBoxXmlFiles.Name = "groupBoxXmlFiles";
      this.groupBoxXmlFiles.Size = new System.Drawing.Size(592, 111);
      this.groupBoxXmlFiles.TabIndex = 1;
      this.groupBoxXmlFiles.TabStop = false;
      this.groupBoxXmlFiles.Text = "KL XML Filer";
      // 
      // buttonBrowseStikord
      // 
      this.buttonBrowseStikord.Location = new System.Drawing.Point(508, 73);
      this.buttonBrowseStikord.Name = "buttonBrowseStikord";
      this.buttonBrowseStikord.Size = new System.Drawing.Size(75, 23);
      this.buttonBrowseStikord.TabIndex = 8;
      this.buttonBrowseStikord.Text = "Gennemse...";
      this.buttonBrowseStikord.UseVisualStyleBackColor = true;
      this.buttonBrowseStikord.Click += new System.EventHandler(this.buttonBrowseStikord_Click);
      // 
      // buttonBrowseFacetter
      // 
      this.buttonBrowseFacetter.Location = new System.Drawing.Point(508, 47);
      this.buttonBrowseFacetter.Name = "buttonBrowseFacetter";
      this.buttonBrowseFacetter.Size = new System.Drawing.Size(75, 23);
      this.buttonBrowseFacetter.TabIndex = 7;
      this.buttonBrowseFacetter.Text = "Gennemse...";
      this.buttonBrowseFacetter.UseVisualStyleBackColor = true;
      this.buttonBrowseFacetter.Click += new System.EventHandler(this.buttonBrowseFacetter_Click);
      // 
      // buttonBrowseEmneplan
      // 
      this.buttonBrowseEmneplan.Location = new System.Drawing.Point(508, 21);
      this.buttonBrowseEmneplan.Name = "buttonBrowseEmneplan";
      this.buttonBrowseEmneplan.Size = new System.Drawing.Size(75, 23);
      this.buttonBrowseEmneplan.TabIndex = 6;
      this.buttonBrowseEmneplan.Text = "Gennemse...";
      this.buttonBrowseEmneplan.UseVisualStyleBackColor = true;
      this.buttonBrowseEmneplan.Click += new System.EventHandler(this.buttonBrowseEmneplan_Click);
      // 
      // textBoxKLStikord
      // 
      this.textBoxKLStikord.Location = new System.Drawing.Point(85, 75);
      this.textBoxKLStikord.Name = "textBoxKLStikord";
      this.textBoxKLStikord.ReadOnly = true;
      this.textBoxKLStikord.Size = new System.Drawing.Size(417, 20);
      this.textBoxKLStikord.TabIndex = 5;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 78);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(66, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "KLE Stikord:";
      // 
      // textBoxKLFacetter
      // 
      this.textBoxKLFacetter.Location = new System.Drawing.Point(85, 49);
      this.textBoxKLFacetter.Name = "textBoxKLFacetter";
      this.textBoxKLFacetter.ReadOnly = true;
      this.textBoxKLFacetter.Size = new System.Drawing.Size(417, 20);
      this.textBoxKLFacetter.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 52);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(72, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "KLE Facetter:";
      // 
      // textBoxKLEmneplan
      // 
      this.textBoxKLEmneplan.Location = new System.Drawing.Point(85, 23);
      this.textBoxKLEmneplan.Name = "textBoxKLEmneplan";
      this.textBoxKLEmneplan.ReadOnly = true;
      this.textBoxKLEmneplan.Size = new System.Drawing.Size(417, 20);
      this.textBoxKLEmneplan.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 26);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(80, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "KLE Emneplan:";
      // 
      // openFileDialogXmlFiles
      // 
      this.openFileDialogXmlFiles.FileName = "openFileDialog1";
      // 
      // buttonCancel
      // 
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(520, 339);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(75, 23);
      this.buttonCancel.TabIndex = 2;
      this.buttonCancel.Text = "&Annuller";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // buttonOK
      // 
      this.buttonOK.Location = new System.Drawing.Point(439, 339);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new System.Drawing.Size(75, 23);
      this.buttonOK.TabIndex = 3;
      this.buttonOK.Text = "&OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      // 
      // FormConfig
      // 
      this.AcceptButton = this.buttonOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.buttonCancel;
      this.ClientSize = new System.Drawing.Size(611, 391);
      this.Controls.Add(this.buttonOK);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.groupBoxXmlFiles);
      this.Controls.Add(this.groupBoxDatabase);
      this.Name = "FormConfig";
      this.Text = "Opdatering af KL Journalplan i eDoc - konfiguration - version 2.0";
      this.groupBoxDatabase.ResumeLayout(false);
      this.groupBoxDatabase.PerformLayout();
      this.groupBoxXmlFiles.ResumeLayout(false);
      this.groupBoxXmlFiles.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBoxDatabase;
    private System.Windows.Forms.GroupBox groupBoxXmlFiles;
    private System.Windows.Forms.TextBox textBoxKLEmneplan;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBoxKLStikord;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox textBoxKLFacetter;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button buttonBrowseEmneplan;
    private System.Windows.Forms.OpenFileDialog openFileDialogXmlFiles;
    private System.Windows.Forms.Button buttonBrowseStikord;
    private System.Windows.Forms.Button buttonBrowseFacetter;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.TextBox textBoxDBServer;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox textBoxDBPassword;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox textBoxDBUser;
    private System.Windows.Forms.ComboBox comboBoxDBDatabase;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.RadioButton radioButtonSQLAuth;
    private System.Windows.Forms.RadioButton radioButtonWinAuth;
  }
}