using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApplication2
{
    public partial class Tabls : Form
    {
        private SqlConnection sqlConnection = null;


        private void LoadData(string TableName)
        {

            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Инст\БД\WindowsFormsApplication2\WindowsFormsApplication2\Database1.mdf;Integrated Security=True");
            sqlConnection.Open();
            var get_table = string.Format("SELECT * FROM {0}", TableName);
            SqlDataAdapter sqlDa = new SqlDataAdapter(get_table, sqlConnection);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            dataGridView1.DataSource = dtbl;
        }
        
        public Tabls()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(comboBox1.Text);
            
        }

        private void Tabls_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Инст\БД\WindowsFormsApplication2\WindowsFormsApplication2\Database1.mdf;Integrated Security=True");

            sqlConnection.Open();

            DataTable schema = sqlConnection.GetSchema("Tables");
            List<string> TableNames = new List<string>();
            foreach (DataRow row in schema.Rows)
            {
                TableNames.Add(row[2].ToString());
            }
            foreach (string str in TableNames)
            {
                comboBox1.Items.Add(str);
            }
            sqlConnection.Close();
        }

    }
}
