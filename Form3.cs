using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using ManualMapInjection.Injection;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Net;
using System.Media;


namespace Loader
{
    public partial class Form3 : MetroForm
    {
        public Form3()
        {
            InitializeComponent();
        }

        private String KeyText = "ASDfghJKL";
        private String FilePath = "";
        private const string DllStoragePath = "C:\\Users\\Public\\Documents\\";
        private List<String> CheatList;

        private void Form3_Load(object sender, EventArgs e)
        {
            metroLabel5.Text = "Welcome " + Properties.Settings.Default.Username + "!";

            string cheatlist_string;

            using (var client = new WebClient())
            {
                cheatlist_string = client.DownloadString("https://URL/askdll.php");
            }

            CheatList = cheatlist_string
                .Trim(new Char[] { '\n', '\r', '{', '}' })
                .Split(',').Select(x => x.Trim('"')).ToList();

            foreach (var cheat_dll in CheatList)
            {
                var name = cheat_dll.Replace(".loader.dll", null); 
                listBox1.Items.Add(name);
            }

            listBox1.Items.Add(" "); // last
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://yoururl/");
        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://yoururl/");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("");
        }

        private void metroLabel3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/");
        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to restart the loader", "yoururl", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        private void metroTrackBar1_Scroll(object sender, ScrollEventArgs e)
        {
            metroLabel7.Text = metroTrackBar1.Value + "ms";
        }

        private bool buttonisclicked = false;

        private string path = string.Empty;
        private void metroButton4_Click(object sender, EventArgs e)
        {
            bool buttonclicked = false;
            buttonisclicked = buttonclicked;
            string pathempty = string.Empty;
            path = pathempty;
            metroLabel11.Text = "Injection Status";
            int yuh = -1;
            listBox1.SelectedIndex = yuh;
            metroLabel3.Text = "Custom DLL";

        }
        private void metroButton2_Click(object sender, EventArgs e)
        {
            Thread.Sleep(metroTrackBar1.Value);

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "Dekstop";
                openFileDialog.Filter = "DLL files (*.dll)|*.dll|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {


                    FilePath = openFileDialog.FileName;
                    path = openFileDialog.FileName;
                    var file = File.ReadAllBytes(path);
                    metroLabel3.Text = path;
                    buttonisclicked = true;
                    metroLabel11.Text = "File Selected! Press Inject.";
                }

            }
        }

        private void CheckDownloadAndInject(string dll_name)
        {
            Thread.Sleep(metroTrackBar1.Value);

            var name = "csgo";
            var target = Process.GetProcessesByName(name).FirstOrDefault();

            if (target != null)
            {
                string path = DllStoragePath + dll_name.Replace(".loader", null);

                if (!File.Exists(path))
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile("http://URL/files/" + dll_name, path);
                    }
                }
                metroLabel11.Text = "Beginning injection - please wait.";
                var file = File.ReadAllBytes(path);
                var injector = new ManualMapInjector(target) { AsyncInjection = true };
                metroLabel9.Text = $"hmodule = 0x{injector.Inject(file).ToInt64():x8}";
                metroLabel11.Text = "Injection Complete!";

                if (metroCheckBox1.Checked == true)
                {
                    Application.Exit();
                }
            }
            else
            {
                metroLabel11.Text = "CS:GO Not Found! Please open it.";
            }
        }
        private void metroButton1_Click(object sender, EventArgs e)
        {

            int s = listBox1.SelectedIndex;

            if (buttonisclicked == true)
            {
                var name = "csgo";
                var target = Process.GetProcessesByName(name).FirstOrDefault();

                if (target != null)
                {

                    metroLabel11.Text = "Beginning injection - please wait.";
                    var file = File.ReadAllBytes(path);
                    //Thread.Sleep(10000);
                    var injector = new ManualMapInjector(target) { AsyncInjection = true };
                    metroLabel9.Text = $"hmodule = 0x{injector.Inject(file).ToInt64():x8}";
                    metroLabel11.Text = "Injection Complete!";
                    return;

                    if (metroCheckBox1.Checked == true)
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    metroLabel11.Text = "CS:GO Not Found! Please open it.";
                }
            }

            if (listBox1.SelectedIndex < CheatList.Count)
            {
                CheckDownloadAndInject(CheatList[listBox1.SelectedIndex]);
            }
            else if (buttonisclicked == true) // Empty
            {
                metroLabel11.Text = "CS:GO Not Found! Please open it.";
            }
            else if (listBox1.SelectedIndex == -1) // Empty
            {
                metroLabel11.Text = "Nothing selected";
            }

        }

        public bool overwriteifExist(string outName)
        {
            if (File.Exists(outName))
            {
                return true;
            }
            return true;
        }

        private void metroLabel14_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to restart the loader", "yoururl", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        private void metroLabel4_Click_1(object sender, EventArgs e)
        {
            var stats = "http://yoururl/loader/stats.php?username=" + Properties.Settings.Default.Username;
            var statstext = (new WebClient()).DownloadString(stats);
            metroLabel13.Text = statstext;
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            Process.Start("steam://rungameid/730"); // csgo
        }

    }
}
