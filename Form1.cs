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
using System.Net;
using System.IO;
using System.Diagnostics;


namespace Loader
{
    public partial class Form1 : MetroForm
    {
        string oldexepath;
        int version;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            version = 25;

            var random = new Random();
            var list = new List<string> { "fuck yall niggas", "yall nns", "owning celex" }; // ranndom messages
            int index = random.Next(list.Count);
            funnymessage.Text = (list[index]); // It will pick a random message and display it

            var ping = new System.Net.NetworkInformation.Ping(); 

            var result = ping.Send("github.com/movingfeds/testing"); // Send a ping to the url to make sure its live

            /* Send a web request to get the versionm, if it matches with the current version, procedude
             * if not, then it will download the another exe, delete itself and restart */
            if (result.Status == System.Net.NetworkInformation.IPStatus.Success) // if ping above succesful, procede
            {
                WebRequest request = WebRequest.Create("https://URL/version.txt");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response == null || response.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show("Could not connect");
                }
                else
                {
                    timer1.Start();
                } 
            }
            else
            {
                MessageBox.Show("Could not connect");
            }

            string path = @"C:\Users\Public\Documents\LoaderPath.txt";
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        oldexepath = s;
                    }

                    sr.Close();
                }
            }

            if (File.Exists(oldexepath))
            {
                File.Delete(oldexepath);
                File.Delete(path);
            }

            var webRequest = WebRequest.Create("https://URL/status.txt");
            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                var status = reader.ReadToEnd();

                if (status == "0")
                {
                    MessageBox.Show("Error: The cheat is down!");
                    Application.Exit();
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var webRequest = WebRequest.Create("https://URL/version.txt");
            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                var webverison = reader.ReadToEnd();

                if (version.ToString() != webverison)
                {
                    pictureBox5.Visible = true;
                    timer1.Stop();
                    timer2.Start();
                }
                else
                {
                    metroLabel2.Text = "Up to date!";
                    timer3.Start();
                    timer1.Stop();
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            timer2.Stop();

            string path = @"C:\Users\Public\Documents\LoaderPath.txt";

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(System.Reflection.Assembly.GetEntryAssembly().Location);
                }
            }

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Downloading the new version
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile("https://URL/loader.exe", Directory.GetCurrentDirectory() + "/" + finalString + ".exe"); // Direct link to your exe
            System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "/" + finalString + ".exe");
            Application.Exit();
        }


// CHECK IF USB 
      private void timer3_Tick(object sender, EventArgs e)
        {
            string path = Process.GetCurrentProcess().MainModule.FileName;
            FileInfo fileInfo = new FileInfo(path);
            string driveRoot = fileInfo.Directory.Root.Name;
            DriveInfo driveInfo = new DriveInfo(driveRoot);
            if (driveInfo.DriveType != DriveType.Removable)
            {
                timer3.Stop();
                timer4.Start();
            }
            else
            {
                timer3.Stop();
                timer4.Start();
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Stop();
            this.Hide();
            var form2 = new Form2();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }
    }
} 

