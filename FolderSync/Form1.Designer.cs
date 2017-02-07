namespace FolderSync
{
    partial class mainFolderSync
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainFolderSync));
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDest = new System.Windows.Forms.Button();
            this.btnSource = new System.Windows.Forms.Button();
            this.grpBoxSource = new System.Windows.Forms.GroupBox();
            this.lblSource = new System.Windows.Forms.Label();
            this.grpboxDest = new System.Windows.Forms.GroupBox();
            this.lblDest = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnRun = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.grpBoxSource.SuspendLayout();
            this.grpboxDest.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(423, 226);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDest
            // 
            this.btnDest.Location = new System.Drawing.Point(99, 226);
            this.btnDest.Name = "btnDest";
            this.btnDest.Size = new System.Drawing.Size(75, 23);
            this.btnDest.TabIndex = 1;
            this.btnDest.Text = "Destination";
            this.btnDest.UseVisualStyleBackColor = true;
            this.btnDest.Click += new System.EventHandler(this.btnDest_Click);
            // 
            // btnSource
            // 
            this.btnSource.Location = new System.Drawing.Point(12, 226);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(75, 23);
            this.btnSource.TabIndex = 2;
            this.btnSource.Text = "Source";
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // grpBoxSource
            // 
            this.grpBoxSource.Controls.Add(this.lblSource);
            this.grpBoxSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.grpBoxSource.Location = new System.Drawing.Point(13, 13);
            this.grpBoxSource.Name = "grpBoxSource";
            this.grpBoxSource.Size = new System.Drawing.Size(485, 46);
            this.grpBoxSource.TabIndex = 3;
            this.grpBoxSource.TabStop = false;
            this.grpBoxSource.Text = "Selected Source";
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FolderSync.Properties.Settings.Default, "lblSource", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblSource.Location = new System.Drawing.Point(7, 20);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(41, 15);
            this.lblSource.TabIndex = 0;
            this.lblSource.Text = global::FolderSync.Properties.Settings.Default.lblSource;
            // 
            // grpboxDest
            // 
            this.grpboxDest.Controls.Add(this.lblDest);
            this.grpboxDest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.grpboxDest.Location = new System.Drawing.Point(13, 65);
            this.grpboxDest.Name = "grpboxDest";
            this.grpboxDest.Size = new System.Drawing.Size(485, 46);
            this.grpboxDest.TabIndex = 4;
            this.grpboxDest.TabStop = false;
            this.grpboxDest.Text = "Selected Destination";
            // 
            // lblDest
            // 
            this.lblDest.AutoSize = true;
            this.lblDest.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FolderSync.Properties.Settings.Default, "lblDest", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblDest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDest.Location = new System.Drawing.Point(7, 20);
            this.lblDest.Name = "lblDest";
            this.lblDest.Size = new System.Drawing.Size(0, 15);
            this.lblDest.TabIndex = 0;
            this.lblDest.Text = global::FolderSync.Properties.Settings.Default.lblDest;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(333, 226);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 5;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.AcceptsReturn = true;
            this.txtStatus.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtStatus.Location = new System.Drawing.Point(13, 130);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtStatus.Size = new System.Drawing.Size(484, 72);
            this.txtStatus.TabIndex = 6;
            // 
            // mainFolderSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 256);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.grpboxDest);
            this.Controls.Add(this.grpBoxSource);
            this.Controls.Add(this.btnSource);
            this.Controls.Add(this.btnDest);
            this.Controls.Add(this.btnExit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "mainFolderSync";
            this.Text = "FolderSync";
            this.grpBoxSource.ResumeLayout(false);
            this.grpBoxSource.PerformLayout();
            this.grpboxDest.ResumeLayout(false);
            this.grpboxDest.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDest;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.GroupBox grpBoxSource;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.GroupBox grpboxDest;
        private System.Windows.Forms.Label lblDest;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txtStatus;
    }
}

