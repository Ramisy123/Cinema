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
    public partial class AddSession : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlCommand sqlCommand = null;
        public AddSession()
        {
            InitializeComponent();
        }

        private void AddSession_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Инст\БД\WindowsFormsApplication2\WindowsFormsApplication2\Database1.mdf;Integrated Security=True");
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SELECT (Id_Hall) from Hall", sqlConnection);
            SqlCommand sqlCommand2 = new SqlCommand("Select (Id) from Time_Index", sqlConnection);
            SqlDataReader reader;
            reader = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id_Hall", typeof(string));
            dt.Load(reader);
            comboBox1.ValueMember = "Id_Hall";
            comboBox1.DataSource = dt;
            reader = sqlCommand2.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Id", typeof(string));
            dt2.Load(reader);
            comboBox2.ValueMember = "Id";
            comboBox2.DataSource = dt2;
            sqlConnection.Close();
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Инст\БД\WindowsFormsApplication2\WindowsFormsApplication2\Database1.mdf;Integrated Security=True");
            sqlConnection.Open();
            sqlCommand = new SqlCommand("INSERT INTO Session (Time, Hall) VALUES (@Time, @Hall)", sqlConnection);
            sqlCommand.Parameters.Add("@Time", comboBox1.Text);
            sqlCommand.Parameters.Add("@Hall", comboBox2.Text);
            sqlCommand.ExecuteNonQuery();
            comboBox1.Text = "";
            comboBox2.Text = "";
            sqlConnection.Close();
        }
    }
}
