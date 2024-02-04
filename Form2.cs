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
using HWIDGrabber;


namespace Loader
{
    public partial class Form2 : MetroForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        string hwid;

        private void Form2_Load(object sender, EventArgs e)
        {
            hwid = HWDI.GetMachineGuid();

            if (Properties.Settings.Default.Checked == true)
            {
                metroTextBox1.Text = Properties.Settings.Default.Username;
                metroTextBox2.Text = Properties.Settings.Default.Password;
                metroCheckBox1.Checked = Properties.Settings.Default.Checked;
            }
        }

        string UppercaseFirst(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string str = metroTextBox1.Text;
            string upstr = UppercaseFirst(str);

            Properties.Settings.Default.Username = upstr;
            Properties.Settings.Default.Password = metroTextBox2.Text;
            Properties.Settings.Default.Checked = metroCheckBox1.Checked;
            Properties.Settings.Default.Save();
            string hwid = System.Security.Principal.WindowsIdentity.GetCurrent().User.Value;

            webBrowser1.Navigate("https://URL/check.php?username=" + metroTextBox1.Text + "&password=" + metroTextBox2.Text + "&hwid=" + hwid);

            /* What you gotta do
             * You need to send a web request to a PHP file, which checks the person's details.
             * You can edit the responses, but the ones now are as follows:
             * p1 = all is good
             * p0 = all is not good
             * */
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.DocumentText.Contains("p1"))
            {
                this.Hide();
                var form3 = new Form3();
                form3.Closed += (s, args) => this.Close();
                form3.Show();
            }
            else
            {
                MessageBox.Show("Password / HWID Incorrect!");
            }
        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
