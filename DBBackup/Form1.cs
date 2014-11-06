using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// ones I've added
using System.IO;
//using System.Configuration;
//using DBBackup.Properties;

namespace DBBackup
{
  public partial class frmMain : Form
  {
    //string DBPath, LocalPath;

    string guessDirectory()
    {
      string dir = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
      // assuming documents exists, windows complains about that
      if (Directory.Exists(dir + Path.DirectorySeparatorChar + "My Dropbox"))
        dir += Path.DirectorySeparatorChar + "My Dropbox";

      return dir;
    }

    void checkSettings()
    {
      if (Directory.Exists(lblDBPath.Text) && Directory.Exists(lblLocalPath.Text))
      {
        fswWatch.Path = lblDBPath.Text;
        fswWatch.EnableRaisingEvents = true;
        cbGo.Checked = true;
        timer.Enabled = true; // initial start
      }

    }

    public frmMain()
    {
      InitializeComponent();
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
      lblDBPath.Text = Properties.Settings.Default.DBPath;
      lblLocalPath.Text = Properties.Settings.Default.LocalPath;
      
      checkSettings();
    }

    private void btnChooseDB_Click(object sender, EventArgs e)
    {
      Browse.Description = "Select the Dropbox Path you would like to use as a Backup";
      Browse.SelectedPath = guessDirectory();
      if (Browse.ShowDialog() == DialogResult.OK)
      {
        Properties.Settings.Default.DBPath = Browse.SelectedPath;
        Properties.Settings.Default.Save();
      }
      checkSettings();
    }

    private void btnChooseLocal_Click(object sender, EventArgs e)
    {
      Browse.Description = "Select where you'd like your Dropbox files to go";
      Browse.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
      if (Browse.ShowDialog() == DialogResult.OK)
      {
        Properties.Settings.Default.LocalPath = Browse.SelectedPath;
        Properties.Settings.Default.Save();
      }
      checkSettings();
    }

    private void fswWatch_Changed(object sender, FileSystemEventArgs e)
    {
      listBox1.Items.Add(e.ChangeType + ": " + e.FullPath);
      timer.Enabled = true;
    }

    private void fswWatch_Renamed(object sender, RenamedEventArgs e)
    {
      listBox1.Items.Add(e.ChangeType + ": " + e.OldFullPath + " renamed to " + e.FullPath);
      timer.Enabled = true;
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
      fswWatch.EnableRaisingEvents = (sender as CheckBox).Checked;
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      // Walk the directory for any files and copy them to the right directory
      timer.Enabled = false;
      Array dirs =  Directory.GetFiles(lblDBPath.Text);
      foreach (string dir in dirs)
      {
        string newdir = dir.Replace(lblDBPath.Text, lblLocalPath.Text);
        listBox1.Items.Add("Copying: " + dir + " to " + newdir);
        //File.Copy(dir, newdir, true);
        if (File.Exists(newdir))
          File.Delete(newdir);

        // lazy man's way of doing things
        // if I can't copy the file, try again...
        try
        {
          File.Move(dir, newdir);
        }
        catch //(Exception ex)
        {
          timer.Enabled = true;
        }
      }

      //timer.Enabled = true;
    }

  }
}
