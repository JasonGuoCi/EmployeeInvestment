using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HelloService.GlobalServiceClient client = new HelloService.GlobalServiceClient();
            textBox1.Text = client.GetSiteNavigation();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
