
namespace TemporalAssassinUtility
{
    partial class frmTAU
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTAU));
            this.btnGML = new System.Windows.Forms.Button();
            this.txtGML = new System.Windows.Forms.TextBox();
            this.btnIU = new System.Windows.Forms.Button();
            this.gbxDirectory = new System.Windows.Forms.GroupBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.gbxProgress = new System.Windows.Forms.GroupBox();
            this.pbxProgress = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.fdiagFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.gbxDirectory.SuspendLayout();
            this.gbxProgress.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGML
            // 
            this.btnGML.Location = new System.Drawing.Point(10, 21);
            this.btnGML.Name = "btnGML";
            this.btnGML.Size = new System.Drawing.Size(126, 23);
            this.btnGML.TabIndex = 0;
            this.btnGML.Text = "Garry\'s Mod Location";
            this.btnGML.UseVisualStyleBackColor = true;
            this.btnGML.Click += new System.EventHandler(this.btnGML_Click);
            // 
            // txtGML
            // 
            this.txtGML.Location = new System.Drawing.Point(142, 22);
            this.txtGML.Name = "txtGML";
            this.txtGML.Size = new System.Drawing.Size(374, 22);
            this.txtGML.TabIndex = 1;
            // 
            // btnIU
            // 
            this.btnIU.Location = new System.Drawing.Point(199, 53);
            this.btnIU.Name = "btnIU";
            this.btnIU.Size = new System.Drawing.Size(126, 23);
            this.btnIU.TabIndex = 2;
            this.btnIU.Text = "Install";
            this.btnIU.UseVisualStyleBackColor = true;
            this.btnIU.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // gbxDirectory
            // 
            this.gbxDirectory.Controls.Add(this.btnUpdate);
            this.gbxDirectory.Controls.Add(this.btnIU);
            this.gbxDirectory.Controls.Add(this.btnGML);
            this.gbxDirectory.Controls.Add(this.txtGML);
            this.gbxDirectory.Location = new System.Drawing.Point(9, 4);
            this.gbxDirectory.Name = "gbxDirectory";
            this.gbxDirectory.Size = new System.Drawing.Size(527, 87);
            this.gbxDirectory.TabIndex = 5;
            this.gbxDirectory.TabStop = false;
            this.gbxDirectory.Text = "Manage Temporal Assassin Install";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(331, 53);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(126, 23);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // gbxProgress
            // 
            this.gbxProgress.Controls.Add(this.pbxProgress);
            this.gbxProgress.Location = new System.Drawing.Point(9, 97);
            this.gbxProgress.Name = "gbxProgress";
            this.gbxProgress.Size = new System.Drawing.Size(527, 56);
            this.gbxProgress.TabIndex = 6;
            this.gbxProgress.TabStop = false;
            this.gbxProgress.Text = "Temporal Assassin Installation Progress";
            // 
            // pbxProgress
            // 
            this.pbxProgress.Location = new System.Drawing.Point(10, 21);
            this.pbxProgress.Name = "pbxProgress";
            this.pbxProgress.Size = new System.Drawing.Size(506, 23);
            this.pbxProgress.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtbOutput);
            this.groupBox1.Location = new System.Drawing.Point(9, 159);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 162);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Program Log";
            // 
            // rtbOutput
            // 
            this.rtbOutput.Location = new System.Drawing.Point(10, 16);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(506, 138);
            this.rtbOutput.TabIndex = 3;
            this.rtbOutput.Text = "";
            // 
            // frmTAU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 329);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbxProgress);
            this.Controls.Add(this.gbxDirectory);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(560, 368);
            this.MinimumSize = new System.Drawing.Size(560, 368);
            this.Name = "frmTAU";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Temporal Assassin Utility";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTAU_FormClosing);
            this.Load += new System.EventHandler(this.frmTAU_Load);
            this.gbxDirectory.ResumeLayout(false);
            this.gbxDirectory.PerformLayout();
            this.gbxProgress.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGML;
        private System.Windows.Forms.TextBox txtGML;
        private System.Windows.Forms.Button btnIU;
        private System.Windows.Forms.GroupBox gbxDirectory;
        private System.Windows.Forms.GroupBox gbxProgress;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ProgressBar pbxProgress;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.FolderBrowserDialog fdiagFolderBrowser;
    }
}

