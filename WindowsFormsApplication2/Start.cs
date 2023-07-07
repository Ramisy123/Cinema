using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tabls tabls = new Tabls();
            tabls.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddData addData = new AddData();
            addData.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 buyTicket = new Form1();
            buyTicket.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            querys querysform = new querys();
            querysform.Show();

        }
    }
}
