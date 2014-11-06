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
using System.Data.SQLite;
//using System.Configuration;
//using DBBackup.Properties;

namespace DBBackup
{
  public partial class frmMain : Form
  {
    //string DBPath, LocalPath;

    string guessDirectory()
    {
      string dropBoxPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      dropBoxPath += Path.DirectorySeparatorChar + "Dropbox" + Path.DirectorySeparatorChar;

      string db = dropBoxPath + "config.db";
      SQLiteConnection con = new SQLiteConnection("Data Source=" + db);
      con.Open();
      SQLiteCommand cmd = con.CreateCommand();
      cmd.CommandText = "select value from config where key = 'dropbox_path'";
      
      string dir = String.Empty;
      using (SQLiteDataReader dr = cmd.ExecuteReader())
      {
        while (dr.Read())
        {
          dir = dr.GetString(0);
        }
      }

      if (dir == String.Empty)
      {
        // now we need to read the host.db file and decode it.
        string hostFile = dropBoxPath + "host.db";
        string data = String.Empty;
        using (StreamReader sr = new StreamReader(hostFile))
        {
          //string data = sr.ReadToEnd();
          while ((data = sr.ReadLine()) != null)
            dir = Base64.Decode(data);
        }
      }

      return dir;
      /*
      string dir = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
      // assuming documents exists, windows complains about that
      if (Directory.Exists(dir + Path.DirectorySeparatorChar + "My Dropbox"))
        dir += Path.DirectorySeparatorChar + "My Dropbox";

      return dir;
      */
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
      lbMessage.Items.Add(e.ChangeType + ": " + e.FullPath);
      timer.Enabled = true;
    }

    private void fswWatch_Renamed(object sender, RenamedEventArgs e)
    {
      lbMessage.Items.Add(e.ChangeType + ": " + e.OldFullPath + " renamed to " + e.FullPath);
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
      fswWatch.EnableRaisingEvents = false;

      GetFiles(lblDBPath.Text);

      foreach (string dir in lbDirs.Items)
      {
        string newdir = dir.Replace(lblDBPath.Text, lblLocalPath.Text);
        Directory.CreateDirectory(newdir);
      }

      foreach (string file in lbFiles.Items)
      {
        string newfile = file.Replace(lblDBPath.Text, lblLocalPath.Text);
        if (File.Exists(newfile))
          File.Delete(newfile);

        // lazy man's way of doing things
        // if I can't copy the file, try again...
        try
        {
          File.Move(file, newfile);
        }
        catch //(Exception ex)
        {
          timer.Enabled = true;
        }        
      }

      
      foreach (string dir in lbDirs.Items)
      {
        string newdir = dir.Replace(lblDBPath.Text, lblLocalPath.Text);
        //MessageBox.Show(dir);
        try
        {
          Directory.Delete(dir, true);
        }
        catch { }
      }
      fswWatch.EnableRaisingEvents = true;

      //return;

      //// Add Support for directorys
      //Array dirs = Directory.GetDirectories(lblDBPath.Text);
      //foreach (string dir in dirs)
      //{
      //  MessageBox.Show(dir);
      //}

      //Array files =  Directory.GetFiles(lblDBPath.Text);
      //foreach (string file in files)
      //{
      //  string newfile = file.Replace(lblDBPath.Text, lblLocalPath.Text);
      //  lbMessage.Items.Add("Copying: " + file + " to " + newfile);
      //  //File.Copy(dir, newdir, true);
      //  if (File.Exists(newfile))
      //    File.Delete(newfile);

      //  // lazy man's way of doing things
      //  // if I can't copy the file, try again...
      //  try
      //  {
      //    File.Move(file, newfile);
      //  }
      //  catch //(Exception ex)
      //  {
      //    timer.Enabled = true;
      //  }
      //}

      //timer.Enabled = true;
    }

    // Make this work with the above code
    private void GetFiles(string path)
    {
      //this.Text = path;
      //Application.DoEvents();

      // Look for the file in our directory
      try
      {
        string[] files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
        if (files.Length > 0)
          lbFiles.Items.AddRange(files);

        // and do the same for each sub directory
        string[] dirs = Directory.GetDirectories(path);
        foreach (string dir in dirs)
        {
          lbDirs.Items.Add(dir);
          GetFiles(dir + Path.DirectorySeparatorChar);
        }
      }
      catch (Exception e)
      {
        lbMessage.Items.Add("ERROR: " + e.Message);
      }
    }
  }
}
