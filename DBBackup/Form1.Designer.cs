﻿namespace DBBackup
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
      this.components = new System.ComponentModel.Container();
      this.fswWatch = new System.IO.FileSystemWatcher();
      this.label1 = new System.Windows.Forms.Label();
      this.btnChooseDB = new System.Windows.Forms.Button();
      this.btnChooseLocal = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.lblDBPath = new System.Windows.Forms.Label();
      this.lblLocalPath = new System.Windows.Forms.Label();
      this.listBox1 = new System.Windows.Forms.ListBox();
      this.Browse = new System.Windows.Forms.FolderBrowserDialog();
      this.cbGo = new System.Windows.Forms.CheckBox();
      this.timer = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.fswWatch)).BeginInit();
      this.SuspendLayout();
      // 
      // fswWatch
      // 
      this.fswWatch.IncludeSubdirectories = true;
      this.fswWatch.SynchronizingObject = this;
      this.fswWatch.Renamed += new System.IO.RenamedEventHandler(this.fswWatch_Renamed);
      this.fswWatch.Created += new System.IO.FileSystemEventHandler(this.fswWatch_Changed);
      this.fswWatch.Changed += new System.IO.FileSystemEventHandler(this.fswWatch_Changed);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(95, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Dropbox Directory:";
      // 
      // btnChooseDB
      // 
      this.btnChooseDB.Location = new System.Drawing.Point(114, 8);
      this.btnChooseDB.Name = "btnChooseDB";
      this.btnChooseDB.Size = new System.Drawing.Size(158, 23);
      this.btnChooseDB.TabIndex = 1;
      this.btnChooseDB.Text = "Choose Dropbox Path";
      this.btnChooseDB.UseVisualStyleBackColor = true;
      this.btnChooseDB.Click += new System.EventHandler(this.btnChooseDB_Click);
      // 
      // btnChooseLocal
      // 
      this.btnChooseLocal.Location = new System.Drawing.Point(114, 50);
      this.btnChooseLocal.Name = "btnChooseLocal";
      this.btnChooseLocal.Size = new System.Drawing.Size(158, 23);
      this.btnChooseLocal.TabIndex = 3;
      this.btnChooseLocal.Text = "Choose Local Path";
      this.btnChooseLocal.UseVisualStyleBackColor = true;
      this.btnChooseLocal.Click += new System.EventHandler(this.btnChooseLocal_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(27, 55);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(81, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Local Directory:";
      // 
      // lblDBPath
      // 
      this.lblDBPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DBBackup.Properties.Settings.Default, "DBPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.lblDBPath.Location = new System.Drawing.Point(13, 34);
      this.lblDBPath.Name = "lblDBPath";
      this.lblDBPath.Size = new System.Drawing.Size(259, 13);
      this.lblDBPath.TabIndex = 4;
      this.lblDBPath.Text = global::DBBackup.Properties.Settings.Default.DBPath;
      // 
      // lblLocalPath
      // 
      this.lblLocalPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::DBBackup.Properties.Settings.Default, "LocalPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.lblLocalPath.Location = new System.Drawing.Point(13, 76);
      this.lblLocalPath.Name = "lblLocalPath";
      this.lblLocalPath.Size = new System.Drawing.Size(259, 20);
      this.lblLocalPath.TabIndex = 5;
      this.lblLocalPath.Text = global::DBBackup.Properties.Settings.Default.LocalPath;
      // 
      // listBox1
      // 
      this.listBox1.FormattingEnabled = true;
      this.listBox1.HorizontalScrollbar = true;
      this.listBox1.Location = new System.Drawing.Point(16, 119);
      this.listBox1.Name = "listBox1";
      this.listBox1.Size = new System.Drawing.Size(256, 134);
      this.listBox1.TabIndex = 6;
      // 
      // cbGo
      // 
      this.cbGo.AutoSize = true;
      this.cbGo.Location = new System.Drawing.Point(16, 96);
      this.cbGo.Name = "cbGo";
      this.cbGo.Size = new System.Drawing.Size(40, 17);
      this.cbGo.TabIndex = 7;
      this.cbGo.Text = "Go";
      this.cbGo.UseVisualStyleBackColor = true;
      this.cbGo.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
      // 
      // timer
      // 
      this.timer.Interval = 1000;
      this.timer.Tick += new System.EventHandler(this.timer_Tick);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 264);
      this.Controls.Add(this.cbGo);
      this.Controls.Add(this.listBox1);
      this.Controls.Add(this.lblLocalPath);
      this.Controls.Add(this.lblDBPath);
      this.Controls.Add(this.btnChooseLocal);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.btnChooseDB);
      this.Controls.Add(this.label1);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(300, 300);
      this.MinimumSize = new System.Drawing.Size(300, 300);
      this.Name = "frmMain";
      this.Text = "Dropbox Backup";
      this.Load += new System.EventHandler(this.frmMain_Load);
      ((System.ComponentModel.ISupportInitialize)(this.fswWatch)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.IO.FileSystemWatcher fswWatch;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblDBPath;
    private System.Windows.Forms.Button btnChooseLocal;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnChooseDB;
    private System.Windows.Forms.Label lblLocalPath;
    private System.Windows.Forms.ListBox listBox1;
    private System.Windows.Forms.FolderBrowserDialog Browse;
    private System.Windows.Forms.CheckBox cbGo;
    private System.Windows.Forms.Timer timer;
  }
}

