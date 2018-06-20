using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MistyManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var url = string.Format("https://api.yubico.com/wsapi/2.0/verify?otp={0}&id=38600&timeout=8&sl=50&nonce=asdjdnkavsndjkasndkjsnad", textBox1.Text);
            var resp = client.GetAsync(url).Result;
            var text = resp.Content.ReadAsStringAsync().Result;

            //MessageBox.Show(resp.Content.ReadAsStringAsync().Result);
            if (text.Contains("status=OK"))
            {
                panel1.Visible = true;
                button2.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var url = string.Format("http://{0}/api/beta/faces/training/start", textBox3.Text);
            var payload = string.Format("{{\"FaceId\":\"{0}\"}}", textBox2.Text);

            HttpClient client = new HttpClient();
            var resp = client.PostAsync(url, new StringContent(payload)).Result;
            if (resp.StatusCode == HttpStatusCode.OK)
                MessageBox.Show("Training started, look into camera");
            else
                MessageBox.Show("Couldn't train... Try again!");
        }
    }
}
