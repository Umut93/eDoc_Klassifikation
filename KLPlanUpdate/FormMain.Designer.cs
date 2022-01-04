namespace Fujitsu.Tools.KLPlanUpdate
{
  partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.treeViewKLPlan = new System.Windows.Forms.TreeView();
            this.imageListMain = new System.Windows.Forms.ImageList(this.components);
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonSelected = new System.Windows.Forms.Button();
            this.buttonReload = new System.Windows.Forms.Button();
            this.buttonDoUpdate = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.KLversion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewKLPlan
            // 
            this.treeViewKLPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewKLPlan.ImageIndex = 0;
            this.treeViewKLPlan.ImageList = this.imageListMain;
            this.treeViewKLPlan.Location = new System.Drawing.Point(12, 39);
            this.treeViewKLPlan.Name = "treeViewKLPlan";
            this.treeViewKLPlan.SelectedImageIndex = 0;
            this.treeViewKLPlan.Size = new System.Drawing.Size(712, 436);
            this.treeViewKLPlan.TabIndex = 0;
            // 
            // imageListMain
            // 
            this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
            this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListMain.Images.SetKeyName(0, "check_16.png");
            this.imageListMain.Images.SetKeyName(1, "document_add_16.png");
            this.imageListMain.Images.SetKeyName(2, "edit_16.png");
            this.imageListMain.Images.SetKeyName(3, "remove_16.png");
            this.imageListMain.Images.SetKeyName(4, "case_16.png");
            // 
            // panelButtons
            // 
            this.panelButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelButtons.Controls.Add(this.buttonSelected);
            this.panelButtons.Controls.Add(this.buttonReload);
            this.panelButtons.Controls.Add(this.buttonDoUpdate);
            this.panelButtons.Controls.Add(this.buttonClose);
            this.panelButtons.Location = new System.Drawing.Point(89, 481);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(635, 33);
            this.panelButtons.TabIndex = 1;
            // 
            // buttonSelected
            // 
            this.buttonSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelected.Location = new System.Drawing.Point(98, 7);
            this.buttonSelected.Name = "buttonSelected";
            this.buttonSelected.Size = new System.Drawing.Size(106, 23);
            this.buttonSelected.TabIndex = 3;
            this.buttonSelected.Text = "&Gem valgte";
            this.buttonSelected.UseVisualStyleBackColor = true;
            this.buttonSelected.Visible = false;
            this.buttonSelected.Click += new System.EventHandler(this.buttonSelected_Click);
            // 
            // buttonReload
            // 
            this.buttonReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReload.Location = new System.Drawing.Point(17, 7);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(75, 23);
            this.buttonReload.TabIndex = 2;
            this.buttonReload.Text = "&Genindlæs";
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Visible = false;
            this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
            // 
            // buttonDoUpdate
            // 
            this.buttonDoUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDoUpdate.Location = new System.Drawing.Point(448, 7);
            this.buttonDoUpdate.Name = "buttonDoUpdate";
            this.buttonDoUpdate.Size = new System.Drawing.Size(106, 23);
            this.buttonDoUpdate.TabIndex = 1;
            this.buttonDoUpdate.Text = "&Udfør opdatering";
            this.buttonDoUpdate.UseVisualStyleBackColor = true;
            this.buttonDoUpdate.Click += new System.EventHandler(this.buttonDoUpdate_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(560, 7);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "&Luk";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // KLversion
            // 
            this.KLversion.Location = new System.Drawing.Point(181, 13);
            this.KLversion.Name = "KLversion";
            this.KLversion.ReadOnly = true;
            this.KLversion.Size = new System.Drawing.Size(337, 20);
            this.KLversion.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Installeret KL version i databasen:";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 517);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KLversion);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.treeViewKLPlan);
            this.Name = "FormMain";
            this.Text = "Opdatering af KL Journalplan i eDoc";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TreeView treeViewKLPlan;
    private System.Windows.Forms.Panel panelButtons;
    private System.Windows.Forms.Button buttonDoUpdate;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.ImageList imageListMain;
    private System.Windows.Forms.Button buttonReload;
    private System.Windows.Forms.Button buttonSelected;
    private System.Windows.Forms.TextBox KLversion;
    private System.Windows.Forms.Label label1;
  }
}

